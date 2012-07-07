using System;
using System.Collections.Generic;
using System.Text;

namespace BNet.MCP.Outgoing
{
	public sealed class RequestCharList : MCPPacket
	{
		public RequestCharList(uint count) : base(MCPPacketId.CharList2)
		{
			if(count > 8)
				throw new ArgumentOutOfRangeException("count", "Can't request more than 8 characters at a time!");

			Write(count);
		}
	}
}

namespace BNet.MCP.Incoming
{
	[MCPPacketHandler(MCPPacketId.CharList2)]
	public sealed class RequestCharList : MCPPacket
	{
		private RequestCharList(byte[] bytes) : base(MCPPacketId.CharList2)
		{
			SeedBytes(bytes);

			Characters = new List<CharInfo>();
			/*ushort requestCount = */ Read<ushort>();
			TotalAccountChars = Read<uint>();
			TotalChars = Read<ushort>();

			for(ushort i = 0; i < TotalChars; i++)
				Characters.Add(new CharInfo(Read<uint>(), Read(), Read()));
		}

		public uint TotalAccountChars { get; private set; }
		public ushort TotalChars { get; private set; }
		public List<CharInfo> Characters { get; private set; }
		public static RequestCharList Parse(byte[] bytes) { return new RequestCharList(bytes); }
	}
}

namespace BNet.MCP
{
	public struct CharInfo
	{
		public CharInfo(uint expires, string name, string statstring) : this()
		{
			Expires = new DateTime(1970, 1, 1).ToUniversalTime().AddSeconds(expires);
			Name = name;
			StatString = statstring;

			byte[] stats = Encoding.ASCII.GetBytes(statstring);
			Headgear = stats[2];
			Armor = stats[3];
			Legs = stats[4];
			RightArm = stats[5];
			LeftArm = stats[6];
			RightWeapon = stats[7];
			LeftWeapon = stats[8];
			LeftShield = stats[9];
			RightShoulder = stats[10];
			LeftShoulder = stats[11];
			LeftItem = stats[12];

			Class = (CharClass)stats[13];
			Level = stats[25];
			Flags = (CharFlags)stats[26];
			CurrentAct = stats[27];
			Ladder = stats[30];
		}

		public DateTime Expires { get; private set; }
		public string Name { get; private set; }
		public string StatString { get; private set; }

		public byte Headgear { get; private set; }
		public byte Armor { get; private set; }
		public byte Legs { get; private set; }
		public byte RightArm { get; private set; }
		public byte LeftArm { get; private set; }
		public byte RightWeapon { get; private set; }
		public byte LeftWeapon { get; private set; }
		public byte LeftShield { get; private set; }
		public byte RightShoulder { get; private set; }
		public byte LeftShoulder { get; private set; }
		public byte LeftItem { get; private set; }

		public CharClass Class { get; private set; }
		public byte Level { get; private set; }
		public CharFlags Flags { get; private set; }
		public byte CurrentAct { get; private set; }
		public byte Ladder { get; private set; }

		public override string ToString() { return Name; }
		public static implicit operator string(CharInfo chr) { return chr.ToString(); }
	}

	public enum CharClass : byte
	{
		Unavailable = 0x0,
		Amazon = 0x01,
		Sorceress = 0x02,
		Necromancer = 0x03,
		Paladin = 0x04,
		Barbarian = 0x05,
		Druid = 0x06,
		Assassin = 0x07
	}

	[Flags]
	public enum CharFlags : byte
	{
		None = 0,
		Hardcore = 0x04,
		Dead = 0x08,
		Expansion = 0x20
	}
}
