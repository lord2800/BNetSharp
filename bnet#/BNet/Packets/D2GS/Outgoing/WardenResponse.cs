namespace BNet.D2GS.Outgoing
{
	public sealed class WardenResponse : D2GSPacket
	{
		public WardenResponse(byte[] data) : base(D2GSOutgoingPacketId.WardenResponse)
		{
			Write((byte)data.Length);
			Write<byte>(0);
			WriteBytes(data);
		}
	}
}
