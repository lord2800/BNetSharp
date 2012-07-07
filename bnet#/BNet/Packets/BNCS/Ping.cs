using BNet.BNCS.Incoming;

namespace BNet.BNCS.Outgoing
{
	public sealed class Pong : BNCSPacket
	{
		public Pong(Ping p) : base(BNCSPacketId.Ping) { Write(p.Token); }
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.Ping)]
	public sealed class Ping : BNCSPacket
	{
		private Ping(byte[] bytes) : base(BNCSPacketId.Ping)
		{
			SeedBytes(bytes);

			Token = Read<uint>();
		}

		public uint Token { get; private set; }
		public static Ping Parse(byte[] bytes) { return new Ping(bytes); }
	}
}
