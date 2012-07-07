namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.SetSkill)]
	public sealed class SetSkill : D2GSPacket
	{
		public SetSkill(byte[] bytes) : base(D2GSIncomingPacketId.SetSkill) { SeedBytes(bytes); }
		public static SetSkill Parse(byte[] bytes) { return new SetSkill(bytes); }
	}
}
