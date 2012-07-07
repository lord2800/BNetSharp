using System;
using System.Collections.Generic;

namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 3, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.WardenRequest)]
	public sealed class WardenRequest : D2GSPacket
	{
		public WardenRequest(byte[] bytes) : base(D2GSIncomingPacketId.WardenRequest) { SeedBytes(bytes); }
		public static WardenRequest Parse(byte[] bytes) { return new WardenRequest(bytes); }

		public void Decrypt(byte[] key)
		{
			

			var code = Read<byte>();
			switch(code)
			{
				case 0:
					var name = Read(16);
					var decKey = ReadBytes(16);
					var len = Read<uint>();
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
			}
		}

		public new static bool IsCompletePacket(byte command, List<byte> input) { return input.Count == BitConverter.ToUInt16(input.ToArray(), 1); }
	}
}
