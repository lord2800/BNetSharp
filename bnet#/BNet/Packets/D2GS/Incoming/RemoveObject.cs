namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.RemoveObject)]
	public sealed class RemoveObject : D2GSPacket
	{
		public RemoveObject(byte[] bytes) : base(D2GSIncomingPacketId.RemoveObject) { SeedBytes(bytes); }
		public static RemoveObject Parse(byte[] bytes) { return new RemoveObject(bytes); }
	}
}
