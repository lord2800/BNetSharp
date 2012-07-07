namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 2)]
	[D2GSPacketHandler(D2GSIncomingPacketId.GoldToInventory)]
	public sealed class GoldToInventory : D2GSPacket
	{
		public GoldToInventory(byte[] bytes) : base(D2GSIncomingPacketId.GoldToInventory) { SeedBytes(bytes); }
		public static GoldToInventory Parse(byte[] bytes) { return new GoldToInventory(bytes); }
	}
}
