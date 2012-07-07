namespace BNet.D2GS.Outgoing
{
	public sealed class RegularChat : D2GSPacket
	{
		public RegularChat(string message) : base(D2GSOutgoingPacketId.Chat)
		{
			Write<ushort>(1);
			Write(message);
			Write<ushort>(0);
		}
	}

	public sealed class OverheadChat : D2GSPacket
	{
		public OverheadChat(string message) : base(D2GSOutgoingPacketId.OverheadChat)
		{
			Write<ushort>(0);
			Write(message);
			Write<ushort>(0);
		}
	}
}
