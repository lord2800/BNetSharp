namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.DelayedState)]
	public sealed class DelayedState : D2GSPacket
	{
		public DelayedState(byte[] bytes) : base(D2GSIncomingPacketId.DelayedState) { SeedBytes(bytes); }
		public static DelayedState Parse(byte[] bytes) { return new DelayedState(bytes); }
	}
}
