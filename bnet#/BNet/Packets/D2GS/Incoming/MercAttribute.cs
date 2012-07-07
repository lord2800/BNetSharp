namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.MercAttributeByte)]
	public sealed class MercAttributeByte : D2GSPacket
	{
		public MercAttributeByte(byte[] bytes) : base(D2GSIncomingPacketId.MercAttributeByte) { SeedBytes(bytes); }
		public static MercAttributeByte Parse(byte[] bytes) { return new MercAttributeByte(bytes); }
	}

	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.MercAttributeWord)]
	public sealed class MercAttributeWord : D2GSPacket
	{
		public MercAttributeWord(byte[] bytes) : base(D2GSIncomingPacketId.MercAttributeWord) { SeedBytes(bytes); }
		public static MercAttributeWord Parse(byte[] bytes) { return new MercAttributeWord(bytes); }
	}

	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.MercAttributeDword)]
	public sealed class MercAttributeDword : D2GSPacket
	{
		public MercAttributeDword(byte[] bytes) : base(D2GSIncomingPacketId.MercAttributeDword) { SeedBytes(bytes); }
		public static MercAttributeDword Parse(byte[] bytes) { return new MercAttributeDword(bytes); }
	}
}
