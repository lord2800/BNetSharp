using System.Globalization;
using BNet.BNCS.Outgoing;

namespace BNet.BNCS
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Threading;
	using System.Collections.Generic;
	using Utilities;
	using Incoming;
	using Exceptions;

	public sealed class BNCSClient : Client<BNCSPacketHandlerAttribute>
	{
		private readonly Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();
		private bool disposed;

		public BNCSClient(IPEndPoint host)
		{
			builder = new BNCSPacketBuilder();
			PingEvent += ProcessPing;
			NullPacketEvent += ProcessNull;

			Host = host;
		}

		public event EventHandler<PacketEventArgs> AccountLoginEvent
		{
			add
			{
				Type t = typeof(AccountLogin);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AccountLogin);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AdvertiseGameEvent
		{
			add
			{
				Type t = typeof(AdvertiseGame);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AdvertiseGame);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AuthCheckEvent
		{
			add
			{
				Type t = typeof(AuthCheck);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AuthCheck);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AuthInfoEvent
		{
			add
			{
				Type t = typeof(AuthInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AuthInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ChatEvent
		{
			add
			{
				Type t = typeof(ChatEvent);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ChatEvent);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> CheckAdEvent
		{
			add
			{
				Type t = typeof(CheckAd);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(CheckAd);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> EnterChatEvent
		{
			add
			{
				Type t = typeof(EnterChat);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(EnterChat);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GetChannelListEvent
		{
			add
			{
				Type t = typeof(GetChannelList);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GetChannelList);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GetFileTimeEvent
		{
			add
			{
				Type t = typeof(GetFileTime);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GetFileTime);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NullPacketEvent
		{
			add
			{
				Type t = typeof(NullPacket);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NullPacket);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PingEvent
		{
			add
			{
				Type t = typeof(Ping);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Ping);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> QueryRealmsEvent
		{
			add
			{
				Type t = typeof(QueryRealms);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(QueryRealms);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> RealmLogonEvent
		{
			add
			{
				Type t = typeof(RealmLogin);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(RealmLogin);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> RequestNewsEvent
		{
			add
			{
				Type t = typeof(RequestNews);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(RequestNews);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if(!disposed && disposing)
			{
				PingEvent -= ProcessPing;
				NullPacketEvent -= ProcessNull;

				disposed = true;
			}
		}

		public override void Connect() { throw new InvalidOperationException("You must not call Connect without all of the proper parameters"); }

		public void Connect(uint version)
		{
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

				var ci = CultureInfo.CurrentUICulture;
				var ri = RegionInfo.CurrentRegion;
				var address = (socket.LocalEndPoint as IPEndPoint ?? new IPEndPoint(IPAddress.Loopback, 0)).Address;
				using(var packet = new Outgoing.AuthInfo(address, version, ci, ri))
					Send(packet);
			}
			catch(ThreadInterruptedException)
			{
			}
			catch(Exception e)
			{
				OnException("BNCS", e);
			}
		}

		public override void OnLogEvent(string s, string m) { base.OnLogEvent("BNCS", m); }
		public override void OnException(string s, Exception e) { base.OnException("BNCS", e); }
		public override void OnSentPacket(string s, Packet p) { base.OnSentPacket("BNCS", p); }
		public override void OnReceivedPacket(string s, Packet p) { base.OnReceivedPacket("BNCS", p); }

		protected override void FirePacket(Packet packet)
		{
			if(events.ContainsKey(packet.GetType()))
				events[packet.GetType()].DynamicInvoke(this, new PacketEventArgs(packet));
		}

		private void ProcessPing(object o, PacketEventArgs e)
		{
			using(var packet = new Pong(e.Packet as Ping))
				Send(packet);
		}

		private void ProcessNull(object o, PacketEventArgs e)
		{
			using(var packet = new Outgoing.NullPacket())
				Send(packet);
		}
	}
}
