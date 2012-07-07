namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 11)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Relator1)]
	public sealed class Relator1 : D2GSPacket
	{
		public Relator1(byte[] bytes) : base(D2GSIncomingPacketId.Relator1) { SeedBytes(bytes); }
		public static Relator1 Parse(byte[] bytes) { return new Relator1(bytes); }
	}

	[PacketLength(Length = 11)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Relator2)]
	public sealed class Relator2 : D2GSPacket
	{
		public Relator2(byte[] bytes) : base(D2GSIncomingPacketId.Relator2) { SeedBytes(bytes); }
		public static Relator2 Parse(byte[] bytes) { return new Relator2(bytes); }
	}
}
