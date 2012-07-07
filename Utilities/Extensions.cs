using System.Runtime.InteropServices;

namespace System
{
	using IO;
	using Text;
	using Net.Sockets;
	using System.Reflection;

	public static class Extensions
	{
		/// <summary>
		/// Calls String.Format with the current string as the formatting string
		/// </summary>
		/// <returns>The formatted string</returns>
		public static string Compose(this string format, params object[] args) { return String.Format(format, args); }

		/// <summary>
		/// Detects when a socket is remotely closed
		/// </summary>
		/// <returns>True if open, false if closed</returns>
		public static bool IsActive(this Socket socket)
		{
			try
			{
				return !(socket.Poll(1, SelectMode.SelectRead) && (socket.Available == 0));
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Reads a number of bytes from the BufferedStream equal to the size of the byte buffer passed in
		/// </summary>
		/// <param name="stream">The stream to read from</param>
		/// <param name="buffer">The buffer of bytes to fill with stream data</param>
		/// <exception cref="System.IO.EndOfStreamException">Thrown if the end of the stream is reached before satisfying the request.</exception>
		public static void BlockRead(this Stream stream, byte[] buffer)
		{
			int index = 0,
			    size = buffer.Length;
			do
			{
				var read = stream.Read(buffer, index, size);
				if(read == 0)
					throw new EndOfStreamException();
				size -= read;
				index += read;
			} while(size > 0);
		}

		/// <summary>
		/// Converts an enum value to a "friendly" printable name
		/// </summary>
		/// <returns>The enum value with spaces inserted inbetween CamelCased name values</returns>
		public static string AsFriendly(this Enum thing)
		{
			var result = new StringBuilder();
			var value = thing.ToString();
			result.Append(value[0]);
			result.Append(value[1]);
			for(var i = 2; i < value.Length; i++)
			{
				if((Char.IsLower(value[i - 1]) && Char.IsUpper(value[i])) || Char.IsDigit(value[i]))
					result.Append(' ');
				result.Append(value[i]);
			}
			return result.ToString();
		}
		public static bool HasFlag(this Enum thing, Enum flag)
		{
			return ((uint)(object)thing & (uint)(object)flag) == (uint)(object)flag;
		}

		/// <summary>
		/// Convert a value to any arbitrary type
		/// </summary>
		/// <param name="t">The type to convert to</param>
		/// <param name="value">The value to convert</param>
		/// <returns>The converted value</returns>
		public static object CastTo(this Type t, string value)
		{
			const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
			const string MissingConverterMsg = "Can't find converter for value \"{0}\" to type {1} using {1}.Parse";
			const string ConversionErrorMsg = "Can't convert value \"{0}\" to type {1} using {2}.{3}";

			// if the type is just string, stuff it in there
			if(String.Compare(t.Name, "String", StringComparison.OrdinalIgnoreCase) == 0)
				return value;
			else
			{
				if(String.Compare(t.Name, "Boolean", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if(value == "0" || String.Compare(value, Boolean.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
						value = Boolean.FalseString;
					else if(value == "1" || String.Compare(value, Boolean.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
						value = Boolean.TrueString;
				}
				// check if the type has its own conversion for it
				var method = t.GetMethod("Parse", PublicStatic, null, CallingConventions.Any,
										new[] { typeof(string) }, null);
				if(method == null)
				{
					// no? then check if Convert has a method for it
					method = typeof(Convert).GetMethod("To" + t.Name, PublicStatic, null,
										CallingConventions.Any, new[] {typeof(string)}, null);
					if(method == null)
					{
						// still no dice? just give up
						throw new InvalidCastException(MissingConverterMsg.Compose(value, t.FullName));
					}
				}

				try
				{
					return method.Invoke(null, new object[] {value});
				}
				catch
				{
					throw new InvalidCastException(ConversionErrorMsg.Compose(value, t.FullName,
															method.DeclaringType.FullName, method.Name));
				}
			}
		}

		/// <summary>
		/// Test for the presence of an attribute on a specified Type
		/// </summary>
		/// <param name="t">The Type to test</param>
		/// <param name="attr">The type of the attribute to test for</param>
		/// <returns>True if the attribute is present, otherwise false</returns>
		public static bool HasAttribute(this ICustomAttributeProvider t, Type attr) { return t.HasAttribute(attr, false); }

		/// <summary>
		/// Test for the presence of an attribute on a specified Type
		/// </summary>
		/// <param name="t">The Type to test</param>
		/// <param name="attr">The type of the attribute to test for</param>
		/// <param name="inherit">Whether or not to examine the inherited attributes for the specified attribute</param>
		/// <returns>True if the attribute is present, otherwise false</returns>
		public static bool HasAttribute(this ICustomAttributeProvider t, Type attr, bool inherit)
		{
			var result = t.GetCustomAttributes(attr, inherit);
			return result != null && result.Length > 0;
		}

		/// <summary>
		/// Test for and return the first attribute of the specified type
		/// </summary>
		/// <typeparam name="T">The Attribute to test for</typeparam>
		/// <param name="t">The Type to test</param>
		/// <returns>The first Attribute matching the specified type, if one exists, otherwise null</returns>
		public static T GetAttribute<T>(this ICustomAttributeProvider t) where T : Attribute
		{
			return t.GetAttribute<T>(false);
		}

		/// <summary>
		/// Test for and return the first attribute of the specified type
		/// </summary>
		/// <typeparam name="T">The Attribute to test for</typeparam>
		/// <param name="t">The Type to test</param>
		/// <param name="inherit">Whether or not to examine the inherited attributes for the specified type</param>
		/// <returns>The first Attribute matching the specified type, if one exists, otherwise null</returns>
		public static T GetAttribute<T>(this ICustomAttributeProvider t, bool inherit) where T : Attribute
		{
			return t.HasAttribute(typeof(T), inherit) ? t.GetCustomAttributes(typeof(T), inherit)[0] as T : (T)null;
		}

		/// <summary>
		/// Test for and return the first attribute of the specified type
		/// </summary>
		/// <typeparam name="T">The Attribute to test for</typeparam>
		/// <param name="t">The Type to test</param>
		/// <returns>The first Attribute matching the specified type, if one exists, otherwise null</returns>
		public static T[] GetAttributes<T>(this ICustomAttributeProvider t) where T : Attribute
		{
			return t.GetAttributes<T>(false);
		}

		/// <summary>
		/// Test for and return the first attribute of the specified type
		/// </summary>
		/// <typeparam name="T">The Attribute to test for</typeparam>
		/// <param name="t">The Type to test</param>
		/// <param name="inherit">Whether or not to examine the inherited attributes for the specified type</param>
		/// <returns>The first Attribute matching the specified type, if one exists, otherwise null</returns>
		public static T[] GetAttributes<T>(this ICustomAttributeProvider t, bool inherit) where T : Attribute
		{
			return t.HasAttribute(typeof(T), inherit) ? t.GetCustomAttributes(typeof(T), inherit) as T[] : (T[])null;
		}

		public static T PinAndCast<T>(this Array o) where T : new()
		{
			var handle = System.Runtime.InteropServices.GCHandle.Alloc(o, GCHandleType.Pinned);
			T result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();
			return result;
		}
	}
}
