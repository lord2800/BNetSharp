namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.MercForHire)]
	public sealed class MercForHire : D2GSPacket
	{
		public MercForHire(byte[] bytes) : base(D2GSIncomingPacketId.MercForHire) { SeedBytes(bytes); }
		public static MercForHire Parse(byte[] bytes) { return new MercForHire(bytes); }
	}
}
