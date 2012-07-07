using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;

namespace BNet.BNCS.Outgoing
{
	public sealed class AuthCheck : BNCSPacket
	{
		[SecurityPermission(SecurityAction.LinkDemand)]
		public AuthCheck(BattleNetCredential credentials, uint clientToken, uint serverToken, string formula, string mpq,
		                 string game, string bnet, string d2) : base(BNCSPacketId.AuthCheck)
		{
			var fi = new FileInfo(game);
			var fileInfo = "{0} {1:MM/dd/yy hh:mm:ss} {2}".Compose(fi.Name, fi.LastWriteTimeUtc, fi.Length);

			var fvi = FileVersionInfo.GetVersionInfo(game);
			var exeversion = ((fvi.FileMajorPart << 24) | (fvi.FileMinorPart << 16) | (fvi.FileBuildPart << 8) | 0);

			var checkrevision = CheckRevision.FastComputeHash(formula, mpq, game, bnet, d2);


			Write(clientToken);
			Write(exeversion);
			Write(checkrevision);
			Write(credentials.CdKeys.Count);
			Write<uint>(0);
#if PVPGN
			for(int i = 0; i < credentials.CdKeyCount; i++)
			{
				Write<uint>(16);
				Write<uint>(0);
				Write<uint>(0);
				Write<uint>(0);
				WriteBytes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
			}
#else
			foreach(var key in credentials.CdKeys)
			{
				Write(key.Key.Length);
				Write((int)key.Product);
				Write(key.Public);
				Write<uint>(0);
				WriteBytes(key.GetHash(clientToken, serverToken));
			}
#endif
			Write(fileInfo);
			Write(credentials.OwnerName);
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.AuthCheck)]
	public sealed class AuthCheck : BNCSPacket
	{
		private AuthCheck(byte[] bytes) : base(BNCSPacketId.AuthCheck)
		{
			SeedBytes(bytes);

			Response = (AuthCheckResponse)Read<uint>();
			Reason = Read();
		}

		public AuthCheckResponse Response { get; private set; }
		public string Reason { get; private set; }
		public static AuthCheck Parse(byte[] bytes) { return new AuthCheck(bytes); }
	}
}

namespace BNet.BNCS
{
	public enum AuthCheckResponse : uint
	{
		Passed = 0x000,
		OldVersion = 0x100,
		InvalidVersion = 0x101,
		NewVersion = 0x102,
		ClassicKeyInvalid = 0x200,
		ClassicKeyInUse = 0x201,
		ClassicKeyBanned = 0x202,
		ClassicKeyWrongProduct = 0x203,
		XPacKeyInvalid = 0x210,
		XPacKeyInUse = 0x211,
		XPacKeyBanned = 0x212,
		XPacKeyWrongProduct = 0x213
	}
}
