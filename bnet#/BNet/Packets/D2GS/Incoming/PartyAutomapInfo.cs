namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PartyAutomapInfo)]
	public sealed class PartyAutomapInfo : D2GSPacket
	{
		public PartyAutomapInfo(byte[] bytes) : base(D2GSIncomingPacketId.PartyAutomapInfo) { SeedBytes(bytes); }
		public static PartyAutomapInfo Parse(byte[] bytes) { return new PartyAutomapInfo(bytes); }
	}
}
