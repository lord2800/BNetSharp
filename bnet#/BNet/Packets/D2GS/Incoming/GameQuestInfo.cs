namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 97)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GameQuestInfo)]
	public sealed class GameQuestInfo : D2GSPacket
	{
		public GameQuestInfo(byte[] bytes) : base(D2GSIncomingPacketId.GameQuestInfo) { SeedBytes(bytes); }
		public static GameQuestInfo Parse(byte[] bytes) { return new GameQuestInfo(bytes); }
	}
}
