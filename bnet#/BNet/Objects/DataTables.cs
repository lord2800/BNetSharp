namespace BNet.Objects
{
	using System;
	using Utilities;

	[Serializable]
	public class ItemData : ITableRow
	{
		[Column("name")] public string Name { get; set; }
		[Column("type")] public string Type { get; set; }
		[Column("version")] public uint Version { get; set; }
		[Column("code")] public string Code { get; set; }
		[Column("levelreq")] public uint RequiredLevel { get; set; }

		[Column("durability")] public uint Durability { get; set; }
		[Column("durwarning")] public uint DurabilityWarning { get; set; }
		[Column("nodurability")] public bool NoDurability { get; set; }

		[Column("invheight")] public uint Height { get; set; }
		[Column("invwidth")] public uint Width { get; set; }

		[Column("stackable")] public bool Stackable { get; set; }
		[Column("useable")] public bool Useable { get; set; }
		[Column("qntwarning")] public uint QuantityWarning { get; set; }

		[Column("StrBonus")] public uint StrengthBonus { get; set; }
		[Column("DexBonus")] public uint DexterityBonus { get; set; }

	}

	[Serializable]
	public class WeaponData : ItemData
	{
		[Column("maxdam")] public uint MaximumDamage { get; set; }
		[Column("mindam")] public uint MinimumDamage { get; set; }
		[Column("maxmisdam")] public uint MissileMaximumDamage { get; set; }
		[Column("minmisdam")] public uint MissileMinimumDamage { get; set; }
		[Column("2handmaxdam")] public uint TwoHMaximumDamage { get; set; }
		[Column("2handmindam")] public uint TwoHMinimumDamage { get; set; }

		[Column("1or2handed")] public bool OneHanded { get; set; }
		[Column("2handed")] public bool TwoHanded { get; set; }

		[Column("quivered")] public bool Quivered { get; set; }

		[Column("rangeadder")] public uint Range { get; set; }
		[Column("speed")] public int SpeedPenalty { get; set; }
		[Column("reqstr")] public uint RequiredStrength { get; set; }
		[Column("reqdex")] public uint RequiredDexterity { get; set; }
	}

	[Serializable]
	public class ArmorData : ItemData
	{
		[Column("block")] public uint BlockRate { get; set; }

		[Column("maxac")] public uint MaxArmor { get; set; }
		[Column("minac")] public uint MinArmor { get; set; }

		[Column("reqstr")] public uint RequiredStrength { get; set; }

		[Column("speed")] public int SpeedPenalty { get; set; }
	}

	[Serializable]
	public class MiscItemData : ItemData
	{
		[Column("belt")] public bool Beltable { get; set; }
		[Column("autobelt")] public bool AutoBelt { get; set; }
		[Column("PermStoreItem")] public bool AlwaysInStore { get; set; }
		[Column("multibuy")] public bool MultiBuy { get; set; }
	}

	[Serializable]
	public class MonsterData : ITableRow
	{
		[Column("hcIdx")] public string Code { get; set; }
		[Column("Id")] public string Id { get; set; }
		[Column("MonType")] public string MonsterType { get; set; }

		[ColumnId("")] public Resistance NormalRes { get; set; }
		[ColumnId("(N)")] public Resistance NightmareRes { get; set; }
		[ColumnId("(H)")] public Resistance HellRes { get; set; }

		[Serializable]
		public struct Resistance
		{
			[Column("ResDm{0}")] public int Physical { get; set; }
			[Column("ResMa{0}")] public int Magical { get; set; }
			[Column("ResCo{0}")] public int Cold { get; set; }
			[Column("ResPo{0}")] public int Poison { get; set; }
			[Column("ResFi{0}")] public int Fire { get; set; }
			[Column("ResLi{0}")] public int Lightning { get; set; }

		} ;
	}

	[Serializable]
	public class StatData : ITableRow
	{
		[Column("ID")] public int Id { get; set; }
		[Column("Stat")] public string Name { get; set; }
		[Column("Encode")] public int Encode { get; set; }

		[Column("Signed")] public bool Signed { get; set; }
		[Column("Add")] public int Add { get; set; }
		[Column("Save Add")] public int SaveAdd { get; set; }

		[Column("Send Bits")] public byte SendBits { get; set; }
		[Column("Send Param Bits")] public byte SendParamBits { get; set; }
		[Column("Save Bits")] public byte SaveBits { get; set; }
		[Column("Save Param Bits")] public byte SaveParamBits { get; set; }

		[Column("op")] public byte Op { get; set; }
		[Column("op param")] public string OpParam { get; set; }
		[Column("op base")] public string OpBase { get; set; }
		[Column("op stat1")] public string OpStat1 { get; set; }
		[Column("op stat2")] public string OpStat2 { get; set; }
		[Column("op stat3")] public string OpStat3 { get; set; }

		[Column("direct")] public bool HasCap { get; set; }
		[Column("maxstat")] public string CapStat { get; set; }
		[Column("itemspecific")] public bool AppliesOnlyToOwner { get; set; }
	}
}
