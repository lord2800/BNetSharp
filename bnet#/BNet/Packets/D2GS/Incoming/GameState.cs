namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GameLoading)]
	public sealed class GameLoading : D2GSPacket
	{
		public GameLoading(byte[] bytes) : base(D2GSIncomingPacketId.GameLoading) { SeedBytes(bytes); }
		public static GameLoading Parse(byte[] bytes) { return new GameLoading(bytes); }
	}

	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GameFlags)]
	public sealed class GameFlags : D2GSPacket
	{
		public GameFlags(byte[] bytes) : base(D2GSIncomingPacketId.GameFlags)
		{
			SeedBytes(bytes);

			Difficulty = Read<byte>();
			Unknown = Read<ushort>();
			Hardcore = Read<ushort>();
			Expansion = Read<byte>();
			Ladder = Read<byte>();
		}

		public byte Difficulty { get; private set; }
		public ushort Unknown { get; private set; }
		public ushort Hardcore { get; private set; }
		public byte Expansion { get; private set; }
		public byte Ladder { get; private set; }
		public static GameFlags Parse(byte[] bytes) { return new GameFlags(bytes); }
	}

	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GameHandshake)]
	public sealed class GameHandshake : D2GSPacket
	{
		public GameHandshake(byte[] bytes) : base(D2GSIncomingPacketId.GameHandshake) { SeedBytes(bytes); }

		public static GameHandshake Parse(byte[] bytes) { return new GameHandshake(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.LoadSuccessful)]
	public sealed class GameLoaded : D2GSPacket
	{
		public GameLoaded(byte[] bytes) : base(D2GSIncomingPacketId.LoadSuccessful) { SeedBytes(bytes); }
		public static GameLoaded Parse(byte[] bytes) { return new GameLoaded(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GameExitSuccess)]
	public sealed class GameExitSuccess : D2GSPacket
	{
		public GameExitSuccess(byte[] bytes) : base(D2GSIncomingPacketId.GameExitSuccess) { SeedBytes(bytes); }
		public static GameExitSuccess Parse(byte[] bytes) { return new GameExitSuccess(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GameDropped)]
	public sealed class GameDropped : D2GSPacket
	{
		public GameDropped(byte[] bytes) : base(D2GSIncomingPacketId.GameDropped) { SeedBytes(bytes); }
		public static GameDropped Parse(byte[] bytes) { return new GameDropped(bytes); }
	}

	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.LoadAct)]
	public sealed class LoadAct : D2GSPacket
	{
		public LoadAct(byte[] bytes) : base(D2GSIncomingPacketId.LoadAct) { SeedBytes(bytes); }
		public static LoadAct Parse(byte[] bytes) { return new LoadAct(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.LoadActComplete)]
	public sealed class LoadActComplete : D2GSPacket
	{
		public LoadActComplete(byte[] bytes) : base(D2GSIncomingPacketId.LoadActComplete) { SeedBytes(bytes); }
		public static LoadActComplete Parse(byte[] bytes) { return new LoadActComplete(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UnloadActComplete)]
	public sealed class UnloadActComplete : D2GSPacket
	{
		public UnloadActComplete(byte[] bytes) : base(D2GSIncomingPacketId.UnloadActComplete) { SeedBytes(bytes); }
		public static UnloadActComplete Parse(byte[] bytes) { return new UnloadActComplete(bytes); }
	}
}
