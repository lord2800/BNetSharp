namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.EndState)]
	public sealed class EndState : D2GSPacket
	{
		public EndState(byte[] bytes) : base(D2GSIncomingPacketId.EndState) { SeedBytes(bytes); }
		public static EndState Parse(byte[] bytes) { return new EndState(bytes); }
	}
}
