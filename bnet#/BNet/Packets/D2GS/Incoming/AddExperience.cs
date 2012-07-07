namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 2)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddExperienceByte)]
	public sealed class AddExperienceByte : D2GSPacket
	{
		public AddExperienceByte(byte[] bytes) : base(D2GSIncomingPacketId.AddExperienceByte) { SeedBytes(bytes); }
		public static AddExperienceByte Parse(byte[] bytes) { return new AddExperienceByte(bytes); }
	}

	[PacketLength(Length = 3)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddExperienceWord)]
	public sealed class AddExperienceWord : D2GSPacket
	{
		public AddExperienceWord(byte[] bytes) : base(D2GSIncomingPacketId.AddExperienceWord) { SeedBytes(bytes); }
		public static AddExperienceWord Parse(byte[] bytes) { return new AddExperienceWord(bytes); }
	}

	[PacketLength(Length = 5)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddExperienceDword)]
	public sealed class AddExperienceDword : D2GSPacket
	{
		public AddExperienceDword(byte[] bytes) : base(D2GSIncomingPacketId.AddExperienceDword) { SeedBytes(bytes); }
		public static AddExperienceDword Parse(byte[] bytes) { return new AddExperienceDword(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddMercExperienceByte)]
	public sealed class AddMercExperienceByte : D2GSPacket
	{
		public AddMercExperienceByte(byte[] bytes) : base(D2GSIncomingPacketId.AddMercExperienceByte) { SeedBytes(bytes); }

		public static AddMercExperienceByte Parse(byte[] bytes) { return new AddMercExperienceByte(bytes); }
	}

	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddMercExperienceWord)]
	public sealed class AddMercExperienceWord : D2GSPacket
	{
		public AddMercExperienceWord(byte[] bytes) : base(D2GSIncomingPacketId.AddMercExperienceWord) { SeedBytes(bytes); }

		public static AddMercExperienceWord Parse(byte[] bytes) { return new AddMercExperienceWord(bytes); }
	}
}
