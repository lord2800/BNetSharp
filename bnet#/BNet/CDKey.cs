using System;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
#if DEBUG
using System.Diagnostics;

#endif

namespace BNSharp.MBNCSUtil
{
	/// <summary>
	/// Provides utilities for decoding and otherwise validating
	/// CD keys of Blizzard products.  This class cannot be inherited.
	/// </summary>
	/// <threadsafety>This type is safe for multithreaded operations.</threadsafety>
	public sealed class CDKey
	{
		#region fields

		private readonly string inputKey;

		private uint product,
		             val1;

		private byte[] val2;
		private byte[] hash;
		private bool valid;

		#endregion

		#region consts

		private const uint W3Keylen = 26;
		private const uint W3Buflen = W3Keylen << 1;

		#endregion

		#region map buffers

		private static readonly byte[] W2Map = new byte[] {
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0x00, 0xFF, 0x01, 0xFF, 0x02, 0x03, 0x04, 0x05, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B,
		            0x0C, 0xFF, 0x0D, 0x0E, 0xFF, 0x0F, 0x10, 0xFF, 0x11, 0xFF, 0x12, 0xFF,
		            0x13, 0xFF, 0x14, 0x15, 0x16, 0xFF, 0x17, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0xFF, 0x0D, 0x0E,
		            0xFF, 0x0F, 0x10, 0xFF, 0x11, 0xFF, 0x12, 0xFF, 0x13, 0xFF, 0x14, 0x15,
		            0x16, 0xFF, 0x17, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
		            0xFF, 0xFF, 0xFF, 0xFF };

		private static readonly byte[] W3KeyMap = new byte[] {
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0x00, 0xFF, 0x01, 0xFF, 0x02, 0x03, 0x04, 0x05, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x06, 0x07, 0x08, 0x09, 0x0A,
					0x0B, 0x0C, 0xFF, 0x0D, 0x0E, 0xFF, 0x0F, 0x10, 0xFF, 0x11, 0xFF, 0x12,
					0xFF, 0x13, 0xFF, 0x14, 0x15, 0x16, 0x17, 0x18, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0xFF, 0x0D,
					0x0E, 0xFF, 0x0F, 0x10, 0xFF, 0x11, 0xFF, 0x12, 0xFF, 0x13, 0xFF, 0x14,
					0x15, 0x16, 0x17, 0x18, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
					0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

		private static readonly byte[] W3TranslateMap = new byte[] {
		            0x09, 0x04, 0x07, 0x0F, 0x0D, 0x0A, 0x03, 0x0B, 0x01, 0x02, 0x0C, 0x08,
		            0x06, 0x0E, 0x05, 0x00, 0x09, 0x0B, 0x05, 0x04, 0x08, 0x0F, 0x01, 0x0E,
		            0x07, 0x00, 0x03, 0x02, 0x0A, 0x06, 0x0D, 0x0C, 0x0C, 0x0E, 0x01, 0x04,
		            0x09, 0x0F, 0x0A, 0x0B, 0x0D, 0x06, 0x00, 0x08, 0x07, 0x02, 0x05, 0x03,
		            0x0B, 0x02, 0x05, 0x0E, 0x0D, 0x03, 0x09, 0x00, 0x01, 0x0F, 0x07, 0x0C,
		            0x0A, 0x06, 0x04, 0x08, 0x06, 0x02, 0x04, 0x05, 0x0B, 0x08, 0x0C, 0x0E,
		            0x0D, 0x0F, 0x07, 0x01, 0x0A, 0x00, 0x03, 0x09, 0x05, 0x04, 0x0E, 0x0C,
		            0x07, 0x06, 0x0D, 0x0A, 0x0F, 0x02, 0x09, 0x01, 0x00, 0x0B, 0x08, 0x03,
		            0x0C, 0x07, 0x08, 0x0F, 0x0B, 0x00, 0x05, 0x09, 0x0D, 0x0A, 0x06, 0x0E,
		            0x02, 0x04, 0x03, 0x01, 0x03, 0x0A, 0x0E, 0x08, 0x01, 0x0B, 0x05, 0x04,
		            0x02, 0x0F, 0x0D, 0x0C, 0x06, 0x07, 0x09, 0x00, 0x0C, 0x0D, 0x01, 0x0F,
		            0x08, 0x0E, 0x05, 0x0B, 0x03, 0x0A, 0x09, 0x00, 0x07, 0x02, 0x04, 0x06,
		            0x0D, 0x0A, 0x07, 0x0E, 0x01, 0x06, 0x0B, 0x08, 0x0F, 0x0C, 0x05, 0x02,
		            0x03, 0x00, 0x04, 0x09, 0x03, 0x0E, 0x07, 0x05, 0x0B, 0x0F, 0x08, 0x0C,
		            0x01, 0x0A, 0x04, 0x0D, 0x00, 0x06, 0x09, 0x02, 0x0B, 0x06, 0x09, 0x04,
		            0x01, 0x08, 0x0A, 0x0D, 0x07, 0x0E, 0x00, 0x0C, 0x0F, 0x02, 0x03, 0x05,
		            0x0C, 0x07, 0x08, 0x0D, 0x03, 0x0B, 0x00, 0x0E, 0x06, 0x0F, 0x09, 0x04,
		            0x0A, 0x01, 0x05, 0x02, 0x0C, 0x06, 0x0D, 0x09, 0x0B, 0x00, 0x01, 0x02,
		            0x0F, 0x07, 0x03, 0x04, 0x0A, 0x0E, 0x08, 0x05, 0x03, 0x06, 0x01, 0x05,
		            0x0B, 0x0C, 0x08, 0x00, 0x0F, 0x0E, 0x09, 0x04, 0x07, 0x0A, 0x0D, 0x02,
		            0x0A, 0x07, 0x0B, 0x0F, 0x02, 0x08, 0x00, 0x0D, 0x0E, 0x0C, 0x01, 0x06,
		            0x09, 0x03, 0x05, 0x04, 0x0A, 0x0B, 0x0D, 0x04, 0x03, 0x08, 0x05, 0x09,
		            0x01, 0x00, 0x0F, 0x0C, 0x07, 0x0E, 0x02, 0x06, 0x0B, 0x04, 0x0D, 0x0F,
		            0x01, 0x06, 0x03, 0x0E, 0x07, 0x0A, 0x0C, 0x08, 0x09, 0x02, 0x05, 0x00,
		            0x09, 0x06, 0x07, 0x00, 0x01, 0x0A, 0x0D, 0x02, 0x03, 0x0E, 0x0F, 0x0C,
		            0x05, 0x0B, 0x04, 0x08, 0x0D, 0x0E, 0x05, 0x06, 0x01, 0x09, 0x08, 0x0C,
		            0x02, 0x0F, 0x03, 0x07, 0x0B, 0x04, 0x00, 0x0A, 0x09, 0x0F, 0x04, 0x00,
		            0x01, 0x06, 0x0A, 0x0E, 0x02, 0x03, 0x07, 0x0D, 0x05, 0x0B, 0x08, 0x0C,
		            0x03, 0x0E, 0x01, 0x0A, 0x02, 0x0C, 0x08, 0x04, 0x0B, 0x07, 0x0D, 0x00,
		            0x0F, 0x06, 0x09, 0x05, 0x07, 0x02, 0x0C, 0x06, 0x0A, 0x08, 0x0B, 0x00,
		            0x0F, 0x04, 0x03, 0x0E, 0x09, 0x01, 0x0D, 0x05, 0x0C, 0x04, 0x05, 0x09,
		            0x0A, 0x02, 0x08, 0x0D, 0x03, 0x0F, 0x01, 0x0E, 0x06, 0x07, 0x0B, 0x00,
		            0x0A, 0x08, 0x0E, 0x0D, 0x09, 0x0F, 0x03, 0x00, 0x04, 0x06, 0x01, 0x0C,
		            0x07, 0x0B, 0x02, 0x05, 0x03, 0x0C, 0x04, 0x0A, 0x02, 0x0F, 0x0D, 0x0E,
		            0x07, 0x00, 0x05, 0x08, 0x01, 0x06, 0x0B, 0x09, 0x0A, 0x0C, 0x01, 0x00,
		            0x09, 0x0E, 0x0D, 0x0B, 0x03, 0x07, 0x0F, 0x08, 0x05, 0x02, 0x04, 0x06,
		            0x0E, 0x0A, 0x01, 0x08, 0x07, 0x06, 0x05, 0x0C, 0x02, 0x0F, 0x00, 0x0D,
		            0x03, 0x0B, 0x04, 0x09, 0x03, 0x08, 0x0E, 0x00, 0x07, 0x09, 0x0F, 0x0C,
		            0x01, 0x06, 0x0D, 0x02, 0x05, 0x0A, 0x0B, 0x04, 0x03, 0x0A, 0x0C, 0x04,
		            0x0D, 0x0B, 0x09, 0x0E, 0x0F, 0x06, 0x01, 0x07, 0x02, 0x00, 0x05, 0x08 };

		#endregion

		#region ctor/static creators

		/// <summary>
		/// Creates a CD key decoder for the specified key.
		/// </summary>
		/// <remarks>
		/// <para>This constructor only conducts initial validity checks to ensure that the CD key is valid; that is,
		/// it checks for a valid key length (13, 16, or 26 characters) and checks that the characters are valid 
		/// for the key type.  For example, Starcraft keys are numeric only, whereas Warcraft II, Diablo II, Lord
		/// of Destruction, Warcraft III, and The Frozen Throne keys are alphanumeric.</para>
		/// <para>Additional validity checks are conducted internally; however, these do not raise Exceptions.  To
		/// confirm the validity of a key after instantiation, call the <b>CdKey</b>.<see cref="CDKey.IsValid">IsValid</see>
		/// property.</para>
		/// </remarks>
		/// <param name="cdKey">The CD key to initialize processing for.</param>
		/// <exception cref="ArgumentNullException">Thrown if the value of <i>cdKey</i> is 
		/// <b>null</b> (<b>Nothing</b> in Visual Basic).</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the 
		/// CD key is an invalid length or does not pass initial 
		/// validity checks.</exception>
		public CDKey(string cdKey)
		{
			InitializePrivate(cdKey);
			inputKey = cdKey;
		}

		public static bool IsValid(string cdKey)
		{
			if(String.IsNullOrEmpty(cdKey))
				return false;

			Func<char, bool> checker;

			switch(cdKey.Length)
			{
				case 13:
					checker = char.IsDigit;
					break;
				case 16:
				case 26:
					checker = char.IsLetterOrDigit;
					break;
				default:
					return false;
			}

			foreach(char c in cdKey)
			{
				if(!checker(c))
					return false;
			}

			return true;
		}

		private void InitializePrivate(string cdKey)
		{
			if(cdKey == null)
				throw new ArgumentNullException("cdKey");
			if(!IsValid(cdKey))
				throw new ArgumentOutOfRangeException("cdKey", cdKey, "Invalid CD Key Value");

			Key = cdKey;

			switch(cdKey.Length)
			{
				case 13:
					// starcraft key
					ProcSCKey();
					break;
				case 16:
					// w2bn/d2dv/d2xp key
					ProcW2Key();
					break;
				case 26:
					// war3/w3xp key
					ProcW3Key();
					break;
			}
		}

		#endregion

		#region properties

		/// <summary>
		/// Gets the CD key this object is processing.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if the object has not yet been
		/// initialized.</exception>
		public string Key { get; private set; }

		/// <summary>
		/// Gets the product value encoded in the CD key.
		/// </summary>
		/// <remarks>This property returns 0 if the CD key is not valid.  To check validity, use the 
		/// <b>IsValid</b> property.</remarks>
		/// <exception cref="InvalidOperationException">Thrown if the object has not yet been
		/// initialized.</exception>
		public CDKeyProduct Product
		{
			get { return (CDKeyProduct)product; }
		}

		/// <summary>
		/// Gets the "Public" or "Value 1" value of the CD key.
		/// </summary>
		/// <remarks>This property returns 0 if the CD key is not valid.  To check validity, use the 
		/// <b>IsValid</b> property.</remarks>
		/// <exception cref="InvalidOperationException">Thrown if the object has not yet been
		/// initialized.</exception>
		public int Public
		{
			get { return unchecked((int)val1); }
		}

		/// <summary>
		/// Gets the "Private" or "Value 2" value of the CD key.
		/// </summary>
		/// <remarks>
		/// <para>This method returns <b>null</b> (<b>Nothing</b> in Visual Basic) if the CD key is not valid.  
		/// To check validity, use the <b>IsValid</b> property.</para>
		/// <para>For Starcraft, Warcraft II: Battle.net Edition, Diablo II, or Lord of Destruction CD keys,
		/// this value is a 4-byte array.  It can be converted to an integer value with the 
		/// <see cref="System.BitConverter">BitConverter</see> class.</para>
		/// <para>For Warcraft III: The Reign of Chaos and The Frozen Throne CD keys, this value is a 10-byte
		/// array.</para>
		/// </remarks>
		public byte[] GetPrivate()
		{
			var result = new byte[val2.Length];
			Buffer.BlockCopy(val2, 0, result, 0, result.Length);
			return result;
		}

		/// <summary>
		/// Computes the 20-byte hash value of the CD key.
		/// </summary>
		/// <remarks>
		/// <para>The result of the hash calculation is used in the message 0x51 SID_AUTH_CHECK (from the client)
		/// as well as 0x36 SID_CDKEY2.</para>
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown if the object has not yet been
		/// initialized.</exception>
		/// <param name="clientToken">A randomly-generated token value that is determined by session at the client.</param>
		/// <param name="serverToken">A randomly-generated token value that is determined by session at the server.</param>
		/// <returns>A 20-byte array containing the hash value of the specified key.</returns>
		public byte[] GetHash(int clientToken, int serverToken) { return GetHash(unchecked((uint)clientToken), unchecked((uint)serverToken)); }

		/// <summary>
		/// Computes the 20-byte hash value of the CD key.  This method is not CLS-compliant.
		/// </summary>
		/// <remarks>
		/// <para>The result of the hash calculation is used in the message 0x51 SID_AUTH_CHECK (from the client)
		/// as well as 0x36 SID_CDKEY2.</para>
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown if the object has not yet been
		/// initialized.</exception>
		/// <param name="clientToken">A randomly-generated token value that is determined by session at the client.</param>
		/// <param name="serverToken">A randomly-generated token value that is determined by session at the server.</param>
		/// <returns>A 20-byte array containing the hash value of the specified key.</returns>
		public byte[] GetHash(uint clientToken, uint serverToken)
		{
			lock(this)
			{
				if(hash == null)
					CalculateHash(clientToken, serverToken);
			}

			return hash;
		}

		#endregion

		#region sc key processing

		private void ProcSCKey()
		{
			uint hashKey = 0x13ac9741;
			uint accum = 3;
			// verification
			for(int i = 0; i < 12; i++)
				accum += (((uint)(Key[i] - '0')) ^ (accum * 2));
			if((accum % 10) != (Key[12] - '0'))
			{
				valid = false;
				return;
			}

			// do the key shuffle
			int pos = 0x0b;
			char temp;
			char[] cdkey = Key.ToCharArray();
			for(int i = 0xc2; i >= 7; i -= 0x11)
			{
				temp = cdkey[pos];
				cdkey[pos] = cdkey[i % 0x0c];
				cdkey[i % 0x0c] = temp;
				pos--;
			}

			// final value
			for(int i = Key.Length - 2; i >= 0; i--)
			{
				temp = Char.ToUpper(cdkey[i], CultureInfo.InvariantCulture);
				cdkey[i] = temp;
				if(temp <= '7')
				{
					cdkey[i] ^= (char)(hashKey & 7);
					hashKey >>= 3;
				}
				else if(temp <= 'A')
					cdkey[i] ^= ((char)(i & 1));
			}
			Key = new string(cdkey);

			product = uint.Parse(Key.Substring(0, 2), CultureInfo.InvariantCulture);
			val1 = uint.Parse(Key.Substring(2, 7), CultureInfo.InvariantCulture);
			val2 = BitConverter.GetBytes(uint.Parse(Key.Substring(9, 3), CultureInfo.InvariantCulture));

			valid = true;
		}

		#endregion

		#region w2 cd key

		private void ProcW2Key()
		{
			char[] cdkey = Key.ToCharArray();

			uint r = 1,
			     checksum = 0;
			uint n2;
			uint n;
			byte c2;
			byte c1;

			for(int i = 0; i < 16; i += 2)
			{
				c1 = W2Map[Key[i]];
				n = c1 * 3u;
				c2 = W2Map[cdkey[i + 1]];
				n = c2 + n * 8;

				if(n >= 0x100)
				{
					n -= 0x100;
					checksum |= r;
				}
				// !
				n2 = n >> 4;
				// !
				cdkey[i] = (n2 % 16).ToString("x", CultureInfo.InvariantCulture)[0];
				cdkey[i + 1] = (n % 16).ToString("x", CultureInfo.InvariantCulture)[0];
				r <<= 1;
			}

			uint v = 3;
			for(int i = 0; i < 16; i++)
			{
				c1 = (byte)(cdkey[i] & 0xff);
				n = GetNumValue((char)c1);
				n2 = v * 2;
				n ^= n2;
				v += n;
			}
			v &= 0xff;

			if(v != checksum)
			{
				valid = false;
				return;
			}
			valid = true;

			for(int j = 15; j >= 0; j--)
			{
				c1 = (byte)cdkey[j];
				if(j > 8)
					n = (uint)(j - 9);
				else
					n = (uint)(0x0f + j - 8);
				n &= 0x0f;
				c2 = (byte)cdkey[n];
				cdkey[j] = (char)c2;
				cdkey[n] = (char)c1;
			}

			uint v2 = 0x13ac9741;
			for(int j = 15; j >= 0; j--)
			{
				cdkey[j] = char.ToUpper(cdkey[j], CultureInfo.InvariantCulture);
				c1 = (byte)cdkey[j];
				if(c1 <= '7')
				{
					v = v2;
					// in both C/++ and C#, the xor operator
					// is lower precedence than the and operator.
					// still, I wanted to ensure that the intent
					// of this line was clear.
					c2 = (byte)((((byte)(v & 0xff)) & 7) ^ c1);
					v >>= 3;
					cdkey[j] = (char)c2;
					v2 = v;
				}
				else if(c1 < 'A')
					cdkey[j] = (char)((((char)j) & 1) ^ c1);
			}

			Key = new string(cdkey);

			product = uint.Parse(Key.Substring(0, 2),
			                     NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			val1 = uint.Parse(Key.Substring(2, 6),
			                  NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			val2 = BitConverter.GetBytes(uint.Parse(Key.Substring(8, 8),
			                                        NumberStyles.HexNumber, CultureInfo.InvariantCulture));
		}

		private static uint GetNumValue(char c)
		{
			char s = char.ToUpper(c, CultureInfo.InvariantCulture);
			return unchecked((uint)((char.IsDigit(s)) ? (s - 0x30) : (s - 0x37)));
		}

		#endregion

		#region w3 cd key

		private void ProcW3Key()
		{
			char[] cdkey = Key.ToUpper(CultureInfo.InvariantCulture).ToCharArray();
			var table = new byte[W3Buflen];
			var values = new uint[4];
			int i;
			uint b = 0x21;

			for(i = 0; i < Key.Length; i++)
			{
				uint a = (b + 0x07b5) % W3Buflen;
				b = (a + 0x07b5) % W3Buflen;
				byte decode = W3KeyMap[cdkey[i]];
				table[a] = (byte)(decode / 5);
				table[b] = (byte)(decode % 5);
			}

			// Mult
			i = unchecked((int)W3Buflen);
			var srcData = new byte[16];
			using(var msval = new MemoryStream(srcData, true))
			{
				do
				{
					Safemult(5, msval, table[i - 1]);
				} while(--i != 0);
			}
			Buffer.BlockCopy(srcData, 0, values, 0, 16);

			DecodeKeyTable(values);

			product = values[0] >> 0x0a;
			//	product = SWAP4(product);

			// it's definitely little-endian
			for(i = 0; i < 4; i++)
				values[i] = SWAP4(values[i]);

			// this is where we pretend we're using pointers.
			using(var ms = new MemoryStream(16))
			{
				var bw = new BinaryWriter(ms);
				var br = new BinaryReader(ms);
				bw.Write(values[0]);
				bw.Write(values[1]);
				bw.Write(values[2]);
				bw.Write(values[3]);
				ms.Seek(2, SeekOrigin.Begin);
				val1 = SWAP4(br.ReadUInt32() & 0xffffff03);

				val2 = new byte[10];
				using(var val2Ms = new MemoryStream(val2, true))
				{
					var val2Bw = new BinaryWriter(val2Ms);
					val2Bw.Write(SWAP2(br.ReadUInt16()));
					val2Bw.Write(SWAP4(br.ReadUInt32()));
					val2Bw.Write(SWAP4(br.ReadUInt32()));
				}
			}

			valid = true;

			return;
		}

		private static void Safemult(int x, Stream a, uint dcByte)
		{
			a.Seek(12, SeekOrigin.Begin);

			var br = new BinaryReader(a);
			var bw = new BinaryWriter(a);
			// unrolled loop?
			// r = 4
			ulong edxeax = ((br.ReadUInt32() & 0xffffffff)) * ((ulong)(x & 0xffffffff));
			a.Seek(12, SeekOrigin.Begin);
			bw.Write((dcByte + (uint)edxeax));
			dcByte = (uint)(edxeax >> 32);
			// r = 3
			a.Seek(8, SeekOrigin.Begin);
			edxeax = ((br.ReadUInt32() & 0xffffffff)) * ((ulong)(x & 0xffffffff));
			a.Seek(8, SeekOrigin.Begin);
			bw.Write((dcByte + (uint)edxeax));
			dcByte = (uint)(edxeax >> 32);
			// r = 2
			a.Seek(4, SeekOrigin.Begin);
			edxeax = ((br.ReadUInt32() & 0xffffffff)) * ((ulong)(x & 0xffffffff));
			a.Seek(4, SeekOrigin.Begin);
			bw.Write((dcByte + (uint)edxeax));
			dcByte = (uint)(edxeax >> 32);
			// r = 1
			a.Seek(0, SeekOrigin.Begin);
			edxeax = ((br.ReadUInt32() & 0xffffffff)) * ((ulong)(x & 0xffffffff));
			a.Seek(0, SeekOrigin.Begin);
			bw.Write((dcByte + (uint)edxeax));

//			while (r-- != 0)
//			{
//				ulong edxeax = ((ulong)(br.ReadUInt32() & 0xffffffff)) * ((ulong)(x & 0xffffffff));
//				// restore ptr to a (because br.ReadUInt32() in last line
//				// caused a to increment.
//				a.Seek(-1 * 4, SeekOrigin.Current);
//				// write to *a
//				bw.Write((uint)(dcByte + (uint)edxeax));
//				// restore ptr to a (because that write caused a to increment)
//				a.Seek(-1 * 4, SeekOrigin.Current);
//				// decrement ptr to a again
//				a.Seek(-1 * 4, SeekOrigin.Current);
//				dcByte = (uint)(edxeax >> 32);
//			}
		}

		private static void DecodeKeyTable(uint[] keyTable)
		{
			unchecked
			{
				uint ecx;
				uint edi,
				     esi,
				     ebp;
				var copy = new uint[4];
				uint var8 = 29;
				int i = 464;

				do
				{
					int j;
					esi = (var8 & 7) << 2;
					uint var4 = var8 >> 3;
					uint varC = keyTable[3 - var4];
					varC &= (0x0fu << (int)esi);
					varC >>= (int)esi;

					if(i < 464)
					{
						for(j = 29; j > var8; j--)
						{
							ecx = (uint)((j & 7) << 2);
							ebp = (keyTable[3 - (j >> 3)]);
							ebp &= (uint)(0x0f << (int)ecx);
							ebp >>= (int)ecx;
							varC = W3TranslateMap[ebp ^ W3TranslateMap[varC + i] + i];
						}
					}

					j = (int)(--var8);

					while(j >= 0)
					{
						ecx = (uint)(j & 7u) << 2;
						ebp = (keyTable[0x3 - (j >> 3)]);
						ebp &= (0x0fu << (int)ecx);
						ebp >>= (int)ecx;
						varC = W3TranslateMap[ebp ^ W3TranslateMap[varC + i] + i];
						j--;
					}

					j = (int)(3 - var4);
					uint ebx = (W3TranslateMap[varC + i] & 0x0fu) << (int)esi;
					keyTable[j] = (ebx | (~(0x0fu << (int)esi)) & keyTable[j]);
				} while((i -= 16) >= 0);

				// pass 2

				for(i = 0; i < 4; i++)
					copy[i] = keyTable[i];

				for(edi = 0; edi < 120; edi++)
				{
					uint location = 12;
					uint eax = edi & 0x1f;
					ecx = esi & 0x1f;
					uint edx = 3 - (edi >> 5);

					location -= ((esi >> 5) << 2);
					location /= 4;
					// location can only be 0, 1, 2, or 3
					// otherwise it would be reaching into memory
					// beyond the "copy" identifier.
#if DEBUG
					Debug.Assert(location <= 3 && location >= 0, "location invalid", "Location can only be 0, 1, 2, or 3.");
#endif
					ebp = copy[location];

					ebp &= (1u << (int)ecx);
					ebp >>= (int)ecx;

					uint cktTmp = keyTable[edx];
					keyTable[edx] = ebp & 1;
					keyTable[edx] <<= (int)eax;
					keyTable[edx] |= ((~(1u << (int)eax)) & cktTmp);
					esi += 0x0b;
					if(esi >= 120)
						esi -= 120;
				}
			}
		}

// ReSharper disable InconsistentNaming
		private static uint SWAP4(uint num)
// ReSharper restore InconsistentNaming
		{
			return ((((num) >> 24) & 0x000000FF) | (((num) >> 8) & 0x0000FF00) | (((num) << 8) & 0x00FF0000) |
			        (((num) << 24) & 0xFF000000));
		}

// ReSharper disable InconsistentNaming
		private static ushort SWAP2(ushort num) { return (ushort)((((num) >> 8) & 0x00FF) | (((num) << 8) & 0xFF00)); }
// ReSharper restore InconsistentNaming

		#endregion

		#region calculate hash

		private void CalculateHash(uint clientToken, uint serverToken)
		{
			if(!valid)
				throw new InvalidOperationException();

			using(var ms = new MemoryStream(26))
			{
				var bw = new BinaryWriter(ms);
				bw.Write(clientToken);
				bw.Write(serverToken);

				switch(Key.Length)
				{
					case 13:
					case 16:
						bw.Write(product);
						bw.Write(val1);
						bw.Write(0);
						bw.Write(val2);
						bw.Write((short)0);

						hash = XSha1.Hash(ms.GetBuffer());
						break;
					case 26:
						bw.Write(product);
						bw.Write(val1);
						bw.Write(val2);
						byte[] buffer = ms.GetBuffer();
						using(SHA1 sha = new SHA1Managed())
							hash = sha.ComputeHash(buffer);
						break;
					default:
						break;
				}
			}
		}

		#endregion

		public override string ToString() { return inputKey; }
		public static implicit operator string(CDKey key) { return key.ToString(); }
	}

	public enum CDKeyProduct
	{
		None = 0x0,
		StarCraft = 0x1,
		StarCraft_Type2 = 0x2,
		StarCraft_Type3 = 0x17,
		DiabloIIClassic = 0x6,
		DiabloIIClassic_Type2 = 0x18,
		DiabloIIExpansion = 0xA,
		DiabloIIExpansion_Type2 = 0x19,
		Warcraft2 = 0x4,
		Warcraft3 = 0xE,
		Warcraft3Expansion = 0x12
	}
}
