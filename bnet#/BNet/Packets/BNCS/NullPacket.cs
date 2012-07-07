namespace BNet.BNCS.Outgoing
{
	public sealed class NullPacket : BNCSPacket
	{
		public NullPacket() : base(BNCSPacketId.Null) { }
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.Null)]
	public sealed class NullPacket : BNCSPacket
	{
		private NullPacket() : base(BNCSPacketId.Null) { }
		public static NullPacket Parse(byte[] bytes) { return new NullPacket(); }
	}
}
