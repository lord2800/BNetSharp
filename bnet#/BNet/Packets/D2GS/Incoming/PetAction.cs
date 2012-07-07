namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.PetAction)]
	public sealed class PetAction : D2GSPacket
	{
		public PetAction(byte[] bytes) : base(D2GSIncomingPacketId.PetAction) { SeedBytes(bytes); }
		public static PetAction Parse(byte[] bytes) { return new PetAction(bytes); }
	}
}
