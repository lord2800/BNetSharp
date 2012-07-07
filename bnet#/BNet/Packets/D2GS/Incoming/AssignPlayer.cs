namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 26)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignPlayer)]
	public sealed class AssignPlayer : D2GSPacket
	{
		public AssignPlayer(byte[] bytes) : base(D2GSIncomingPacketId.AssignPlayer) { SeedBytes(bytes); }
		public static AssignPlayer Parse(byte[] bytes) { return new AssignPlayer(bytes); }
	}
}
