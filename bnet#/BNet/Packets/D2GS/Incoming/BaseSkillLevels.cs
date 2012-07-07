using System.Collections.Generic;

namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 2, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.BaseSkillLevels)]
	public sealed class BaseSkillLevels : D2GSPacket
	{
		public BaseSkillLevels(byte[] bytes) : base(D2GSIncomingPacketId.BaseSkillLevels) { SeedBytes(bytes); }
		public static BaseSkillLevels Parse(byte[] bytes) { return new BaseSkillLevels(bytes); }

		public new static bool IsCompletePacket(byte command, List<byte> input) { return input.Count == (input[0] * 3) + 5; }
	}
}
