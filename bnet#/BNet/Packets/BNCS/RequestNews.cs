using System;
using System.Collections.Generic;

namespace BNet.BNCS.Outgoing
{
	public sealed class RequestNews : BNCSPacket
	{
		public RequestNews(DateTime time) : base(BNCSPacketId.NewsInfo)
		{
			TimeSpan offset = (time.ToUniversalTime() - new DateTime(1970, 1, 1).ToUniversalTime());
			Write(Convert.ToUInt32(offset.TotalSeconds));
		}

		public RequestNews() : this(DateTime.Now) { }
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.NewsInfo)]
	public sealed class RequestNews : BNCSPacket
	{
		private RequestNews(byte[] bytes) : base(BNCSPacketId.NewsInfo)
		{
			SeedBytes(bytes);

			NewsEntries = new List<NewsInfo>();
			byte count = Read<byte>();
			LastLogon = new DateTime(1970, 1, 1).ToUniversalTime().AddSeconds(Read<uint>());
			OldestNews = new DateTime(1970, 1, 1).ToUniversalTime().AddSeconds(Read<uint>());
			NewestNews = new DateTime(1970, 1, 1).ToUniversalTime().AddSeconds(Read<uint>());
			for(byte i = 0; i < count; i++)
				NewsEntries.Add(new NewsInfo(Read<uint>(), Read()));
		}

		public List<NewsInfo> NewsEntries { get; private set; }
		public DateTime LastLogon { get; private set; }
		public DateTime OldestNews { get; private set; }
		public DateTime NewestNews { get; private set; }
		public static RequestNews Parse(byte[] bytes) { return new RequestNews(bytes); }
	}
}

namespace BNet.BNCS
{
	public struct NewsInfo
	{
		public NewsInfo(uint timestamp, string news) : this()
		{
			if(timestamp == 0)
				Timestamp = DateTime.UtcNow;
			else
				Timestamp = new DateTime(1970, 1, 1).ToUniversalTime().AddSeconds(timestamp);
			News = news;
		}

		public DateTime Timestamp { get; private set; }
		public string News { get; private set; }

		public override bool Equals(object obj) { return obj is NewsInfo && ((NewsInfo)obj).News == News && ((NewsInfo)obj).Timestamp == Timestamp; }
		public override int GetHashCode() { return base.GetHashCode(); }
		public static bool operator ==(NewsInfo obj1, NewsInfo obj2) { return obj1.Equals(obj2); }
		public static bool operator !=(NewsInfo obj1, NewsInfo obj2) { return !obj1.Equals(obj2); }
	}
}
