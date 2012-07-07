using System;

namespace BNet
{
	internal class Warden
	{
		private uint lastSeed;

		public byte[] IncomingKey { get; private set; }
		public byte[] OutgoingKey { get; private set; }

		public Warden(uint seed)
		{
			lastSeed = seed;
			UpdateKeys(seed);
		}

		public void UpdateKeys(uint seed)
		{
			lastSeed = seed;

			var seedBytes = BitConverter.GetBytes(seed);

		}
	}
}
