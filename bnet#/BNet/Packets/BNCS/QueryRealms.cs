using System.Collections.Generic;

namespace BNet.BNCS.Outgoing
{
	public sealed class QueryRealms : BNCSPacket
	{
		public QueryRealms() : base(BNCSPacketId.QueryRealms2) { }
	}
}

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.QueryRealms2)]
	public sealed class QueryRealms : BNCSPacket
	{
		private QueryRealms(byte[] bytes) : base(BNCSPacketId.QueryRealms2)
		{
			SeedBytes(bytes);

			Realms = new List<RealmInfo>();
			Unknown = Read<uint>();
			var count = Read<uint>();
			for(uint i = 0; i < count; i++)
				Realms.Add(new RealmInfo(Read<uint>(), Read(), Read()));
		}

		public uint Unknown { get; private set; }
		public List<RealmInfo> Realms { get; private set; }
		public static QueryRealms Parse(byte[] bytes) { return new QueryRealms(bytes); }
	}
}

namespace BNet.BNCS
{
	public struct RealmInfo
	{
		public RealmInfo(uint unknown, string title, string description) : this()
		{
			Unknown = unknown;
			Title = title.Trim();
			Description = description.Trim();
		}

		public uint Unknown { get; private set; }
		public string Title { get; private set; }
		public string Description { get; private set; }

		public override string ToString() { return Title; }
		public static implicit operator string(RealmInfo realm) { return realm.ToString(); }
	}
}
