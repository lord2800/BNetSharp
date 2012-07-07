namespace BNet.MCP.Outgoing
{
	public sealed class CharLogon : MCPPacket
	{
		public CharLogon(string charname) : base(MCPPacketId.CharLogon) { Write(charname); }
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.CharLogon)]
	public sealed class CharLogon : MCPPacket
	{
		private CharLogon(byte[] bytes) : base(MCPPacketId.CharLogon)
		{
			SeedBytes(bytes);

			Response = (CharLogonResponse)Read<uint>();
		}

		public CharLogonResponse Response { get; private set; }
		public static CharLogon Parse(byte[] bytes) { return new CharLogon(bytes); }
	}
}

namespace BNet.MCP
{
	public enum CharLogonResponse
	{
		Success = 0x00,
		PlayerNotFound = 0x46,
		LogonFailed = 0x7A,
		CharacterExpired = 0x7B
	}
}
