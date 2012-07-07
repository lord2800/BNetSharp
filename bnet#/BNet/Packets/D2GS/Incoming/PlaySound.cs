namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PlaySound)]
	public sealed class PlaySound : D2GSPacket
	{
		public PlaySound(byte[] bytes) : base(D2GSIncomingPacketId.PlaySound) { SeedBytes(bytes); }
		public static PlaySound Parse(byte[] bytes) { return new PlaySound(bytes); }
	}
}
