namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GoldInTrade)]
	public sealed class GoldInTrade : D2GSPacket
	{
		public GoldInTrade(byte[] bytes) : base(D2GSIncomingPacketId.GoldInTrade) { SeedBytes(bytes); }
		public static GoldInTrade Parse(byte[] bytes) { return new GoldInTrade(bytes); }
	}
}
