using System;

namespace BNet.MCP.Outgoing
{
	public sealed class GetGameInfo : MCPPacket
	{
		public GetGameInfo(ushort requestId, string name) : base(MCPPacketId.GameInfo)
		{
			Write(requestId);
			Write(name);
		}
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.GameInfo)]
	public sealed class GetGameInfo : MCPPacket
	{
		public GetGameInfo(byte[] bytes) : base(MCPPacketId.GameInfo)
		{
			SeedBytes(bytes);

			RequestID = Read<ushort>();
			Status = (GameStatus)Read<uint>();
			Uptime = new TimeSpan(0, 0, Read<int>());
			Unknown = Read<ushort>();
			MaxPlayers = Read<byte>();
			CurrentPlayers = Read<byte>();

			PlayerClasses = new CharClass[16];
			PlayerLevels = new byte[16];
			PlayerNames = new string[16];

			for(var i = 0; i < 16; i++)
				PlayerClasses[i] = (CharClass)(Read<byte>() + 1);
			for(var i = 0; i < 16; i++)
				PlayerLevels[i] = Read<byte>();

			Unused = Read<byte>();

			for(var i = 0; i < 16; i++)
				PlayerNames[i] = Read();
		}

		public ushort RequestID { get; private set; }
		public GameStatus Status { get; private set; }
		public TimeSpan Uptime { get; private set; }
		public ushort Unknown { get; private set; }
		public byte MaxPlayers { get; private set; }
		public byte CurrentPlayers { get; private set; }
		public CharClass[] PlayerClasses { get; private set; }
		public byte[] PlayerLevels { get; private set; }
		public ushort Unused { get; private set; }
		public string[] PlayerNames { get; private set; }
		public static GetGameInfo Parse(byte[] bytes) { return new GetGameInfo(bytes); }
	}
}
