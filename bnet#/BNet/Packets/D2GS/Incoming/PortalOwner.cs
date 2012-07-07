namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 29)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PortalOwner)]
	public sealed class PortalOwner : D2GSPacket
	{
		public PortalOwner(byte[] bytes) : base(D2GSIncomingPacketId.PortalOwner) { SeedBytes(bytes); }
		public static PortalOwner Parse(byte[] bytes) { return new PortalOwner(bytes); }
	}
}
