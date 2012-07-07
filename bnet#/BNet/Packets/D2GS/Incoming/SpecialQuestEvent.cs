namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 2)]
	[D2GSPacketHandler(D2GSIncomingPacketId.SpecialQuestEvent)]
	public sealed class SpecialQuestEvent : D2GSPacket
	{
		public SpecialQuestEvent(byte[] bytes) : base(D2GSIncomingPacketId.SpecialQuestEvent) { SeedBytes(bytes); }
		public static SpecialQuestEvent Parse(byte[] bytes) { return new SpecialQuestEvent(bytes); }
	}
}
