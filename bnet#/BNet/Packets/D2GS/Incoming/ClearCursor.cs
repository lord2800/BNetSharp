namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ClearCursor)]
	public sealed class ClearCursor : D2GSPacket
	{
		public ClearCursor(byte[] bytes) : base(D2GSIncomingPacketId.ClearCursor) { SeedBytes(bytes); }
		public static ClearCursor Parse(byte[] bytes) { return new ClearCursor(bytes); }
	}
}
