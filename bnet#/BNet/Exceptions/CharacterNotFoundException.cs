using System;
using System.Runtime.Serialization;

namespace BNet.Exceptions
{
	[Serializable]
	public class CharacterNotFoundException : Exception
	{
		public CharacterNotFoundException(string charName) : base("The selected character, " + charName + ", was not found.") { }
		public CharacterNotFoundException(string charName, Exception inner) : base("The selected character, " + charName +
		                                                                           ", was not found.", inner) { }

		protected CharacterNotFoundException(SerializationInfo info,
		                                     StreamingContext context) : base(info, context) { }
	}
}
