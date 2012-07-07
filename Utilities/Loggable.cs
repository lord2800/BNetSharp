using System;

namespace Utilities
{
	public abstract class Loggable
	{
		public event Action<string, string> Log = delegate { };
		public event Action<string, Packet> SentPackets = delegate { };
		public event Action<string, Packet> ReceivedPackets = delegate { };
		public event Action<string, Exception> ExceptionHandler = delegate { };

		public virtual void OnException(string s, Exception e) { ExceptionHandler(s ?? GetType().Name, e); }
		public virtual void OnSentPacket(string s, Packet p) { SentPackets(s ?? GetType().Name, p); }
		public virtual void OnReceivedPacket(string s, Packet p) { ReceivedPackets(s ?? GetType().Name, p); }
		public virtual void OnLogEvent(string s, string m) { Log(s ?? GetType().Name, m); }
	}
}
