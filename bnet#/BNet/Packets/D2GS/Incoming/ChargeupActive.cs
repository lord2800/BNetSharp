namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ChargeupActive)]
	public sealed class ChargeupActive : D2GSPacket
	{
		public ChargeupActive(byte[] bytes) : base(D2GSIncomingPacketId.ChargeupActive) { SeedBytes(bytes); }
		public static ChargeupActive Parse(byte[] bytes) { return new ChargeupActive(bytes); }
	}
}
