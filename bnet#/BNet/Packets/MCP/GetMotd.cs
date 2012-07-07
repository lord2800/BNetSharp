namespace BNet.MCP.Outgoing
{
	public sealed class GetMotd : MCPPacket
	{
		public GetMotd() : base(MCPPacketId.MOTD) { }
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.MOTD)]
	public sealed class GetMotd : MCPPacket
	{
		private GetMotd(byte[] bytes) : base(MCPPacketId.MOTD)
		{
			SeedBytes(bytes);

			Read<byte>();
			Message = Read();
		}

		public string Message { get; private set; }
		public static GetMotd Parse(byte[] bytes) { return new GetMotd(bytes); }
	}
}
