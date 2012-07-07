namespace BNet.MCP.Outgoing
{
	public sealed class Startup : MCPPacket
	{
		public Startup(uint cookie, uint status, byte[] chunk, string uniqueName) : base(MCPPacketId.Startup)
		{
			Write(cookie);
			Write(status);
			WriteBytes(chunk);
			Write(uniqueName);
		}
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.Startup)]
	public sealed class Startup : MCPPacket
	{
		private Startup(byte[] bytes) : base(MCPPacketId.Startup)
		{
			SeedBytes(bytes);

			Response = (StartupResponse)Read<uint>();
		}

		public StartupResponse Response { get; private set; }
		public static Startup Parse(byte[] bytes) { return new Startup(bytes); }
	}
}

namespace BNet.MCP
{
	public enum StartupResponse
	{
		Success = 0x0,
		NotConnected = 0x2,
		RealmUnavailable = 0xA,
		RealmUnavailable2 = 0xB,
		RealmUnavailable3 = 0xC,
		RealmUnavailable4 = 0xD,
		CdKeyBanned = 0x7E,
		TemporaryIPBan = 0x7F
	}
}
