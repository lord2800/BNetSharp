using System;
using System.Collections.Generic;

namespace BNet.BNCS.Outgoing
{
	public sealed class GetChannelList : BNCSPacket
	{
		public GetChannelList() : base(BNCSPacketId.GetChannelList) { Write("D2XP", true); }
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.GetChannelList)]
	public sealed class GetChannelList : BNCSPacket
	{
		private GetChannelList(byte[] bytes) : base(BNCSPacketId.GetChannelList)
		{
			SeedBytes(bytes);

			Channels = new List<string>();
			string s;
			do
			{
				s = Read().Trim();
				if(!String.IsNullOrEmpty(s)) Channels.Add(s);
			} while(!String.IsNullOrEmpty(s));
		}

		public List<string> Channels { get; private set; }
		public static GetChannelList Parse(byte[] bytes) { return new GetChannelList(bytes); }
	}
}
