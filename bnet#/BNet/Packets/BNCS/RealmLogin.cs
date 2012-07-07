using System;
using System.Net;

namespace BNet.BNCS.Outgoing
{
	public sealed class RealmLogin : BNCSPacket
	{
		public RealmLogin(uint clientToken, uint serverToken, RealmInfo realm) : base(BNCSPacketId.LogonRealmEx)
		{
			Write(clientToken);
			WriteBytes(BattleNetCredential.GetHash("password", clientToken, serverToken));
			Write(realm.Title);
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.LogonRealmEx)]
	public sealed class RealmLogin : BNCSPacket
	{
		private RealmLogin(byte[] bytes) : base(BNCSPacketId.LogonRealmEx)
		{
			LogonSuccess = bytes.Length != 8;

			SeedBytes(bytes);

			MCPCookie = Read<uint>();
			MCPStatus = Read<uint>();
			if(!LogonSuccess)
				return;

			byte[] chunk1 = ReadBytes(8);

			RealmIP = new IPAddress(Read<uint>());
			Port = (ushort)IPAddress.HostToNetworkOrder(Read<short>());
			Read<short>();

			byte[] chunk2 = ReadBytes(48);

			MCPChunk = new byte[56];
			Array.Copy(chunk1, MCPChunk, 8);
			Array.Copy(chunk2, 0, MCPChunk, 8, 48);

			UniqueName = Read();
		}

		public uint MCPCookie { get; private set; }
		public uint MCPStatus { get; private set; }
		public byte[] MCPChunk { get; private set; }
		public IPAddress RealmIP { get; private set; }
		public ushort Port { get; private set; }
		public string UniqueName { get; private set; }
		public bool LogonSuccess { get; private set; }
		public static RealmLogin Parse(byte[] bytes) { return new RealmLogin(bytes); }
	}
}

namespace BNet.BNCS
{
	public enum MCPStatusResult : uint
	{
		RealmUnavailable = 0x80000001,
		RealmLogonFailed = 0x80000002
	}
}
