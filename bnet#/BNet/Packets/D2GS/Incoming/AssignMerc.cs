namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 20)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignMerc)]
	public sealed class AssignMerc : D2GSPacket
	{
		public AssignMerc(byte[] bytes) : base(D2GSIncomingPacketId.AssignMerc) { SeedBytes(bytes); }
		public static AssignMerc Parse(byte[] bytes) { return new AssignMerc(bytes); }
	}
}
