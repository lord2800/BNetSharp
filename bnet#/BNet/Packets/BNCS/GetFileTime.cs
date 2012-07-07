using System;

namespace BNet.BNCS.Outgoing
{
	public sealed class GetFileTime : BNCSPacket
	{
		public GetFileTime(uint requestId, string file) : base(BNCSPacketId.GetFileTime)
		{
			Write(requestId);
			Write<uint>(0);
			Write(file);
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.GetFileTime)]
	public sealed class GetFileTime : BNCSPacket
	{
		private GetFileTime(byte[] bytes) : base(BNCSPacketId.GetFileTime)
		{
			SeedBytes(bytes);

			RequestID = Read<uint>();
			Unknown = Read<uint>();
			FileTime = DateTime.FromFileTimeUtc(Read<Int64>());
			FileName = Read();
		}

		public uint RequestID { get; private set; }
		public uint Unknown { get; private set; }
		public DateTime FileTime { get; private set; }
		public string FileName { get; private set; }
		public static GetFileTime Parse(byte[] bytes) { return new GetFileTime(bytes); }
	}
}
