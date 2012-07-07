namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignPlayerToParty)]
	public sealed class AssignPlayerToParty : D2GSPacket
	{
		public AssignPlayerToParty(byte[] bytes) : base(D2GSIncomingPacketId.AssignPlayerToParty) { SeedBytes(bytes); }
		public static AssignPlayerToParty Parse(byte[] bytes) { return new AssignPlayerToParty(bytes); }
	}
}
