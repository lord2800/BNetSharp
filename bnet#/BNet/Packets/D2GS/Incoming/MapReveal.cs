namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.MapReveal)]
	public sealed class MapReveal : D2GSPacket
	{
		public MapReveal(byte[] bytes) : base(D2GSIncomingPacketId.MapReveal) { SeedBytes(bytes); }
		public static MapReveal Parse(byte[] bytes) { return new MapReveal(bytes); }
	}
}
