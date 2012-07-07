namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 14)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignObject)]
	public sealed class AssignObject : D2GSPacket
	{
		public AssignObject(byte[] bytes) : base(D2GSIncomingPacketId.AssignObject) { SeedBytes(bytes); }
		public static AssignObject Parse(byte[] bytes) { return new AssignObject(bytes); }
	}
}
