namespace BNet.BNCS.Outgoing
{
	public sealed class AdvertiseGame : BNCSPacket
	{
		public AdvertiseGame(string name, string password, bool priv) : base(BNCSPacketId.StartAdvEx3)
		{
			Write((uint)(priv ? 1 : 0));
			Write<uint>(0);
			Write<ushort>(0);
			Write<ushort>(0);
			Write<uint>(0);
			Write<uint>(0);
			Write(name);
			Write(password);
			Write("");
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.StartAdvEx3)]
	public sealed class AdvertiseGame : BNCSPacket
	{
		public AdvertiseGame(byte[] bytes) : base(BNCSPacketId.StartAdvEx3)
		{
			SeedBytes(bytes);

			Status = Read<uint>() == 0;
		}

		public bool Status { get; private set; }
		public static AdvertiseGame Parse(byte[] bytes) { return new AdvertiseGame(bytes); }
	}
}
