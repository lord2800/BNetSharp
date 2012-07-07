using System.Collections.Generic;

namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.AddUnit)]
	public sealed class AddUnit : D2GSPacket
	{
		public AddUnit(byte[] bytes) : base(D2GSIncomingPacketId.AddUnit) { SeedBytes(bytes); }
		public static AddUnit Parse(byte[] bytes) { return new AddUnit(bytes); }

		public new static bool IsCompletePacket(byte command, List<byte> input) { return input.Count == input[5] - 1; }
	}
}
