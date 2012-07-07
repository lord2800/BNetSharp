namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 18)]
	[D2GSPacketHandler(D2GSIncomingPacketId.SetItemState)]
	public sealed class SetItemState : D2GSPacket
	{
		public SetItemState(byte[] bytes) : base(D2GSIncomingPacketId.SetItemState) { SeedBytes(bytes); }
		public static SetItemState Parse(byte[] bytes) { return new SetItemState(bytes); }
	}
}
