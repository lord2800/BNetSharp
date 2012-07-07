namespace BNet.BNFTP
{
	using System;
	using Utilities;

	public class BNFTPv1Packet : D2Packet
	{
		public override byte[] GetHeader()
		{
			byte[] size = BitConverter.GetBytes(Count + 4);
			byte[] version = BitConverter.GetBytes(0x100);
			return new[] {size[0], size[1], version[0], version[1]};
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class BNFTPv1PacketHandlerAttribute : Attribute, IPacketHandler
	{
		#region IPacketHandler Members

		public byte HeaderId
		{
			get { return 0; }
		}

		#endregion
	}
}
