using System;

namespace BNet.MCP.Outgoing
{
	public sealed class GetGameList : MCPPacket
	{
		public GetGameList(ushort requestId, string search) : base(MCPPacketId.GameList)
		{
			Write(requestId);
			Write<uint>(0);
			Write(search);
		}
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.GameList)]
	public sealed class GetGameList : MCPPacket
	{
		public GetGameList(byte[] bytes) : base(MCPPacketId.GameList)
		{
			SeedBytes(bytes);

			RequestID = Read<ushort>();
			Index = Read<uint>();
			NumberOfPlayers = Read<byte>();
			Status = (GameStatus)Read<uint>();
			Name = Read();
			Description = Read();
		}

		public ushort RequestID { get; private set; }
		public uint Index { get; private set; }
		public byte NumberOfPlayers { get; private set; }
		public GameStatus Status { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public static GetGameList Parse(byte[] bytes) { return new GetGameList(bytes); }
	}
}

namespace BNet.MCP
{
	[Flags]
	public enum GameStatus : uint
	{
		None = 0x0,
		ContainsGame = 0x4,
		Hardcore = 0x800,
		Nightmare = 0x1000,
		Hell = 0x2000,
		Empty = 0x20000,
		Expansion = 0x100000,
		Ladder = 0x200000,
		GameDoesNotExist = 0xFFFFFFFE,
		RealmDown = 0xFFFFFFFF
	}
}
