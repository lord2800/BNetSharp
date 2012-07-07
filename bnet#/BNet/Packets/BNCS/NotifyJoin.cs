namespace BNet.BNCS.Outgoing
{
	public sealed class NotifyJoin : BNCSPacket
	{
		public NotifyJoin(string name, string password, byte version) : base(BNCSPacketId.NotifyJoin)
		{
			Write("D2XP", true);
			Write<uint>(version);
			Write(name);
			Write(password);
		}
	}

	public sealed class LeaveChat : BNCSPacket
	{
		public LeaveChat() : base(BNCSPacketId.LeaveChat) { }
	}
}
