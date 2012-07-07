using System;
using System.Runtime.Serialization;

namespace Utilities
{
	[Serializable]
	public class InvalidPacketDefinitionException : Exception
	{
		public InvalidPacketDefinitionException(string typeName) : base("The packet processor " + typeName + " is invalid.") { }
		public InvalidPacketDefinitionException(string typeName, Exception inner) : base("The packet processor " + typeName
																		+ " is invalid.", inner) { }

		public InvalidPacketDefinitionException(Type type) : base(type.FullName) { }
		public InvalidPacketDefinitionException(Type type, Exception inner) : base(type.FullName, inner) { }

		protected InvalidPacketDefinitionException(SerializationInfo info,
												   StreamingContext context) : base(info, context) { }
	}
}