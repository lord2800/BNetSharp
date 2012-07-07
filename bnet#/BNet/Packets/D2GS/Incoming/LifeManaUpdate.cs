namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.LifeManaUpdate)]
	public sealed class LifeManaUpdate : D2GSPacket
	{
		public LifeManaUpdate(byte[] bytes) : base(D2GSIncomingPacketId.LifeManaUpdate) { SeedBytes(bytes); }
		public static LifeManaUpdate Parse(byte[] bytes) { return new LifeManaUpdate(bytes); }
	}
}
