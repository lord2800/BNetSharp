namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 2)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ButtonAction)]
	public sealed class ButtonAction : D2GSPacket
	{
		public ButtonAction(byte[] bytes) : base(D2GSIncomingPacketId.ButtonAction) { SeedBytes(bytes); }
		public static ButtonAction Parse(byte[] bytes) { return new ButtonAction(bytes); }
	}
}
