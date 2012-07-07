using System;

namespace BNet.BNCS.Outgoing
{
	public sealed class JoinChannel : BNCSPacket
	{
		public JoinChannel(JoinChannelFlags flags, string channel) : base(BNCSPacketId.JoinChannel)
		{
			Write((uint)flags);
			Write(channel);
		}
	}
}

namespace BNet.BNCS
{
	[Flags]
	public enum JoinChannelFlags
	{
		NoCreate = 0,
		FirstJoin = 1,
		ForcedJoin = 2,
		D2FirstJoin = 5
	}
}
