using System.Diagnostics;
using System.Linq;
using System.Threading;
using BNet.BNCS.Outgoing;
using BNet.MCP.Outgoing;
using AuthInfo = BNet.BNCS.Incoming.AuthInfo;
using Startup = BNet.MCP.Incoming.Startup;

namespace BNet.Clients
{
	using System;
	using System.IO;
	using System.Collections.Generic;
	using System.Net;
	using System.Security.Permissions;
	using Utilities;

	using BNet;
	using Exceptions;
	using BNCS;
	using MCP;
	using BNFTP;

	using BNCSIn = BNCS.Incoming;
	using MCPIn = MCP.Incoming;
	using BNCSOut = BNCS.Outgoing;
	using MCPOut = MCP.Outgoing;

	public delegate void ConnectionReadyEvent(BNetClient client);
	public delegate void MCPConnectEvent();
	public delegate void GameJoinedEvent(LocalGameInfo game);

	public sealed class BNetClient : Loggable, IDisposable
	{
		private readonly string bnet, d2client, game, character, realm;

		private readonly List<CharInfo> chars = new List<CharInfo>();
		private readonly List<LocalGameInfo> games = new List<LocalGameInfo>();
		private readonly List<RealmInfo> realms = new List<RealmInfo>();

		private readonly Dictionary<ushort, WeakReference> remoteGameList = new Dictionary<ushort, WeakReference>();
		private readonly Dictionary<ushort, WeakReference> remoteGamesList = new Dictionary<ushort, WeakReference>();

		private readonly Random rng = new Random();

		private readonly uint versionByte;

		private uint clientToken;
		private bool disposed;
		private bool joinedChannel;
		private WebProxy proxy;
		private CharInfo? selectedChar;
		private RealmInfo? selectedRealm;
		private uint serverToken;

		[SecurityPermission(SecurityAction.LinkDemand)]
		public BNetClient(IPEndPoint host, BattleNetCredential credentials, string realm, string character,
						  string game, string bnet, string d2client)
		{
			var fvi = FileVersionInfo.GetVersionInfo(game);
			versionByte = (uint)fvi.FileBuildPart;

			BNCS = new BNCSClient(host);
			BNFTP = new BNFTPClient(host);

			BNCS.Log += OnLogEvent;
			BNCS.SentPackets += OnSentPacket;
			BNCS.ReceivedPackets += OnReceivedPacket;
			BNCS.ExceptionHandler += OnException;

			BNFTP.Log += OnLogEvent;
			BNFTP.SentPackets += OnSentPacket;
			BNFTP.ReceivedPackets += OnReceivedPacket;
			BNFTP.ExceptionHandler += OnException;

			BNCS.Proxy = Proxy;
			BNFTP.Proxy = Proxy;

			BNCS.AuthInfoEvent += ProcessAuthInfo;
			BNCS.AuthCheckEvent += ProcessAuthCheck;
			BNCS.AccountLoginEvent += ProcessLogonResponse2;
			BNCS.QueryRealmsEvent += ProcessQueryRealms;
			BNCS.RealmLogonEvent += ProcessRealmLogon;
			BNCS.EnterChatEvent += ProcessEnterChat;
			BNCS.CheckAdEvent += ProcessCheckAd;

			Credentials = credentials;
			this.realm = realm;
			this.character = character;
			this.game = game;
			this.bnet = bnet;
			this.d2client = d2client;
		}

		public BNCSClient BNCS { get; private set; }
		public MCPClient MCP { get; private set; }
		public BNFTPClient BNFTP { get; private set; }

		public WebProxy Proxy
		{
			get { return proxy; }
			set
			{
				proxy = value;
				if(BNCS != null) BNCS.Proxy = value;
				if(MCP != null) MCP.Proxy = value;
				if(BNFTP != null) BNFTP.Proxy = value;
			}
		}

		public BattleNetCredential Credentials { get; private set; }

		public IEnumerable<RealmInfo> AvailableRealms
		{
			get { return realms; }
		}

		public IEnumerable<CharInfo> AvailableChars
		{
			get { return chars; }
		}

		public bool Connected
		{
			get
			{
				if(BNCS.Connected)
					return true;
				if(MCP != null && MCP.Connected)
					return true;
				return false;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		public event ConnectionReadyEvent ConnectionReady;
		public event GameJoinedEvent GameJoined;
		public event MCPConnectEvent MCPConnected;

		private void Dispose(bool disposing)
		{
			if(!disposed && disposing)
			{
				BNCS.Dispose();

				BNCS.AuthInfoEvent -= ProcessAuthInfo;
				BNCS.AuthCheckEvent -= ProcessAuthCheck;
				BNCS.AccountLoginEvent -= ProcessLogonResponse2;
				BNCS.QueryRealmsEvent -= ProcessQueryRealms;
				BNCS.RealmLogonEvent -= ProcessRealmLogon;
				BNCS.EnterChatEvent -= ProcessEnterChat;
				BNCS.CheckAdEvent -= ProcessCheckAd;

				if(MCP != null)
				{
					MCP.StartupEvent -= ProcessStartup;
					MCP.RequestCharListEvent -= ProcessCharList;
					MCP.CharLogonEvent -= ProcessCharLogon;
					MCP.CreateGameEvent -= ProcessCreateGame;
					MCP.JoinGameEvent -= ProcessJoinGame;
					MCP.GetGameListEvent -= ProcessGameList;
					MCP.GetGameInfoEvent -= ProcessGameInfo;

					MCP.Dispose();
				}

				disposed = true;
			}
		}

		public void Connect()
		{
			clientToken = (uint)rng.Next();
			BNCS.Connect(versionByte);
		}

		public void Disconnect()
		{
			if(BNCS.Connected)
				BNCS.Disconnect();
			if(MCP != null && MCP.Connected)
				MCP.Disconnect();
		}

		public override void OnLogEvent(string s, string m) { base.OnLogEvent(s, m); }
		public override void OnException(string s, Exception e) { base.OnException(s, e); }
		public override void OnSentPacket(string s, Packet p) { base.OnSentPacket(s, p); }
		public override void OnReceivedPacket(string s, Packet p) { base.OnReceivedPacket(s, p); }

		public LocalGameInfo CreateGame(string name, string password, Difficulty difficulty, string desc, byte maxPlayers,
		                                byte maxLevelDiff, bool joinIfExists)
		{
			if(MCP != null && MCP.Connected)
			{
				if(games.Count == ushort.MaxValue)
					games.Clear();

				int count = games.Count;
				games.Add(new LocalGameInfo(name, password, joinIfExists));
				using(var packet = new CreateGame((ushort)count, name, password, desc, difficulty, maxPlayers, maxLevelDiff))
					MCP.Send(packet);

				return games[count];
			}
			return null;
		}

		public LocalGameInfo CreateGame(string name, Difficulty difficulty) { return CreateGame(name, "", difficulty, "", 8, 0xFF, false); }
		public LocalGameInfo CreateGame(string name, string password, Difficulty difficulty) { return CreateGame(name, password, difficulty, string.Empty, 8, 0xFF, false); }
		public LocalGameInfo CreateGame(string name, Difficulty difficulty, bool joinIfExists) { return CreateGame(name, "", difficulty, "", 8, 0xFF, joinIfExists); }
		public LocalGameInfo CreateGame(string name, string password, Difficulty difficulty, bool joinIfExists) { return CreateGame(name, password, difficulty, "", 8, 0xFF, joinIfExists); }

		public LocalGameInfo JoinGame(string name, string password)
		{
			if(MCP != null && MCP.Connected)
			{
				if(games.Count == ushort.MaxValue)
					games.Clear();

				int count = games.Count;
				games.Add(new LocalGameInfo(name, password, false));
				using(var packet = new JoinGame((ushort)count, name, password))
					MCP.Send(packet);
				return games[count];
			}
			return null;
		}

		public RemoteGameDetails GetGameInfo(string name)
		{
			if(MCP != null && MCP.Connected)
			{
				var key = (ushort)rng.Next(ushort.MaxValue);
				var details = new RemoteGameDetails();
				remoteGameList.Add(key, new WeakReference(details));

				using(var packet = new GetGameInfo(key, name))
					MCP.Send(packet);

				return details;
			}
			return null;
		}

		public RemoteGameList GetGameList(string filter)
		{
			if(MCP != null && MCP.Connected)
			{
				var key = (ushort)rng.Next(ushort.MaxValue);
				var list = new RemoteGameList();
				remoteGamesList.Add(key, new WeakReference(list));

				using(var packet = new GetGameList(key, filter))
					MCP.Send(packet);

				return list;
			}
			return null;
		}

		public void EnterChat()
		{
			if(selectedRealm == null || selectedChar == null) return;
			using(var p = new EnterChat(selectedRealm.Value.Title, selectedChar.Value.Name))
				BNCS.Send(p);
		}

		public void JoinChannel(string channel)
		{
			JoinChannelFlags flags = (joinedChannel ? JoinChannelFlags.ForcedJoin : JoinChannelFlags.D2FirstJoin);
			using(var packet = new JoinChannel(flags, channel))
				BNCS.Send(packet);
			joinedChannel = true;
		}

		public void Say(string message)
		{
			if(joinedChannel)
			{
				using(var packet = new ChatCommand(message))
					BNCS.Send(packet);
			}
		}

		private void ProcessAuthInfo(object o, PacketEventArgs e)
		{
			var packet = e.Packet as AuthInfo;
			if(packet == null) return;

			serverToken = packet.ServerToken;

			if(!File.Exists(packet.FileName) ||
			   (File.Exists(packet.FileName) && File.GetCreationTimeUtc(packet.FileName) < packet.FileTime))
			{
				OnLogEvent("BNetClient", "Got new CheckRevision DLL: {0} {1}".Compose(packet.FileName, packet.FileTime));
				File.WriteAllBytes(packet.FileName, BNFTP.GetFile(packet.FileName, packet.FileTime));
				File.SetCreationTimeUtc(packet.FileName, packet.FileTime);
			}

			using(var p = new AuthCheck(Credentials, clientToken, serverToken, packet.Formula, packet.FileName,
									game, bnet, d2client))
				BNCS.Send(p);
		}

		private void ProcessAuthCheck(object o, PacketEventArgs e)
		{
			var packet = e.Packet as BNCS.Incoming.AuthCheck;
			if(packet == null || packet.Response != AuthCheckResponse.Passed) return;

			using(var p = new GetFileTime(0x80000004, "bnserver-D2DV.ini"))
				BNCS.Send(p);
			using(var p = new AccountLogin(Credentials, clientToken, serverToken))
				BNCS.Send(p);
		}

		private void ProcessLogonResponse2(object o, PacketEventArgs e)
		{
			var packet = e.Packet as BNCS.Incoming.AccountLogin;
			if(packet == null || packet.Response != LogonResponse.Success) return;

			using(var p = new QueryRealms())
				BNCS.Send(p);
		}

		private void ProcessQueryRealms(object o, PacketEventArgs e)
		{
			var packet = e.Packet as BNCS.Incoming.QueryRealms;
			if(packet == null) return;

			if(packet.Realms.Count > 0)
			{
				realms.AddRange(packet.Realms);
				var firstRealm = realms.FirstOrDefault(ri => String.Compare(ri.Title, realm, StringComparison.OrdinalIgnoreCase) == 0);
				foreach(RealmInfo ri in realms)
					if(ri.Title == realm)
						selectedRealm = ri;
				if(firstRealm == default(RealmInfo?) && selectedRealm != default(RealmInfo?))
					OnLogEvent("BNetClient", "Failed to find realm when the foreach found a realm?!");
				if(selectedRealm == default(RealmInfo?))
					throw new RealmNotFoundException(realm);

				using(var p = new RealmLogin(clientToken, serverToken, packet.Realms[0]))
					BNCS.Send(p);
			}
		}

		private void ProcessRealmLogon(object o, PacketEventArgs e)
		{
			var packet = e.Packet as BNCS.Incoming.RealmLogin;
			if(packet == null || !packet.LogonSuccess) return;

			MCP = new MCPClient(new IPEndPoint(packet.RealmIP, packet.Port));

			MCP.Log += OnLogEvent;
			MCP.SentPackets += OnSentPacket;
			MCP.ReceivedPackets += OnReceivedPacket;
			MCP.ExceptionHandler += OnException;

			MCP.Proxy = Proxy;

			MCP.StartupEvent += ProcessStartup;
			MCP.RequestCharListEvent += ProcessCharList;
			MCP.CharLogonEvent += ProcessCharLogon;
			MCP.CreateGameEvent += ProcessCreateGame;
			MCP.JoinGameEvent += ProcessJoinGame;
			MCP.GetGameListEvent += ProcessGameList;
			MCP.GetGameInfoEvent += ProcessGameInfo;

			MCP.Connect(packet.MCPCookie, packet.MCPStatus, packet.MCPChunk, packet.UniqueName);
			if(MCPConnected != null)
				MCPConnected();
		}

		private void ProcessEnterChat(object o, PacketEventArgs e)
		{
			using(var packet = new RequestNews())
				BNCS.Send(packet);

			using(var packet = new CheckAd())
				BNCS.Send(packet);
		}

		private void ProcessStartup(object o, PacketEventArgs e)
		{
			var packet = e.Packet as Startup;
			if(packet == null || packet.Response != StartupResponse.Success) return;

			if(MCP.Reconnecting)
			{
				// if we're reconnecting, we know the character already exists
				using(var p = new CharLogon((string)selectedChar))
					MCP.Send(p);
			}
			else
			{
				// otherwise, we just request the list and continue on
				using(var p = new RequestCharList(8))
					MCP.Send(p);
			}
		}

		private void ProcessCharList(object o, PacketEventArgs e)
		{
			var packet = e.Packet as MCP.Incoming.RequestCharList;
			if(packet == null || packet.TotalChars <= 0) return;

			chars.AddRange(packet.Characters);
			foreach(CharInfo ci in packet.Characters)
			{
				if(ci.Name == character)
					selectedChar = ci;
			}
			if(selectedChar == null)
				throw new CharacterNotFoundException(character);

			using(var p = new CharLogon(packet.Characters[0].Name))
				MCP.Send(p);
		}

		private void ProcessCharLogon(object o, PacketEventArgs e)
		{
			var packet = e.Packet as MCP.Incoming.CharLogon;
			if(packet == null || packet.Response != CharLogonResponse.Success) return;

			EnterChat();

			using(var p = new GetMotd())
				MCP.Send(p);

			using(var p = new GetChannelList())
				BNCS.Send(p);

			if(ConnectionReady != null)
				ConnectionReady(this);
		}

		private void ProcessCreateGame(object o, PacketEventArgs e)
		{
			var packet = e.Packet as MCP.Incoming.CreateGame;
			if(packet == null) return;

			var localGameInfo = games[packet.RequestId];
			localGameInfo.CreateResult = packet.Result;

			var willJoin = packet.Result == CreateGameResult.Success ||
			               (packet.Result == CreateGameResult.AlreadyExists && localGameInfo.JoinIfExists);

			if(willJoin)
			{
				using(var p = new JoinGame(packet.RequestId, localGameInfo.Name, localGameInfo.Pass))
					MCP.Send(p);
			}
		}

		private void ProcessJoinGame(object o, PacketEventArgs e)
		{
			var packet = e.Packet as MCP.Incoming.JoinGame;
			if(packet == null) return;

			var localGameInfo = games[packet.RequestID];
			localGameInfo.JoinResult = packet.Result;

			if(packet.Result != JoinGameResult.Success) return;

			using(var p = new NotifyJoin(localGameInfo.Name, localGameInfo.Pass, (byte)versionByte))
				BNCS.Send(p);
			using(var p = new LeaveChat())
				BNCS.Send(p);

			if(selectedChar == null) return;

			localGameInfo.JoinedGame(new IPEndPoint(packet.GameServerIP, 4000), versionByte, packet.GameHash, packet.GameToken,
			                         selectedChar.Value);

			if(GameJoined != null)
				GameJoined(localGameInfo);
		}

		private void ProcessGameList(object o, PacketEventArgs e)
		{
			var packet = e.Packet as MCP.Incoming.GetGameList;
			if(packet == null) return;

			if(remoteGamesList.ContainsKey(packet.RequestID) && remoteGamesList[packet.RequestID].IsAlive)
			{
				var remoteGames = remoteGamesList[packet.RequestID].Target as RemoteGameList;
				if(remoteGames == null) return;
				if((packet.Status & GameStatus.ContainsGame) != GameStatus.ContainsGame ||
				   (String.IsNullOrEmpty(packet.Name) && packet.NumberOfPlayers == 0))
					remoteGames.FinishList();
				else
				{
					remoteGames.AddGame(new RemoteGameInfo(packet.Name, packet.Description, (packet.Status | GameStatus.ContainsGame),
					                                       packet.NumberOfPlayers));
				}
			}
		}

		private void ProcessGameInfo(object o, PacketEventArgs e)
		{
			var packet = e.Packet as MCP.Incoming.GetGameInfo;
			if(packet == null) return;

			if(remoteGameList.ContainsKey(packet.RequestID) && remoteGameList[packet.RequestID].IsAlive)
			{
				var remoteGame = remoteGameList[packet.RequestID].Target as RemoteGameDetails;
				if(remoteGame == null) return;

				remoteGame.AddData(packet.Status, packet.Uptime, packet.MaxPlayers, packet.CurrentPlayers, packet.PlayerClasses,
				                   packet.PlayerLevels, packet.PlayerNames);
			}
		}

		private void ProcessCheckAd(object o, PacketEventArgs e)
		{
			var packet = e.Packet as BNCS.Incoming.CheckAd;
			if(packet == null) return;

			BNFTP.GetFile(packet.FileName, packet.FileTime);
			using(var p = new DisplayAd(packet.AdID, packet.FileName, packet.URL))
				BNCS.Send(p);
		}
	}

	public sealed class LocalGameInfo
	{
		public LocalGameInfo(string name, string pass, bool ifExist)
		{
			Name = name;
			Pass = pass;
			JoinIfExists = ifExist;

			CreateResult = CreateGameResult.ResultNotAvailable;
			JoinResult = JoinGameResult.ResultNotAvailable;
		}

		public string Name { get; private set; }
		public string Pass { get; private set; }
		public bool JoinIfExists { get; private set; }
		public CreateGameResult CreateResult { get; internal set; }
		public JoinGameResult JoinResult { get; internal set; }

		public IPEndPoint Host { get; private set; }
		public uint Version { get; private set; }
		public uint Hash { get; private set; }
		public ushort Token { get; private set; }
		public CharInfo Character { get; private set; }

		public void JoinedGame(IPEndPoint host, uint version, uint hash, ushort token, CharInfo chr)
		{
			Host = host;
			Version = version;
			Hash = hash;
			Token = token;
			Character = chr;
		}
	}

	public sealed class RemoteGameInfo
	{
		public RemoteGameInfo(string name, string desc, GameStatus status, byte numPlayers)
		{
			Name = name;
			Description = desc;
			Status = status;
			NumberOfPlayers = numPlayers;
		}

		public byte NumberOfPlayers { get; private set; }
		public GameStatus Status { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
	}

	public sealed class RemoteGameDetails : IDisposable
	{
		private readonly ManualResetEvent complete = new ManualResetEvent(false);
		private readonly List<PlayerData> players = new List<PlayerData>();

		public GameStatus Status { get; private set; }
		public TimeSpan Duration { get; private set; }
		public byte MaxPlayers { get; private set; }
		public byte CurrentPlayers { get; private set; }

		public IEnumerable<PlayerData> Players
		{
			get { return players; }
		}

		public void Dispose()
		{
//			complete.Dispose();
			GC.SuppressFinalize(this);
		}

		public void AddData(GameStatus status, TimeSpan uptime, byte maxPlayers, byte currentPlayers, CharClass[] classes,
		                    byte[] levels, string[] names)
		{
			Status = status;
			Duration = uptime;
			MaxPlayers = maxPlayers;
			CurrentPlayers = currentPlayers;
			for(int i = 0; names.Length < i && !String.IsNullOrEmpty(names[i]) && i < 16; i++)
				players.Add(new PlayerData(names[i], levels[i], classes[i]));

			complete.Set();
		}

		public void WaitForData() { complete.WaitOne(); }

		public struct PlayerData
		{
			public PlayerData(string name, byte level, CharClass clas) : this()
			{
				Level = level;
				Class = clas;
				Name = name;
			}

			public byte Level { get; private set; }
			public CharClass Class { get; private set; }
			public string Name { get; private set; }
		}
	}

	public sealed class RemoteGameList : IDisposable
	{
		private readonly ManualResetEvent complete = new ManualResetEvent(false);
		private readonly List<RemoteGameInfo> games = new List<RemoteGameInfo>();
		private readonly object syncObj = new object();

		public bool IsCompleteList { get; private set; }

		public IEnumerable<RemoteGameInfo> Games
		{
			get { return games.AsReadOnly(); }
		}

		public void Dispose()
		{
//			complete.Dispose();
			GC.SuppressFinalize(this);
		}

		public void AddGame(RemoteGameInfo game)
		{
			lock(syncObj)
			{
				games.Add(game);
			}
		}

		public void WaitForList() { complete.WaitOne(5000); }

		public void FinishList()
		{
			lock(syncObj)
			{
				IsCompleteList = true;
			}
			complete.Set();
		}
	}
}
