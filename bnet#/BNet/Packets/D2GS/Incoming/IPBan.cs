namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 5)]
	[D2GSPacketHandler(D2GSIncomingPacketId.IPBan)]
	public sealed class IPBan : D2GSPacket
	{
		public IPBan(byte[] bytes) : base(D2GSIncomingPacketId.IPBan) { SeedBytes(bytes); }
		public static IPBan Parse(byte[] bytes) { return new IPBan(bytes); }
	}
}
