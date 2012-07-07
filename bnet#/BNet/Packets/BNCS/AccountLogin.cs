namespace BNet.BNCS.Outgoing
{
	public sealed class AccountLogin : BNCSPacket
	{
		public AccountLogin(BattleNetCredential credentials, uint clientToken, uint serverToken) : base(BNCSPacketId.LogonResponse2)
		{
			Write(clientToken);
			Write(serverToken);
			WriteBytes(credentials.GetPasswordHash(clientToken, serverToken));
			Write(credentials.UserName);
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.LogonResponse2)]
	public sealed class AccountLogin : BNCSPacket
	{
		private AccountLogin(byte[] bytes) : base(BNCSPacketId.LogonResponse2)
		{
			SeedBytes(bytes);

			Response = (LogonResponse)Read<uint>();
			Reason = Read();
		}

		public LogonResponse Response { get; private set; }
		public string Reason { get; private set; }
		public static AccountLogin Parse(byte[] bytes) { return new AccountLogin(bytes); }
	}
}

namespace BNet.BNCS
{
	public enum LogonResponse : uint
	{
		Success = 0x00,
		AccountDoesNotExist = 0x01,
		InvalidPassword = 0x02,
		AccountClosed = 0x06
	}
}
