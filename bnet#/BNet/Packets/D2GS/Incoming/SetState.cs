using System.Collections.Generic;

namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 7, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.SetState)]
	public sealed class SetState : D2GSPacket
	{
		public SetState(byte[] bytes) : base(D2GSIncomingPacketId.SetState) { SeedBytes(bytes); }
		public static SetState Parse(byte[] bytes) { return new SetState(bytes); }

		public new static bool IsCompletePacket(byte command, List<byte> input) { return input.Count == input[5] - 1; }
	}
}
