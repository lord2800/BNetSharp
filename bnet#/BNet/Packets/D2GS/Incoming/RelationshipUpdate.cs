namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 11)]
	[D2GSPacketHandler(D2GSIncomingPacketId.RelationshipUpdate)]
	public sealed class RelationshipUpdate : D2GSPacket
	{
		public RelationshipUpdate(byte[] bytes) : base(D2GSIncomingPacketId.RelationshipUpdate) { SeedBytes(bytes); }
		public static RelationshipUpdate Parse(byte[] bytes) { return new RelationshipUpdate(bytes); }
	}
}
