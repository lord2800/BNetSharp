namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.WeaponSwitch)]
	public sealed class WeaponSwitch : D2GSPacket
	{
		public WeaponSwitch(byte[] bytes) : base(D2GSIncomingPacketId.WeaponSwitch) { SeedBytes(bytes); }
		public static WeaponSwitch Parse(byte[] bytes) { return new WeaponSwitch(bytes); }
	}
}
