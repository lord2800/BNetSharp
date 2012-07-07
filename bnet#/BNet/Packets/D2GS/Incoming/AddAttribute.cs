namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 3)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddAttributeByte)]
	public sealed class AddAttributeByte : D2GSPacket
	{
		public AddAttributeByte(byte[] bytes) : base(D2GSIncomingPacketId.AddAttributeByte) { SeedBytes(bytes); }
		public static AddAttributeByte Parse(byte[] bytes) { return new AddAttributeByte(bytes); }
	}

	[PacketLength(Length = 4)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddAttributeWord)]
	public sealed class AddAttributeWord : D2GSPacket
	{
		public AddAttributeWord(byte[] bytes) : base(D2GSIncomingPacketId.AddAttributeWord) { SeedBytes(bytes); }
		public static AddAttributeWord Parse(byte[] bytes) { return new AddAttributeWord(bytes); }
	}

	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddAttributeDword)]
	public sealed class AddAttributeDword : D2GSPacket
	{
		public AddAttributeDword(byte[] bytes) : base(D2GSIncomingPacketId.AddAttributeDword) { SeedBytes(bytes); }
		public static AddAttributeDword Parse(byte[] bytes) { return new AddAttributeDword(bytes); }
	}
}
