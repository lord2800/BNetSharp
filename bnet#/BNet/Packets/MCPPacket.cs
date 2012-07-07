namespace BNet.MCP
{
	using System;
	using System.IO;
	using Utilities;

	internal sealed class MCPPacketBuilder : PacketBuilder<MCPPacketHandlerAttribute>
	{
		public override Packet Parse(BufferedStream buff)
		{
			var buf = new byte[2];
			buff.BlockRead(buf);

			ushort size = BitConverter.ToUInt16(buf, 0);
			if(size < 3)
				return null;

			var command = (MCPPacketId)buff.ReadByte();
			ProcessType((byte)command);

			buf = new byte[size - 3];
			buff.BlockRead(buf);
			return processors[(byte)command].Value(buf);
		}
	}

	public enum MCPPacketId : byte
	{
		Startup = 0x01,
		CreateChar = 0x02,
		CreateGame = 0x03,
		JoinGame = 0x04,
		GameList = 0x05,
		GameInfo = 0x06,
		CharLogon = 0x07,
		CharDelete = 0x0A,
		RequestLadderData = 0x11,
		MOTD = 0x12,
		CancelGameCreate = 0x13,
		CreateQueue = 0x14,
		CharList = 0x17,
		CharUpgrade = 0x18,
		CharList2 = 0x19
	}

	public class MCPPacket : D2Packet
	{
		private readonly MCPPacketId id;


		public MCPPacket(MCPPacketId id) { this.id = id; }

		public override byte[] GetHeader()
		{
			var size = BitConverter.GetBytes(Count + 3);
			return new[] {size[0], size[1], (byte)id};
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class MCPPacketHandlerAttribute : Attribute, IPacketHandler
	{
		public MCPPacketHandlerAttribute(MCPPacketId id) { Header = id; }
		public MCPPacketId Header { get; set; }

		#region IPacketHandler Members

		public byte HeaderId
		{
			get { return (byte)Header; }
		}

		#endregion
	}
}
