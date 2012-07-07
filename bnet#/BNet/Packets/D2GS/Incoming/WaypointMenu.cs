namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 21)]
	[D2GSPacketHandler(D2GSIncomingPacketId.WaypointMenu)]
	public sealed class WaypointMenu : D2GSPacket
	{
		public WaypointMenu(byte[] bytes) : base(D2GSIncomingPacketId.WaypointMenu) { SeedBytes(bytes); }
		public static WaypointMenu Parse(byte[] bytes) { return new WaypointMenu(bytes); }
	}
}
