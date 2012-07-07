namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UseStackableItem)]
	public sealed class UseStackableItem : D2GSPacket
	{
		public UseStackableItem(byte[] bytes) : base(D2GSIncomingPacketId.UseStackableItem) { SeedBytes(bytes); }
		public static UseStackableItem Parse(byte[] bytes) { return new UseStackableItem(bytes); }
	}
}
