namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 5)]
	[D2GSPacketHandler(D2GSIncomingPacketId.QuestItemState)]
	public sealed class QuestItemState : D2GSPacket
	{
		public QuestItemState(byte[] bytes) : base(D2GSIncomingPacketId.QuestItemState) { SeedBytes(bytes); }
		public static QuestItemState Parse(byte[] bytes) { return new QuestItemState(bytes); }
	}
}
