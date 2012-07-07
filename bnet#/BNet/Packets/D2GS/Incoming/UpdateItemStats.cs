namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 34)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UpdateItemStats)]
	public sealed class UpdateItemStats : D2GSPacket
	{
		public UpdateItemStats(byte[] bytes) : base(D2GSIncomingPacketId.UpdateItemStats) { SeedBytes(bytes); }
		public static UpdateItemStats Parse(byte[] bytes) { return new UpdateItemStats(bytes); }
	}
}
