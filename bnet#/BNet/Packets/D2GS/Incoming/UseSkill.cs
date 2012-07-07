namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UseSkillOnTarget)]
	public sealed class UseSkillOnTarget : D2GSPacket
	{
		public UseSkillOnTarget(byte[] bytes) : base(D2GSIncomingPacketId.UseSkillOnTarget) { SeedBytes(bytes); }
		public static UseSkillOnTarget Parse(byte[] bytes) { return new UseSkillOnTarget(bytes); }
	}

	[PacketLength(Length = 17)]
	[D2GSPacketHandler(D2GSIncomingPacketId.UseSkillOnPoint)]
	public sealed class UseSkillOnPoint : D2GSPacket
	{
		public UseSkillOnPoint(byte[] bytes) : base(D2GSIncomingPacketId.UseSkillOnPoint) { SeedBytes(bytes); }
		public static UseSkillOnPoint Parse(byte[] bytes) { return new UseSkillOnPoint(bytes); }
	}
}
