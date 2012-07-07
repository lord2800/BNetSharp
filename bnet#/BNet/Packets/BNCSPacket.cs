namespace BNet.BNCS
{
	using System;
	using System.IO;
	using Utilities;

	internal class BNCSPacketBuilder : PacketBuilder<BNCSPacketHandlerAttribute>
	{
		public override Packet Parse(BufferedStream buff)
		{
			var check = (byte)buff.ReadByte();
			if(check != 0xFF)
				throw new Exception("Invalid check header!");

			var header = (byte)buff.ReadByte();

			var command = (BNCSPacketId)header;
			ProcessType((byte)command);

			var buf = new byte[2];
			buff.Read(buf, 0, 2);
			int size = BitConverter.ToUInt16(buf, 0);
			if(size < 4)
				throw new Exception("Invalid packet size!");

			buf = new byte[size - 4];
			buff.BlockRead(buf);
			return processors[(byte)command].Value(buf);
		}
	}

	public enum BNCSPacketId : byte
	{
		Null = 0x00,
		StopAdv = 0x02,
		StartAdvEx = 0x08,
		GetAdvListEx = 0x09,
		EnterChat = 0x0a,
		GetChannelList = 0x0b,
		JoinChannel = 0x0c,
		ChatCommand = 0x0e,
		ChatEvent = 0x0f,
		LeaveChat = 0x10,
		FloodDetected = 0x13,
		UdpPingResponse = 0x14,
		CheckAd = 0x15,
		ClickAd = 0x16,
		MessageBox = 0x19,
		StartAdvEx3 = 0x1c,
		LoginChallengeEx = 0x1d,
		LeaveGame = 0x1f,
		DisplayAd = 0x21,
		NotifyJoin = 0x22,
		Ping = 0x25,
		ReadUserData = 0x26,
		WriteUserData = 0x27,
		LogonChallenge = 0x28,
		LogonResponse = 0x29,
		CreateAccount = 0x2a,
		GameResult = 0x2c,
		GetIconData = 0x2d,
		GetLadderData = 0x2e,
		FindLadderUser = 0x2f,
		CdKey = 0x30,
		ChangePassword = 0x31,
		GetFileTime = 0x33,
		QueryRealms = 0x34,
		Profile = 0x35,
		CdKey2 = 0x36,
		LogonResponse2 = 0x3a,
		CreateAccount2 = 0x3d,
		LogonRealmEx = 0x3e,
		QueryRealms2 = 0x40,
		QueryAdUrl = 0x41,
		WarcraftGeneral = 0x44,
		NetGamePort = 0x45,
		NewsInfo = 0x46,
		OptionalWork = 0x4a,
		ExtraWork = 0x4b,
		RequiredWork = 0x4c,
		AuthInfo = 0x50,
		AuthCheck = 0x51,
		AuthAccountCreate = 0x52,
		AuthAccountLogon = 0x53,
		AuthAccountLogonProof = 0x54,
		AuthAccountChange = 0x55,
		AuthAccountChangeProof = 0x56,
		AuthAccountUpgrade = 0x57,
		AuthAccountUpgradeProof = 0x58,
		SetEmail = 0x59,
		ResetPassword = 0x5a,
		ChangeEmail = 0x5b,
		SwitchProduct = 0x5c,
		Warden = 0x5e,
		GamePlayerSearch = 0x60,
		FriendsList = 0x65,
		FriendsUpdate = 0x66,
		FriendsAdd = 0x67,
		FriendsRemove = 0x68,
		FriendsPosition = 0x69,
		ClanFindCandidates = 0x70,
		ClanInviteMultiple = 0x71,
		ClanCreationInvitation = 0x72,
		ClanDisband = 0x73,
		ClanMakeChieftan = 0x74,
		ClanInfo = 0x75,
		ClanQuitNotify = 0x76,
		ClanInvitation = 0x77,
		ClanRemoveMember = 0x78,
		ClanInvitationResponse = 0x79,
		ClanRankChange = 0x7a,
		ClanSetMOTD = 0x7b,
		ClanMOTD = 0x7c,
		ClanMemberList = 0x7d,
		ClanMemberRemoved = 0x7e,
		ClanMemberStatusChanged = 0x7f,
		ClanMemberRankChange = 0x81,
		ClanMemberInformation = 0x82,
	}

	public class BNCSPacket : D2Packet
	{
		private readonly BNCSPacketId id;

		public BNCSPacket(BNCSPacketId id) { this.id = id; }

		public override byte[] GetHeader()
		{
			byte[] size = BitConverter.GetBytes(Count + 4);
			return new byte[] {0xFF, (byte)id, size[0], size[1]};
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class BNCSPacketHandlerAttribute : Attribute, IPacketHandler
	{
		public BNCSPacketHandlerAttribute(BNCSPacketId id) { Header = id; }
		public BNCSPacketId Header { get; set; }

		#region IPacketHandler Members

		public byte HeaderId
		{
			get { return (byte)Header; }
		}

		#endregion
	}
}
