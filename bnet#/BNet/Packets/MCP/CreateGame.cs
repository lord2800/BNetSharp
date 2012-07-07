using System;

namespace BNet.MCP.Outgoing
{
	public sealed class CreateGame : MCPPacket
	{
		public CreateGame(ushort requestId, string name, string password, string description, Difficulty difficulty,
		                  byte maxPlayers, byte playerLevelDifference) : base(MCPPacketId.CreateGame)
		{
			if(maxPlayers > 8)
				throw new ArgumentException("The maximum number of players is 8", "maxPlayers");

			if(playerLevelDifference > 99)
				playerLevelDifference = 0xFF;

			Write(requestId);
			Write((uint)difficulty);
			Write<byte>(1);
			Write(playerLevelDifference);
			Write(maxPlayers);
			Write(name);
			Write(password);
			Write(description);
		}

		public CreateGame(ushort requestId, string name, string password, Difficulty difficulty, byte maxPlayers,
		                  byte playerLevelDifference)
			: this(requestId, name, password, "", difficulty, maxPlayers, playerLevelDifference) { }

		public CreateGame(ushort requestId, string name, string password, Difficulty difficulty)
			: this(requestId, name, password, "", difficulty, 8, 0xFF) { }

		public CreateGame(ushort requestId, string name, Difficulty difficulty)
			: this(requestId, name, "", "", difficulty, 8, 0xFF) { }
	}
}

namespace BNet.MCP
{
	public enum Difficulty : uint
	{
		Normal = 0x0000,
		Nightmare = 0x1000,
		Hell = 0x2000
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.CreateGame)]
	public sealed class CreateGame : MCPPacket
	{
		public CreateGame(byte[] bytes) : base(MCPPacketId.CreateGame)
		{
			WriteBytes(bytes);
			Beginning();

			RequestId = Read<ushort>();
			GameToken = Read<ushort>();
			Unknown = Read<ushort>();
			Result = (CreateGameResult)Read<uint>();
		}

		public ushort RequestId { get; private set; }
		public ushort GameToken { get; private set; }
		public ushort Unknown { get; private set; }
		public CreateGameResult Result { get; private set; }
		public static CreateGame Parse(byte[] bytes) { return new CreateGame(bytes); }
	}
}

namespace BNet.MCP
{
	public enum CreateGameResult : uint
	{
		Success = 0x0,
		InvalidName = 0x1E,
		AlreadyExists = 0x1F,
		ServerDown = 0x20,
		DeadCharacter = 0x6E,
		ResultNotAvailable = 0xFF
	}
}
