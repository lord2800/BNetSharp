namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 9)]
	[D2GSPacketHandler(D2GSIncomingPacketId.WalkVerify)]
	public sealed class WalkVerify : D2GSPacket
	{
		public WalkVerify(byte[] bytes) : base(D2GSIncomingPacketId.WalkVerify) { SeedBytes(bytes); }
		public static WalkVerify Parse(byte[] bytes) { return new WalkVerify(bytes); }
	}
}
