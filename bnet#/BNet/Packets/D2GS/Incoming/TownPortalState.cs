namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.TownPortalState)]
	public sealed class TownPortalState : D2GSPacket
	{
		public TownPortalState(byte[] bytes) : base(D2GSIncomingPacketId.TownPortalState) { SeedBytes(bytes); }
		public static TownPortalState Parse(byte[] bytes) { return new TownPortalState(bytes); }
	}
}
