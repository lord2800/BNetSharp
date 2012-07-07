namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UpdateItemOSkill)]
	public sealed class UpdateItemOSkill : D2GSPacket
	{
		public UpdateItemOSkill(byte[] bytes) : base(D2GSIncomingPacketId.UpdateItemOSkill) { SeedBytes(bytes); }
		public static UpdateItemOSkill Parse(byte[] bytes) { return new UpdateItemOSkill(bytes); }
	}

	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UpdateItemSkill)]
	public sealed class UpdateItemSkill : D2GSPacket
	{
		public UpdateItemSkill(byte[] bytes) : base(D2GSIncomingPacketId.UpdateItemSkill) { SeedBytes(bytes); }
		public static UpdateItemSkill Parse(byte[] bytes) { return new UpdateItemSkill(bytes); }
	}
}
