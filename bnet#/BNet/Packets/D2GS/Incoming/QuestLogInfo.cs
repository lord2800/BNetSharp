namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 42)]
	[D2GSPacketHandler(D2GSIncomingPacketId.QuestLogInfo)]
	public sealed class QuestLogInfo : D2GSPacket
	{
		public QuestLogInfo(byte[] bytes) : base(D2GSIncomingPacketId.QuestLogInfo) { SeedBytes(bytes); }
		public static QuestLogInfo Parse(byte[] bytes) { return new QuestLogInfo(bytes); }
	}
}
