namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignHotkey)]
	public sealed class AssignHotkey : D2GSPacket
	{
		public AssignHotkey(byte[] bytes) : base(D2GSIncomingPacketId.AssignHotkey) { SeedBytes(bytes); }
		public static AssignHotkey Parse(byte[] bytes) { return new AssignHotkey(bytes); }
	}
}
