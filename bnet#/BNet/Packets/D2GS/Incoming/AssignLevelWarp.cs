namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignLevelWarp)]
	public sealed class AssignLevelWarp : D2GSPacket
	{
		public AssignLevelWarp(byte[] bytes) : base(D2GSIncomingPacketId.AssignLevelWarp) { SeedBytes(bytes); }
		public static AssignLevelWarp Parse(byte[] bytes) { return new AssignLevelWarp(bytes); }
	}
}
