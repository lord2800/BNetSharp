using System.Collections.Generic;

namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 10, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.ReceiveChat)]
	public sealed class ReceiveChat : D2GSPacket
	{
		public ReceiveChat(byte[] bytes) : base(D2GSIncomingPacketId.ReceiveChat) { SeedBytes(bytes); }
		public static ReceiveChat Parse(byte[] bytes) { return new ReceiveChat(bytes); }

		public new static bool IsCompletePacket(byte command, List<byte> input)
		{
			var last = input.FindIndex(9, (byte x) => x == 0);
			return input[input.Count - 1] == 0x00 && last != -1 && last != input.Count - 1;
		}
	}
}
