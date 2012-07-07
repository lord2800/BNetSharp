namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.CreateQueue)]
	public sealed class CreateGameQueue : MCPPacket
	{
		public CreateGameQueue(byte[] bytes) : base(MCPPacketId.CreateQueue)
		{
			SeedBytes(bytes);

			Position = Read<uint>();
		}

		public uint Position { get; private set; }
		public static CreateGameQueue Parse(byte[] bytes) { return new CreateGameQueue(bytes); }
	}
}
