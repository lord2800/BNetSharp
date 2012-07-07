namespace BNet
{
	using System;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Collections.Generic;
	using Utilities;

	public abstract class D2Packet : Packet
	{
		protected MemoryStream stream = new MemoryStream();

		public int Count
		{
			get { return (int)stream.Length; }
		}

		public override void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
				stream.Dispose();
		}

		public abstract byte[] GetHeader();

		public long Seek(long destination, SeekOrigin origin) { return stream.Seek(destination, origin); }
		public void Beginning() { Seek(0, SeekOrigin.Begin); }
		public void Ending() { Seek(0, SeekOrigin.End); }

		public T Read<T>() where T : struct
		{
			var size = Marshal.SizeOf(typeof(T));
			var buffer = new byte[size];
			stream.Read(buffer, 0, size);
			return buffer.PinAndCast<T>();
		}

		public string Read() { return Read(-1); }
		public string Read(int size)
		{
			// read only to the end of the stream, in case we can't find the null terminator for some reason
			var keepReading = true;
			if(size == -1)
			{
				size = (int)(stream.Length - stream.Position);
				keepReading = false;
			}

			var bytes = new List<byte>();
			byte c;
			do { c = (byte)stream.ReadByte(); if(c != '\0') bytes.Add(c); } while(c != '\0' && --size > 0);

			if(size > 0 && keepReading) while(--size > 0) stream.ReadByte();

			return Encoding.ASCII.GetString(bytes.ToArray());
		}

		public void Write<T>(T value) where T : struct
		{
			var size = Marshal.SizeOf(typeof(T));
			var buffer = new byte[size];
			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
			WriteBytes(buffer);
			handle.Free();
		}
		public void Write(string value)
		{
			WriteBytes(Encoding.ASCII.GetBytes(value));
			Write<byte>(0);
		}
		public void Write(string value, bool reverse) { Write(value, 0x00, reverse); }
		public void Write(string value, byte padding, bool reverse)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(reverse)
			{
				var chars = value.ToCharArray();
				Array.Reverse(chars);

				var bsize = value.Length;
				var diff = bsize % 4;
				if(diff != 0) bsize += diff;
				var buffer = new byte[bsize];
				for(var i = 0; i <= diff; i++)
					buffer[i] = padding;
				var bytes = Encoding.ASCII.GetBytes(chars);
				Array.Copy(bytes, 0, buffer, diff, bytes.Length);
				WriteBytes(buffer);
			}
			else Write(value);
		}

		public byte[] ReadBytes(uint count)
		{
			var buffer = new byte[count];
			stream.BlockRead(buffer);
			return buffer;
		}
		public void WriteBytes(byte[] bytes)
		{
			if(bytes == null)
				throw new ArgumentNullException("bytes");

			stream.Write(bytes, 0, bytes.Length);
		}
		public void SeedBytes(byte[] bytes)
		{
			WriteBytes(bytes);
			Beginning();
		}

		public override string ToString()
		{
			// TODO: different formatting options
			byte[] bytes = this.ToByteArray();
			return BitConverter.ToString(bytes).Replace('-', ' ');
		}
		public override byte[] ToByteArray()
		{
			var header = GetHeader();
			var bytes = new byte[Count + header.Length];
			Array.Copy(header, bytes, header.Length);
			Array.Copy(stream.ToArray(), 0, bytes, header.Length, Count);
			return bytes;
		}

		public static implicit operator string(D2Packet packet) { return packet.ToString(); }
		public static implicit operator byte[](D2Packet packet) { return packet.ToByteArray(); }
	}
}
