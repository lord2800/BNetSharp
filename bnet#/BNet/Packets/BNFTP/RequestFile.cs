using System;

namespace BNet.BNFTP.Outgoing
{
	internal sealed class RequestFile : BNFTPv1Packet
	{
		public RequestFile(string filename, DateTime filetime)
		{
			Write("IX86", true);
			Write("D2DV", true);
			Write<uint>(0);
			Write<uint>(0);
			Write<uint>(0);
			Write(filetime.Ticks);
			Write(filename);
		}
	}
}
