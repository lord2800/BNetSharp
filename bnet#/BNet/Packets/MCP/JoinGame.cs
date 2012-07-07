using System.Net;

namespace BNet.MCP.Outgoing
{
	public sealed class JoinGame : MCPPacket
	{
		public JoinGame(ushort requestid, string name, string password) : base(MCPPacketId.JoinGame)
		{
			Write(requestid);
			Write(name);
			Write(password);
		}
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.JoinGame)]
	public sealed class JoinGame : MCPPacket
	{
		public JoinGame(byte[] bytes) : base(MCPPacketId.JoinGame)
		{
			SeedBytes(bytes);

			RequestID = Read<ushort>();
			GameToken = Read<ushort>();
			Unknown = Read<ushort>();
			GameServerIP = new IPAddress(Read<uint>());
			GameHash = Read<uint>();
			Result = (JoinGameResult)Read<uint>();
		}

		public ushort RequestID { get; private set; }
		public ushort GameToken { get; private set; }
		public ushort Unknown { get; private set; }
		public IPAddress GameServerIP { get; private set; }
		public uint GameHash { get; private set; }
		public JoinGameResult Result { get; private set; }
		public static JoinGame Parse(byte[] bytes) { return new JoinGame(bytes); }
	}
}

namespace BNet.MCP
{
	public enum JoinGameResult : uint
	{
		Success = 0x0,
		InvalidPassword = 0x29,
		GameDoesNotExist = 0x2A,
		GameIsFull = 0x2B,
		LevelRequired = 0x2C,
		DeadCharacter = 0x6E,
		HardcoreGame = 0x71,
		NightmareGame = 0x73,
		HellGame = 0x74,
		ExpansionGame = 0x78,
		NonExpansionGame = 0x79,
		LadderGame = 0x7D,
		ResultNotAvailable = 0xFF
	}
}
