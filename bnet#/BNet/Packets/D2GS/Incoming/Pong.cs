namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 33)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Pong)]
	public sealed class Pong : D2GSPacket
	{
		public Pong(byte[] bytes) : base(D2GSIncomingPacketId.Pong)
		{
			SeedBytes(bytes);

			Token = ReadBytes(32);
		}

		public byte[] Token { get; private set; }
		public static Pong Parse(byte[] bytes) { return new Pong(bytes); }
	}
}
