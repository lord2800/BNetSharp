namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.MapHide)]
	public sealed class MapHide : D2GSPacket
	{
		public MapHide(byte[] bytes) : base(D2GSIncomingPacketId.MapHide) { SeedBytes(bytes); }
		public static MapHide Parse(byte[] bytes) { return new MapHide(bytes); }
	}
}
