using System.Collections.Generic;

namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 13, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AssignNPC)]
	public sealed class AssignNPC : D2GSPacket
	{
		public AssignNPC(byte[] bytes) : base(D2GSIncomingPacketId.AssignNPC) { SeedBytes(bytes); }
		public static AssignNPC Parse(byte[] bytes) { return new AssignNPC(bytes); }

		public new static bool IsCompletePacket(byte command, List<byte> input) { return input.Count == input[11] - 1; }
	}
}
