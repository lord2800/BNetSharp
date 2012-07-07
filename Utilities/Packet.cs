using System.Linq;

namespace Utilities
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;

	public class PacketBuilder<T> where T : Attribute, IPacketHandler
	{
		public delegate Packet Parser(byte[] bytes);

		private readonly List<Type> processedTypes = new List<Type>();
		protected Dictionary<byte, KeyValuePair<Type, Parser>> processors = new Dictionary<byte, KeyValuePair<Type, Parser>>();

		public PacketBuilder() { ProcessTypes(); }

		private void ProcessTypes()
		{
			// only scan this T's assembly because it has all the packet definitions
			ProcessTypes(Assembly.GetAssembly(typeof(T)));
		}

		public virtual void ProcessTypes(params Assembly[] assemblies)
		{
			foreach(var type in assemblies.SelectMany(a => a.GetTypes().Where(t => !processedTypes.Contains(t))))
			{
				processedTypes.Add(type);

				if(!typeof(Packet).IsAssignableFrom(type))
					continue;

				var attrs = type.GetAttributes<T>();
				if(attrs == null || attrs.Length == 0)
					continue;

				foreach(T attr in attrs)
				{
					if(processors.ContainsKey(attr.HeaderId))
						throw new Exception("{0} is already handled by {1}".Compose(attr.HeaderId, processors[attr.HeaderId].Key.FullName));

					var method = type.GetMethod("Parse");
					if(method == null)
						throw new InvalidPacketDefinitionException(type);

					var parser = (Parser)Delegate.CreateDelegate(typeof(Parser), method);
					processors.Add(attr.HeaderId, new KeyValuePair<Type, Parser>(type, parser));
				}
			}
		}

		protected Type ProcessType(byte header)
		{
			if(!processors.ContainsKey(header))
				throw new Exception("Failed to find parser for 0x{0:X}".Compose(header));
			return processors[header].Key;
		}

		public virtual Packet Parse(BufferedStream stream) { return null; }
	}

	public abstract class Packet : IDisposable
	{
		public virtual void Dispose() { }

		public virtual byte[] ToByteArray() { throw new NotSupportedException(); }
		public static implicit operator byte[](Packet p) { return p.ToByteArray(); }
		public static implicit operator string(Packet p) { return p.ToString(); }
	}

	public class PacketEventArgs : EventArgs
	{
		public PacketEventArgs(Packet value) { Packet = value; }
		public Packet Packet { get; set; }
	}

	public interface IPacketHandler
	{
		byte HeaderId { get; }
	}
}
