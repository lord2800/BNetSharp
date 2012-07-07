using System;

namespace BNet.BNCS.Outgoing
{
	public sealed class DisplayAd : BNCSPacket
	{
		public DisplayAd(uint adID, string filename, Uri url) : base(BNCSPacketId.DisplayAd)
		{
			Write("IX86", true);
			Write("D2XP", true);
			Write(adID);
			Write(filename);
			Write(url.OriginalString);
		}
	}
}
