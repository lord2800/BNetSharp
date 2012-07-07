using System;

namespace BNet.BNCS.Outgoing
{
	public sealed class CheckAd : BNCSPacket
	{
		public CheckAd(uint lastId) : base(BNCSPacketId.CheckAd)
		{
			Write("IX86", true);
			Write("D2XP", true);
			Write(lastId);
			Write(Convert.ToUInt32((DateTime.UtcNow - new DateTime(1970, 1, 1).ToUniversalTime()).TotalSeconds));
		}

		public CheckAd() : this(0) { }
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.CheckAd)]
	public sealed class CheckAd : BNCSPacket
	{
		private CheckAd(byte[] bytes) : base(BNCSPacketId.CheckAd)
		{
			SeedBytes(bytes);

			AdID = Read<uint>();
			BannerExtension = Read<uint>();
			FileTime = DateTime.FromFileTimeUtc(Read<Int64>());
			FileName = Read();
			URL = new Uri(Read());
		}

		public uint AdID { get; private set; }
		public uint BannerExtension { get; private set; }
		public DateTime FileTime { get; private set; }
		public string FileName { get; private set; }
		public Uri URL { get; private set; }
		public static CheckAd Parse(byte[] bytes) { return new CheckAd(bytes); }
	}
}
