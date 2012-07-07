using System;
using System.Runtime.Serialization;

namespace BNet.Exceptions
{
	[Serializable]
	public class UnknownPacketException : Exception
	{
		public UnknownPacketException() : base("This packet format is unknown.") { }
		protected UnknownPacketException(SerializationInfo info,
		                                 StreamingContext context) : base(info, context) { }
	}
}
