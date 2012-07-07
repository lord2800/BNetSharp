namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AllyPartyInfo)]
	public sealed class AllyPartyInfo : D2GSPacket
	{
		public AllyPartyInfo(byte[] bytes) : base(D2GSIncomingPacketId.AllyPartyInfo) { SeedBytes(bytes); }
		public static AllyPartyInfo Parse(byte[] bytes) { return new AllyPartyInfo(bytes); }
	}
}
