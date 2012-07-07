namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ObjectState)]
	public sealed class ObjectState : D2GSPacket
	{
		public ObjectState(byte[] bytes) : base(D2GSIncomingPacketId.ObjectState) { SeedBytes(bytes); }
		public static ObjectState Parse(byte[] bytes) { return new ObjectState(bytes); }
	}
}
