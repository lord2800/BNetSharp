namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerCorpseAssign)]
	public sealed class PlayerCorpseAssign : D2GSPacket
	{
		public PlayerCorpseAssign(byte[] bytes) : base(D2GSIncomingPacketId.PlayerCorpseAssign) { SeedBytes(bytes); }
		public static PlayerCorpseAssign Parse(byte[] bytes) { return new PlayerCorpseAssign(bytes); }
	}

	[PacketLength(Length = 36)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerInGame)]
	public sealed class PlayerInGame : D2GSPacket
	{
		public PlayerInGame(byte[] bytes) : base(D2GSIncomingPacketId.PlayerInGame) { SeedBytes(bytes); }
		public static PlayerInGame Parse(byte[] bytes) { return new PlayerInGame(bytes); }
	}

	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerInProximity)]
	public sealed class PlayerInProximity : D2GSPacket
	{
		public PlayerInProximity(byte[] bytes) : base(D2GSIncomingPacketId.PlayerInProximity) { SeedBytes(bytes); }
		public static PlayerInProximity Parse(byte[] bytes) { return new PlayerInProximity(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerKillCount)]
	public sealed class PlayerKillCount : D2GSPacket
	{
		public PlayerKillCount(byte[] bytes) : base(D2GSIncomingPacketId.PlayerKillCount) { SeedBytes(bytes); }
		public static PlayerKillCount Parse(byte[] bytes) { return new PlayerKillCount(bytes); }
	}

	[PacketLength(Length = 5)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerLeftGame)]
	public sealed class PlayerLeftGame : D2GSPacket
	{
		public PlayerLeftGame(byte[] bytes) : base(D2GSIncomingPacketId.PlayerLeftGame) { SeedBytes(bytes); }
		public static PlayerLeftGame Parse(byte[] bytes) { return new PlayerLeftGame(bytes); }
	}

	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerMove)]
	public sealed class PlayerMove : D2GSPacket
	{
		public PlayerMove(byte[] bytes) : base(D2GSIncomingPacketId.PlayerMove) { SeedBytes(bytes); }
		public static PlayerMove Parse(byte[] bytes) { return new PlayerMove(bytes); }
	}

	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerPartyInfo)]
	public sealed class PlayerPartyInfo : D2GSPacket
	{
		public PlayerPartyInfo(byte[] bytes) : base(D2GSIncomingPacketId.PlayerPartyInfo) { SeedBytes(bytes); }
		public static PlayerPartyInfo Parse(byte[] bytes) { return new PlayerPartyInfo(bytes); }
	}

	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerRelationship)]
	public sealed class PlayerRelationship : D2GSPacket
	{
		public PlayerRelationship(byte[] bytes) : base(D2GSIncomingPacketId.PlayerRelationship) { SeedBytes(bytes); }
		public static PlayerRelationship Parse(byte[] bytes) { return new PlayerRelationship(bytes); }
	}

	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerSlotRefresh)]
	public sealed class PlayerSlotRefresh : D2GSPacket
	{
		public PlayerSlotRefresh(byte[] bytes) : base(D2GSIncomingPacketId.PlayerSlotRefresh) { SeedBytes(bytes); }
		public static PlayerSlotRefresh Parse(byte[] bytes) { return new PlayerSlotRefresh(bytes); }
	}

	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerStop)]
	public sealed class PlayerStop : D2GSPacket
	{
		public PlayerStop(byte[] bytes) : base(D2GSIncomingPacketId.PlayerStop) { SeedBytes(bytes); }
		public static PlayerStop Parse(byte[] bytes) { return new PlayerStop(bytes); }
	}

	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlayerToTarget)]
	public sealed class PlayerToTarget : D2GSPacket
	{
		public PlayerToTarget(byte[] bytes) : base(D2GSIncomingPacketId.PlayerToTarget) { SeedBytes(bytes); }
		public static PlayerToTarget Parse(byte[] bytes) { return new PlayerToTarget(bytes); }
	}
}
