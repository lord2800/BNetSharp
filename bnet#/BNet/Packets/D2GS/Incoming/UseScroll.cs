namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UseScroll)]
	public sealed class UseScroll : D2GSPacket
	{
		public UseScroll(byte[] bytes) : base(D2GSIncomingPacketId.UseScroll) { SeedBytes(bytes); }
		public static UseScroll Parse(byte[] bytes) { return new UseScroll(bytes); }
	}
}
