namespace BNet.MCP
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Collections.Generic;
	using Utilities;
	using Incoming;
	using Exceptions;

	public sealed class MCPClient : Client<MCPPacketHandlerAttribute>
	{
		private readonly Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();
		private byte[] chunk;

		private uint cookie,
		             status;

		private string uniquename;

		public MCPClient(IPEndPoint host)
		{
			builder = new MCPPacketBuilder();
			Host = host;
		}

		public bool Reconnecting { get; private set; }

		public event EventHandler<PacketEventArgs> CharLogonEvent
		{
			add
			{
				Type t = typeof(CharLogon);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(CharLogon);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> CreateGameEvent
		{
			add
			{
				Type t = typeof(CreateGame);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(CreateGame);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> CreateGameQueueEvent
		{
			add
			{
				Type t = typeof(CreateGameQueue);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(CreateGameQueue);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GetGameInfoEvent
		{
			add
			{
				Type t = typeof(GetGameInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GetGameInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GetGameListEvent
		{
			add
			{
				Type t = typeof(GetGameList);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GetGameList);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GetMotdEvent
		{
			add
			{
				Type t = typeof(GetMotd);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GetMotd);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> JoinGameEvent
		{
			add
			{
				Type t = typeof(JoinGame);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(JoinGame);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> RequestCharListEvent
		{
			add
			{
				Type t = typeof(RequestCharList);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(RequestCharList);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> StartupEvent
		{
			add
			{
				Type t = typeof(Startup);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Startup);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}

		public override void Connect() { throw new InvalidOperationException("You must not call Connect without all of the proper parameters"); }

		public void Connect(uint mcpCookie, uint mcpStatus, byte[] mcpChunk, string uniqueName)
		{
			cookie = mcpCookie;
			status = mcpStatus;
			chunk = mcpChunk;
			uniquename = uniqueName;

			base.Connect();

			try
			{
				using(var bs = new NetworkStream(socket, false))
				{
					bs.WriteByte(1);
					bs.Flush();
				}

				if(!socket.IsActive())
					throw new IPBannedException();

				using(var packet = new Outgoing.Startup(mcpCookie, mcpStatus, mcpChunk, uniqueName))
					Send(packet);
			}
			catch(Exception e)
			{
				OnException("MCP", e);
			}
		}

		public void Reconnect()
		{
			Reconnecting = true;
			GetMotdEvent += OnMotd;
			Connect(cookie, status, chunk, uniquename);
		}

		public override void OnLogEvent(string s, string m) { base.OnLogEvent("MCP", m); }
		public override void OnException(string s, Exception e) { base.OnException("MCP", e); }
		public override void OnSentPacket(string s, Packet p) { base.OnSentPacket("MCP", p); }
		public override void OnReceivedPacket(string s, Packet p) { base.OnReceivedPacket("MCP", p); }

		private void OnMotd(object sender, PacketEventArgs e)
		{
			if(Reconnecting)
			{
				GetMotdEvent -= OnMotd;
				Reconnecting = false;
			}
		}

		protected override void FirePacket(Packet packet)
		{
			if(events.ContainsKey(packet.GetType()))
				events[packet.GetType()].DynamicInvoke(this, new PacketEventArgs(packet));
		}
	}
}
