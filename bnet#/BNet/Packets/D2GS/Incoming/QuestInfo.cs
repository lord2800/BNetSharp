namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 103)]
	[D2GSPacketHandler(D2GSIncomingPacketId.QuestInfo)]
	public sealed class QuestInfo : D2GSPacket
	{
		public QuestInfo(byte[] bytes) : base(D2GSIncomingPacketId.QuestInfo) { SeedBytes(bytes); }
		public static QuestInfo Parse(byte[] bytes) { return new QuestInfo(bytes); }
	}
}
