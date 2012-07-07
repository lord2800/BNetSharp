using System;
using System.IO;

namespace BNSharp.MBNCSUtil
{
	/// <summary>
	/// Provides an implementation of Battle.net's "broken" (nonstandard) SHA-1 
	/// implementation.  This class cannot be inherited.
	/// </summary>
	/// <remarks>
	/// This class does not derive from the standard
	/// .NET <see cref="System.Security.Cryptography.SHA1">SHA1</see>
	/// class, and also does not provide adequate security for independent
	/// security solutions.  See the System.Security.Cryptography 
	/// namespace for more information.
	/// </remarks>
	/// <threadsafety>This type is safe for multithreaded operations.</threadsafety>
	public static class XSha1
	{
		private static uint ROL(uint val, int shift)
		{
			shift &= 0x1f;
			val = (val >> (0x20 - shift)) | (val << shift);
			return val;
		}

		/// <summary>
		/// Calculates the "broken" SHA-1 hash used by Battle.net.
		/// </summary>
		/// <param name="data">The data to hash.</param>
		/// <returns>A 20-byte array containing the hashed result.</returns>
		public static byte[] Hash(byte[] input)
		{
			if(input.Length > 1024) throw new ArgumentOutOfRangeException("input");

			byte[] data = new byte[1024];
			Array.Copy(input, 0, data, 0, input.Length);

			int i;
			uint a, b, c, d, e, g;
			using(MemoryStream mdata = new MemoryStream(data, true))
			{
				BinaryReader br = new BinaryReader(mdata);
				BinaryWriter bw = new BinaryWriter(mdata);

				for(i = 0; i < 64; i++)
				{
					mdata.Seek((i * 4), SeekOrigin.Begin);
					// mdata now at ldata[i]
					uint expr_ldata_i = br.ReadUInt32();
					// mdata now at ldata[i+1]
					mdata.Seek(1 * 4, SeekOrigin.Current);
					// mdata now at ldata[i+2]
					uint expr_ldata_i_2 = br.ReadUInt32();
					// mdata now at ldata[i+3]
					mdata.Seek(5 * 4, SeekOrigin.Current);
					// mdata now at ldata[i+8]
					uint expr_ldata_i_8 = br.ReadUInt32();
					// mdata now at ldata[i+9]
					mdata.Seek(4 * 4, SeekOrigin.Current);
					// mdata now at ldata[i+13]
					uint expr_ldata_i_13 = br.ReadUInt32();
					// mdata now at ldata[i+14]
					int shiftVal = (int)((expr_ldata_i ^ expr_ldata_i_8 ^ expr_ldata_i_2 ^ expr_ldata_i_13) & 0x1f);
					mdata.Seek(2 * 4, SeekOrigin.Current);
					// mdata now at ldata[i+16]
					bw.Write(ROL(1, shiftVal));
				}

				a = 0x67452301;
				b = 0xefcdab89;
				c = 0x98badcfe;
				d = 0x10325476;
				e = 0xc3d2e1f0;
				g = 0;

				mdata.Seek(0, SeekOrigin.Begin);

				#region loop 1

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(a, 5) + e + ((b & c) | (~b & d)) + 0x5A827999;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				#endregion

				#region loop 2

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() + 0x6ed9eba1;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				#endregion

				#region loop 3

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = br.ReadUInt32() + ROL(g, 5) + e + ((c & b) | (d & c) | (d & b)) - 0x70E44324;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				#endregion

				#region loop 4

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				g = (d ^ c ^ b) + e + ROL(g, 5) + br.ReadUInt32() - 0x359d3e2a;
				e = d;
				d = c;
				c = ROL(b, 30);
				b = a;
				a = g;

				#endregion
			}

			byte[] result = new byte[20];
			using(MemoryStream mdata2 = new MemoryStream(result, 0, 20, true, true))
			{
				BinaryWriter bw2 = new BinaryWriter(mdata2);
				unchecked
				{
					bw2.Write((0x67452301 + a));
					bw2.Write((0xefcdab89 + b));
					bw2.Write((0x98badcfe + c));
					bw2.Write((0x10325476 + d));
					bw2.Write((0xc3d2e1f0 + e));
				}
			}
			return result;
		}
	}
}
