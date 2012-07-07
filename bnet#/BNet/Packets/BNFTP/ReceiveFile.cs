using System;

namespace BNet.BNFTP.Incoming
{
	[BNFTPv1PacketHandler]
	internal sealed class ReceiveFile : BNFTPv1Packet
	{
		private ReceiveFile(byte[] bytes)
		{
			SeedBytes(bytes);

			Unknown = Read<ushort>();
			FileSize = Read<int>();
			BannerId = Read<uint>();
			BannerFileExtension = Read<uint>();
			FileTime = DateTime.FromFileTimeUtc(Read<Int64>());
			FileName = Read();
		}

		public ushort Unknown { get; private set; }
		public int FileSize { get; private set; }
		public uint BannerId { get; private set; }
		public uint BannerFileExtension { get; private set; }
		public DateTime FileTime { get; private set; }
		public string FileName { get; private set; }
		public static ReceiveFile Parse(byte[] bytes) { return new ReceiveFile(bytes); }
	}
}
