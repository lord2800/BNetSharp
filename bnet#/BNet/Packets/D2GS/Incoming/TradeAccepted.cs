namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 21)]
	[D2GSPacketHandler(D2GSIncomingPacketId.TradeAccepted)]
	public sealed class TradeAccepted : D2GSPacket
	{
		public TradeAccepted(byte[] bytes) : base(D2GSIncomingPacketId.TradeAccepted) { SeedBytes(bytes); }
		public static TradeAccepted Parse(byte[] bytes) { return new TradeAccepted(bytes); }
	}
}
