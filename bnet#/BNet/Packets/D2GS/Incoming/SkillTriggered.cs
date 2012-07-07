namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 16)]
	[D2GSPacketHandler(D2GSIncomingPacketId.SkillTriggered)]
	public sealed class SkillTriggered : D2GSPacket
	{
		public SkillTriggered(byte[] bytes) : base(D2GSIncomingPacketId.SkillTriggered) { SeedBytes(bytes); }
		public static SkillTriggered Parse(byte[] bytes) { return new SkillTriggered(bytes); }
	}
}
