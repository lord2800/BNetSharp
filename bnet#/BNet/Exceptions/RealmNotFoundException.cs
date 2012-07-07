using System;
using System.Runtime.Serialization;

namespace BNet.Exceptions
{
	[Serializable]
	public class RealmNotFoundException : Exception
	{
		public RealmNotFoundException(string realmName) : base("The selected realm, " + realmName + ", was not found.") { }
		public RealmNotFoundException(string realmName, Exception inner) : base("The selected realm, " + realmName +
		                                                                        ", was not found.", inner) { }

		protected RealmNotFoundException(SerializationInfo info,
		                                 StreamingContext context) : base(info, context) { }
	}
}
