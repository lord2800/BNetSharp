using System;
using System.Globalization;
using System.Net;

namespace BNet.BNCS.Outgoing
{
	public sealed class AuthInfo : BNCSPacket
	{
		public AuthInfo(IPAddress local, uint version, CultureInfo ci, RegionInfo ri) : base(BNCSPacketId.AuthInfo)
		{
			Write<uint>(0);
			Write("IX86", true);
			Write("D2XP", true);
			Write(version);
			Write(ci.TwoLetterISOLanguageName + ri.TwoLetterISORegionName, true);
			WriteBytes(local.GetAddressBytes());
			Write((int)(DateTime.UtcNow - DateTime.Now).TotalMinutes);
			Write((uint)CultureInfo.CurrentCulture.LCID);
			Write((uint)CultureInfo.CurrentCulture.LCID);
			Write(ri.ThreeLetterWindowsRegionName);
			Write(ri.DisplayName);
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.AuthInfo)]
	public sealed class AuthInfo : BNCSPacket
	{
		private AuthInfo(byte[] bytes) : base(BNCSPacketId.AuthInfo)
		{
			SeedBytes(bytes);

			LogonType = Read<uint>();
			ServerToken = Read<uint>();
			UDPValue = Read<uint>();
			FileTime = DateTime.FromFileTimeUtc(Read<Int64>());
			FileName = Read().Trim();
			Formula = Read().Trim();
		}

		public uint LogonType { get; private set; }
		public uint ServerToken { get; private set; }
		public uint UDPValue { get; private set; }
		public DateTime FileTime { get; private set; }
		public string FileName { get; private set; }
		public string Formula { get; private set; }
		public static AuthInfo Parse(byte[] bytes) { return new AuthInfo(bytes); }
	}
}
