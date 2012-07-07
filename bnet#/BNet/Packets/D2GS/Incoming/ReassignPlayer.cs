namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 11)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ReassignPlayer)]
	public sealed class ReassignPlayer : D2GSPacket
	{
		public ReassignPlayer(byte[] bytes) : base(D2GSIncomingPacketId.ReassignPlayer) { SeedBytes(bytes); }
		public static ReassignPlayer Parse(byte[] bytes) { return new ReassignPlayer(bytes); }
	}
}
