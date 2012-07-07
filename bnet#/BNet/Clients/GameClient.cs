using BNet.D2GS.Outgoing;

namespace BNet.Clients
{
	using System;
	using System.Net;
	using System.Threading;
	using Utilities;
	using D2GS;
	using D2GSIn = D2GS.Incoming;
	using D2GSOut = D2GS.Outgoing;

	public sealed class GameClient : Loggable, IDisposable
	{
		public D2GSClient D2GS { get; private set; }
		private readonly LocalGameInfo game;
		private DateTime lastping, lastpong;
		private Thread ping;
		private WebProxy proxy;

		public GameClient(LocalGameInfo game)
		{
			this.game = game;

			// create initial Warden key


			D2GS = new D2GSClient(game.Host) {Proxy = Proxy};

			D2GS.Log += OnLogEvent;
			D2GS.SentPackets += OnSentPacket;
			D2GS.ReceivedPackets += OnReceivedPacket;
			D2GS.ExceptionHandler += OnException;

			D2GS.GameLoadedEvent += ProcessGameJoin;
			D2GS.GameFlagsEvent += ProcessGameFlags;
			D2GS.GameExitSuccessEvent += ProcessGameExit;
			D2GS.GameDroppedEvent += ProcessGameExit;
			D2GS.PongEvent += ProcessPong;
			D2GS.WardenRequestEvent += ProcessWarden;
		}

		public WebProxy Proxy
		{
			get { return proxy; }
			set
			{
				proxy = value;
				if(D2GS != null) D2GS.Proxy = value;
			}
		}

		public bool Connected { get { return D2GS.Connected; } }

		public void Dispose()
		{
			Disconnect();

			D2GS.GameLoadedEvent -= ProcessGameJoin;
			D2GS.GameFlagsEvent -= ProcessGameFlags;
			D2GS.GameExitSuccessEvent -= ProcessGameExit;
			D2GS.GameDroppedEvent -= ProcessGameExit;
			D2GS.PongEvent -= ProcessPong;
			D2GS.WardenRequestEvent -= ProcessWarden;

			D2GS.Dispose();
		}

		public event Action GameEntered;
		public event Action GameExiting;
		public event Action GameExited;

		public void Connect() { D2GS.Connect(game.Version, game.Hash, game.Token, game.Character); }

		public void Disconnect()
		{
			if(D2GS.Connected)
				D2GS.Disconnect();
		}

		public override void OnLogEvent(string s, string m) { base.OnLogEvent(s, m); }
		public override void OnException(string s, Exception e) { base.OnException(s, e); }
		public override void OnSentPacket(string s, Packet p) { base.OnSentPacket(s, p); }
		public override void OnReceivedPacket(string s, Packet p) { base.OnReceivedPacket(s, p); }

		public void Say(string message)
		{
			using(var packet = new RegularChat(message))
				D2GS.Send(packet);
		}

		public void Exit()
		{
			if(ping != null && ping.IsAlive)
				ping.Abort();

			using(var packet = new LeaveGame())
				D2GS.Send(packet);

			if(GameExiting != null)
				GameExiting();
		}

		private void ProcessGameJoin(object o, PacketEventArgs e)
		{
			using(var packet = new JoinGame())
				D2GS.Send(packet);

			if(GameEntered != null)
				GameEntered();
		}

		private void ProcessGameFlags(object o, PacketEventArgs e)
		{
			using(var packet = new Ping())
				D2GS.Send(packet);

			ping = new Thread(PingThread) {Name = "GameClient Ping Thread", IsBackground = true};
			ping.Start();
		}

		private void ProcessPong(object sender, PacketEventArgs e) { lastpong = DateTime.Now; }

		private void ProcessGameExit(object o, PacketEventArgs e)
		{
			if(GameExited != null)
				GameExited();
		}

		private void ProcessWarden(object o, PacketEventArgs e)
		{
			var packet = e.Packet as D2GSIn.WardenRequest;
			//packet.Decrypt(key);
		}

		private void PingThread()
		{
			try
			{
				while(Connected)
				{
					if(lastpong - lastping > TimeSpan.FromSeconds(30))
					{
						// assume the connection dropped
						if(GameExited != null)
							GameExited();
						Disconnect();
						return;
					}
					if(Connected && (DateTime.Now - lastping >= TimeSpan.FromSeconds(5)))
					{
						using(var packet = new Ping())
							D2GS.Send(packet);
						lastping = DateTime.Now;
					}

					Thread.Sleep(50);
				}
			}
			catch(ThreadAbortException)
			{
			}
			catch(Exception e)
			{
				D2GS.OnException("GameClient", e);
			}
		}
	}
}
