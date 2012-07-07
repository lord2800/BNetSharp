using System;
using System.Runtime.Serialization;

namespace BNet.Exceptions
{
	[Serializable]
	public class IPBannedException : Exception
	{
		public IPBannedException() { }
		protected IPBannedException(SerializationInfo info,
		                            StreamingContext context) : base(info, context) { }
	}
}
