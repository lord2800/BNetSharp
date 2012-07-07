namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.CorpseAssign)]
	public sealed class CorpseAssign : D2GSPacket
	{
		public CorpseAssign(byte[] bytes) : base(D2GSIncomingPacketId.CorpseAssign) { SeedBytes(bytes); }
		public static CorpseAssign Parse(byte[] bytes) { return new CorpseAssign(bytes); }
	}
}
