namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ReportKill)]
	public sealed class ReportKill : D2GSPacket
	{
		public ReportKill(byte[] bytes) : base(D2GSIncomingPacketId.ReportKill) { SeedBytes(bytes); }
		public static ReportKill Parse(byte[] bytes) { return new ReportKill(bytes); }
	}
}
