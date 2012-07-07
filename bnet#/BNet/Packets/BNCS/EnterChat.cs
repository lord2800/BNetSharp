namespace BNet.BNCS.Outgoing
{
	public sealed class EnterChat : BNCSPacket
	{
		public EnterChat(string realm, string character) : base(BNCSPacketId.EnterChat)
		{
			Write(character);
			Write(realm + "," + character);
		}
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.EnterChat)]
	public sealed class EnterChat : BNCSPacket
	{
		private EnterChat(byte[] bytes) : base(BNCSPacketId.EnterChat)
		{
			SeedBytes(bytes);

			UniqueName = Read();
			StatString = Read();
			AccountName = Read();
		}

		public string UniqueName { get; private set; }
		public string StatString { get; private set; }
		public string AccountName { get; private set; }
		public static EnterChat Parse(byte[] bytes) { return new EnterChat(bytes); }
	}
}
