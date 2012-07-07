namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AttributeUpdate)]
	public sealed class AttributeUpdate : D2GSPacket
	{
		public AttributeUpdate(byte[] bytes) : base(D2GSIncomingPacketId.AttributeUpdate) { SeedBytes(bytes); }
		public static AttributeUpdate Parse(byte[] bytes) { return new AttributeUpdate(bytes); }
	}
}
