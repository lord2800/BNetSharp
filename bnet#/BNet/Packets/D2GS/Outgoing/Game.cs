using BNet.MCP;

namespace BNet.D2GS.Outgoing
{
	using System;

	public sealed class Startup : D2GSPacket
	{
		public Startup(uint version, uint serverHash, ushort serverToken, CharInfo character)
			: base(D2GSOutgoingPacketId.Startup)
		{
			Write(serverHash);
			Write(serverToken);
			Write((byte)character.Class);
			Write(version);
			Write(0xED5DCC50);
			Write(0x91A519B6);
			Write<byte>(0);
			Write(character.Name.PadRight(15, '\0'));
		}
	}

	public sealed class LeaveGame : D2GSPacket
	{
		public LeaveGame() : base(D2GSOutgoingPacketId.LeaveGame) { }
	}

	public sealed class JoinGame : D2GSPacket
	{
		public JoinGame() : base(D2GSOutgoingPacketId.JoinGame) { }
	}

	public sealed class Ping : D2GSPacket
	{
		public Ping() : base(D2GSOutgoingPacketId.Ping)
		{
			Write((uint)(DateTime.Now.Ticks / 10000));
			Write<uint>(0);
			Write<uint>(0);
		}
	}
}
