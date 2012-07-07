namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.StartMercList)]
	public sealed class StartMercList : D2GSPacket
	{
		public StartMercList(byte[] bytes) : base(D2GSIncomingPacketId.StartMercList) { SeedBytes(bytes); }
		public static StartMercList Parse(byte[] bytes) { return new StartMercList(bytes); }
	}
}
