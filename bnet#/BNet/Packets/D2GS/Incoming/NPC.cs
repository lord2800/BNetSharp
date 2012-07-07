namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCAction)]
	public sealed class NPCAction : D2GSPacket
	{
		public NPCAction(byte[] bytes) : base(D2GSIncomingPacketId.NPCAction) { SeedBytes(bytes); }
		public static NPCAction Parse(byte[] bytes) { return new NPCAction(bytes); }
	}

	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCAttack)]
	public sealed class NPCAttack : D2GSPacket
	{
		public NPCAttack(byte[] bytes) : base(D2GSIncomingPacketId.NPCAttack) { SeedBytes(bytes); }
		public static NPCAttack Parse(byte[] bytes) { return new NPCAttack(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCHeal)]
	public sealed class NPCHeal : D2GSPacket
	{
		public NPCHeal(byte[] bytes) : base(D2GSIncomingPacketId.NPCHeal) { SeedBytes(bytes); }
		public static NPCHeal Parse(byte[] bytes) { return new NPCHeal(bytes); }
	}

	[PacketLength(Length = 9)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCHit)]
	public sealed class NPCHit : D2GSPacket
	{
		public NPCHit(byte[] bytes) : base(D2GSIncomingPacketId.NPCHit) { SeedBytes(bytes); }
		public static NPCHit Parse(byte[] bytes) { return new NPCHit(bytes); }
	}

	[PacketLength(Length = 40)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCInfo)]
	public sealed class NPCInfo : D2GSPacket
	{
		public NPCInfo(byte[] bytes) : base(D2GSIncomingPacketId.NPCInfo) { SeedBytes(bytes); }
		public static NPCInfo Parse(byte[] bytes) { return new NPCInfo(bytes); }
	}

	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCMove)]
	public sealed class NPCMove : D2GSPacket
	{
		public NPCMove(byte[] bytes) : base(D2GSIncomingPacketId.NPCMove) { SeedBytes(bytes); }
		public static NPCMove Parse(byte[] bytes) { return new NPCMove(bytes); }
	}

	[PacketLength(Length = 21)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCMoveToTarget)]
	public sealed class NPCMoveToTarget : D2GSPacket
	{
		public NPCMoveToTarget(byte[] bytes) : base(D2GSIncomingPacketId.NPCMoveToTarget) { SeedBytes(bytes); }
		public static NPCMoveToTarget Parse(byte[] bytes) { return new NPCMoveToTarget(bytes); }
	}

	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCState)]
	public sealed class NPCState : D2GSPacket
	{
		public NPCState(byte[] bytes) : base(D2GSIncomingPacketId.NPCState) { SeedBytes(bytes); }
		public static NPCState Parse(byte[] bytes) { return new NPCState(bytes); }
	}

	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCStop)]
	public sealed class NPCStop : D2GSPacket
	{
		public NPCStop(byte[] bytes) : base(D2GSIncomingPacketId.NPCStop) { SeedBytes(bytes); }
		public static NPCStop Parse(byte[] bytes) { return new NPCStop(bytes); }
	}

	[PacketLength(Length = 15)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCTransaction)]
	public sealed class NPCTransaction : D2GSPacket
	{
		public NPCTransaction(byte[] bytes) : base(D2GSIncomingPacketId.NPCTransaction) { SeedBytes(bytes); }
		public static NPCTransaction Parse(byte[] bytes) { return new NPCTransaction(bytes); }
	}

	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.NPCWantInteract)]
	public sealed class NPCWantInteract : D2GSPacket
	{
		public NPCWantInteract(byte[] bytes) : base(D2GSIncomingPacketId.NPCWantInteract) { SeedBytes(bytes); }
		public static NPCWantInteract Parse(byte[] bytes) { return new NPCWantInteract(bytes); }
	}
}
