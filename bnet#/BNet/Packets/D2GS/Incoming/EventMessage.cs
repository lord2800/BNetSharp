namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 40)]
	[D2GSPacketHandler(D2GSIncomingPacketId.EventMessage)]
	public sealed class EventMessage : D2GSPacket
	{
		public EventMessage(byte[] bytes) : base(D2GSIncomingPacketId.EventMessage) { SeedBytes(bytes); }
		public static EventMessage Parse(byte[] bytes) { return new EventMessage(bytes); }
	}
}
