using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;

namespace Utilities
{
	public class BitReader
	{
		private BitArray m_bits;
		private int m_offset;

		public BitReader(byte[] bytes)
		{
			m_bits = new BitArray(bytes);
			m_offset = 0;
		}

		public bool ReadBit()
		{
			return m_bits[m_offset++];
		}

		public uint Read(int length)
		{
			int initialLen = length;
			uint bits = 0;
			while (length > 0)
			{
				bool bit = ReadBit();
				bits |= (uint)((bit ? 1 : 0) << initialLen - length);

				length -= 1;
			}
			return bits;
		}

		public string ReadString()
		{
			List<byte> bytes = new List<byte>();
			byte b = 0;
			do { b = Read<byte>(); bytes.Add(b); } while(b != 0);
			return System.Text.Encoding.ASCII.GetString(bytes.ToArray());
		}

		public string ReadString(int length)
		{
			List<byte> bytes = new List<byte>();
			byte b = 0;
			do { b = Read<byte>(); bytes.Add(b); } while (b != 0 && (length--)-1 != 0);
			return System.Text.Encoding.ASCII.GetString(bytes.ToArray());
		}

		public T Read<T>(int length) where T : struct
		{
			return BitConverter.GetBytes(Read(length)).PinAndCast<T>();
		}

		public T Read<T>() where T : struct
		{
			return Read<T>(Marshal.SizeOf(typeof(T))*8);
		}
	}
}
