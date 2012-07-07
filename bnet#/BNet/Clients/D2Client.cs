using BNet.BNCS;
using BNet.BNCS.Incoming;
using BNet.MCP;
using BNet.MCP.Incoming;

namespace BNet.Clients
{
	using System;
	using System.IO;
	using System.Threading;
	using System.Net;
	using Utilities;

	public delegate void AuthCheckResult(AuthCheckResponse response, string reason);
	public delegate void AccountLoginResult(LogonResponse response, string reason);
	public delegate void MCPConnectStatus(bool success, MCPStatusResult result);
	public delegate void ServerConnected();
	public delegate void CharLogonResult(CharLogonResponse response);

	public class D2Client : Loggable, IDisposable
	{
		private bool disposed;
		private bool enteredChat;
		private WebProxy proxy;

		public D2Client(IPEndPoint host, BattleNetCredential credentials, string realm, string character,
		                string game, string bnet, string d2client)
		{
			BNet = new BNetClient(host, credentials, realm, character, game, bnet, d2client);

			BNet.Log += OnLogEvent;
			BNet.SentPackets += OnSentPacket;
			BNet.ReceivedPackets += OnReceivedPacket;
			BNet.ExceptionHandler += OnException;

			BNet.Proxy = Proxy;

			BNet.BNCS.AuthCheckEvent += OnAuthCheck;
			BNet.BNCS.AccountLoginEvent += OnAccountLogin;
			BNet.BNCS.RealmLogonEvent += OnRealmLogin;
			BNet.BNCS.EnterChatEvent += OnEnterChat;

			BNet.MCPConnected += OnMCPConnected;
			BNet.ConnectionReady += OnConnected;
		}

		public BNetClient BNet { get; private set; }
		public GameClient Game { get; private set; }

		public WebProxy Proxy
		{
			get { return proxy; }
			set
			{
				proxy = value;
				if(BNet != null) BNet.Proxy = value;
				if(Game != null) Game.Proxy = value;
			}
		}

		public bool Connected
		{
			get { return BNet.Connected || (Game != null && Game.Connected); }
		}

		public bool InGame
		{
			get { return CurrentGame != null && CurrentGame.JoinResult == JoinGameResult.Success; }
		}

		public bool KeepBNCS { get; set; }
		public bool KeepMCP { get; set; }
		public LocalGameInfo CurrentGame { get; private set; }

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		public event AuthCheckResult AuthCheckEvent;
		public event AccountLoginResult AccountLoginEvent;
		public event MCPConnectStatus RealmLoginEvent;
		public event ServerConnected BNCSConnected;
		public event ServerConnected MCPConnected;
		public event CharLogonResult CharLoginEvent;
		public event ServerConnected ConnectionReady;

		protected virtual void Dispose(bool disposing)
		{
			if(disposing && !disposed)
			{
				BNet.BNCS.AuthCheckEvent -= OnAuthCheck;
				BNet.BNCS.AccountLoginEvent -= OnAccountLogin;
				BNet.BNCS.RealmLogonEvent -= OnRealmLogin;
				BNet.BNCS.EnterChatEvent -= OnEnterChat;
				BNet.MCPConnected -= OnMCPConnected;
				if(BNet.MCP != null)
				{
					BNet.MCP.StartupEvent -= OnMCPStartup;
					BNet.MCP.CharLogonEvent -= OnCharLogin;
				}
				BNet.Dispose();

				if(Game != null)
					Game.Dispose();
				disposed = true;
			}
		}

		public void Connect()
		{
			if(Connected)
				return;

			BNet.ConnectionReady += OnLogin;
			BNet.Connect();
		}

		public void Disconnect()
		{
			if(!Connected)
				return;

			BNet.Disconnect();
			if(Game != null && Game.Connected) Game.Disconnect();
		}

// ReSharper disable RedundantOverridenMember
		public override void OnLogEvent(string s, string m) { base.OnLogEvent(s, m); }
		public override void OnException(string s, Exception e) { base.OnException(s, e); }
		public override void OnSentPacket(string s, Packet p) { base.OnSentPacket(s, p); }
		public override void OnReceivedPacket(string s, Packet p) { base.OnReceivedPacket(s, p); }
// ReSharper restore RedundantOverridenMember

		private void OnLogin(BNetClient c)
		{
			BNet.ConnectionReady -= OnLogin;
			if(!KeepBNCS)
				BNet.BNCS.Disconnect();
		}

		private void OnExitGame()
		{
			if(!KeepMCP && !BNet.MCP.Connected)
				BNet.MCP.Reconnect();
			BNet.EnterChat();
			CurrentGame = null;
		}

		private void OnAuthCheck(object o, PacketEventArgs e)
		{
			var packet = ((AuthCheck)e.Packet);
			if(AuthCheckEvent != null)
				AuthCheckEvent(packet.Response, packet.Reason);
		}

		private void OnAccountLogin(object o, PacketEventArgs e)
		{
			var packet = ((AccountLogin)e.Packet);
			if(AccountLoginEvent != null)
				AccountLoginEvent(packet.Response, packet.Reason);
		}

		private void OnRealmLogin(object o, PacketEventArgs e)
		{
			var packet = (RealmLogin)e.Packet;
			if(RealmLoginEvent != null)
				RealmLoginEvent(packet.LogonSuccess, (MCPStatusResult)packet.MCPStatus);
		}

		private void OnEnterChat(object o, PacketEventArgs e)
		{
			if(BNCSConnected != null && !enteredChat)
				BNCSConnected();
			enteredChat = true;
		}

		private void OnMCPConnected()
		{
			BNet.MCP.StartupEvent += OnMCPStartup;
			BNet.MCP.CharLogonEvent += OnCharLogin;
		}

		private void OnMCPStartup(object o, PacketEventArgs e)
		{
			if(MCPConnected != null)
				MCPConnected();
		}

		private void OnCharLogin(object o, PacketEventArgs e)
		{
			if(CharLoginEvent != null)
				CharLoginEvent(((CharLogon)e.Packet).Response);
		}

		private void OnConnected(BNetClient client)
		{
			if(ConnectionReady != null)
				ConnectionReady();
		}

		public RemoteGameList GetGameList() { return GetGameList(""); }

		public RemoteGameList GetGameList(string filter)
		{
			if(!Connected)
				return null;

			RemoteGameList list = BNet.GetGameList(filter);
			list.WaitForList();
			if(list.IsCompleteList)
				return list;
			return null;
		}

		public RemoteGameDetails GetGameInfo(string name)
		{
			if(!Connected)
				return null;

			RemoteGameDetails game = BNet.GetGameInfo(name);
			game.WaitForData();
			return game;
		}

		public bool JoinGame(string name) { return JoinGame(name, ""); }

		public bool JoinGame(string name, string pass)
		{
			if(!Connected)
				return false;

			var didEnter = false;
			var joined = new ManualResetEvent(false);
			GameJoinedEvent join = delegate {
				Game = new GameClient(CurrentGame);

				Game.Log += OnLogEvent;
				Game.SentPackets += OnSentPacket;
				Game.ReceivedPackets += OnReceivedPacket;
				Game.ExceptionHandler += OnException;

				Game.Proxy = Proxy;

				Game.GameExited += OnExitGame;

				var enter = new ManualResetEvent(false);
				Action entered = delegate {
					if(CurrentGame.JoinResult == JoinGameResult.Success)
					{
						if(!KeepMCP)
							BNet.MCP.Disconnect();
						didEnter = true;
						enter.Set();
					}
				};

				Game.GameEntered += entered;
				Game.Connect();

				enter.WaitOne(7000);
				Game.GameEntered -= entered;
				joined.Set();
			};

			BNet.GameJoined += join;
			CurrentGame = BNet.JoinGame(name, pass);

			joined.WaitOne();
			BNet.GameJoined -= join;

			return didEnter;
		}

		public void RejoinGame()
		{
			if(BNet.Connected && Game == null && CurrentGame != null)
				JoinGame(CurrentGame.Name, CurrentGame.Pass);
		}

		public void Say(string message)
		{
			if(!Connected)
				return;

			if(InGame)
				Game.Say(message);
			else
				BNet.Say(message);
		}

		public bool ExitGame()
		{
			if(Game != null && Game.Connected)
			{
				var didExit = false;
				var gameExited = new ManualResetEvent(false);
				Action exited = delegate {
					didExit = true;
					gameExited.Set();
				};
				Game.GameExited += exited;

				Game.Exit();
				gameExited.WaitOne(7000);
				Game.GameExited -= exited;

				Game.Dispose();
				Game = null;
				return didExit;
			}
			return false;
		}
	}
}
