using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace BNet
{
	public abstract class BinFile<T> where T : struct
	{
		public abstract IEnumerable<T> Entries { get; set; }
		public abstract IEnumerable<ITableRow> ToTable();
	}

	#region flags
	[Flags]
	public enum ItemStatCostFlags
	{
		None = 0x0,
		sendOther = 0x1,
		signed = 0x2,
		damagerelated = 0x4,
		itemspecific = 0x8,
		direct = 0x10,
		updateanimrate = 0x200,
		fmin = 0x400,
		fcallback = 0x800,
		saved = 0x1000,
		csvsigned = 0x2000,
	}

	[Flags]
	public enum MonstatsFlags : uint
	{
		isSpawn			= 0x0,
		isMelee			= 0x1,
		noRatio			= 0x2,
		opendoors		= 0x4,
		SetBoss			= 0x8,
		BossXfer		= 0x10,
		boss			= 0x20,
		primeevil		= 0x40,
		npc				= 0x80,
		interact		= 0x100,
		inTown			= 0x200,
		lUndead			= 0x400,
		hUndead			= 0x800,
		demon			= 0x1000,
		flying			= 0x2000,
		killable		= 0x4000,
		switchai		= 0x8000,
		nomultishot		= 0x10000,
		neverCount		= 0x20000,
		petIgnore		= 0x40000,
		deathDmg		= 0x80000,
		genericSpawn	= 0x100000,
		zoo				= 0x200000,
		placespawn		= 0x400000,
		inventory		= 0x800000,
		enabled			= 0x1000000,
		NoShldBlock		= 0x2000000,
		noaura			= 0x4000000,
		rangedtype		= 0x8000000,
	}

	[Flags]
	public enum SkillsFlags1
	{
		decquant		= 0x0,
		lob				= 0x1,
		progressive		= 0x2,
		finishing		= 0x4,
		passive			= 0x8,
		aura			= 0x10,
		periodic		= 0x20,
		prgstack		= 0x40,
		InTown			= 0x80,
		Kick			= 0x100,
		InGame			= 0x200,
		repeat			= 0x400,
		stsuccessonly	= 0x800,
		stsounddelay	= 0x1000,
		weaponsnd		= 0x2000,
		immediate		= 0x4000,
		noammo			= 0x8000,
		enhanceable		= 0x10000,
		durability		= 0x20000,
		UseAttackRate	= 0x40000,
		TargetableOnly	= 0x80000,
		SearchEnemyXY	= 0x100000,
		SearchEnemyNear	= 0x200000,
		SearchOpenXY	= 0x400000,
		TargetCorpse	= 0x800000,
		TargetPet		= 0x1000000,
		TargetAlly		= 0x2000000,
		TargetItem		= 0x4000000,
		AttackNoMana	= 0x8000000,
		ItemTgtDo		= 0x10000000,
		leftskill		= 0x20000000,
		interrupt		= 0x40000000,
	}

	[Flags]
	public enum SkillsFlags2
	{
		TgtPlaceCheck		= 0x0,
		ItemCheckStart		= 0x1,
		ItemCltCheckStart	= 0x2,
		general				= 0x4,
		scroll				= 0x8,
		usemanaondo			= 0x10,
		warp				= 0x20,
	}

	[Flags]
	public enum MissilesFlags
	{
		LastCollide		= 0x0,
		Explosion		= 0x1,
		Pierce			= 0x2,
		CanSlow			= 0x4,
		CanDestroy		= 0x8,
		ClientSend		= 0x10,
		GetHit			= 0x20,
		SoftHit			= 0x40,
		ApplyMastery	= 0x80,
		ReturnFire		= 0x100,
		Town			= 0x200,
		SrcTown			= 0x400,
		NoMultiShot		= 0x800,
		NoUniqueMod		= 0x1000,
		Half2HSrc		= 0x2000,
		MissileSkill	= 0x4000,
	}

	[Flags]
	public enum Monstats2Flags1
	{
		noGfxHitTest	= 0x0,
		noMap			= 0x1,
		noOvly			= 0x2,
		isSel			= 0x4,
		alSel			= 0x8,
		noSel			= 0x10,
		shiftSel		= 0x20,
		corpseSel		= 0x40,
		revive			= 0x80,
		isAtt			= 0x100,
		small			= 0x200,
		large			= 0x400,
		soft			= 0x800,
		critter			= 0x1000,
		shadow			= 0x2000,
		noUniqueShift	= 0x4000,
		compositeDeath	= 0x8000,
		inert			= 0x10000,
		objCol			= 0x20000,
		deadCol			= 0x40000,
		unflatDead		= 0x80000,
	}

	[Flags]
	public enum Monstats2Flags2
	{
		HD		= 0x0,
		TR		= 0x1,
		LG		= 0x2,
		RA		= 0x4,
		LA		= 0x8,
		RH		= 0x10,
		LH		= 0x20,
		SH		= 0x40,
		S1		= 0x80,
		S2		= 0x100,
		S3		= 0x200,
		S4		= 0x400,
		S5		= 0x800,
		S6		= 0x1000,
		S7		= 0x2000,
		S8		= 0x4000,
	}

	[Flags]
	public enum Monstats2Flags3
	{
		mDT		= 0x0,
		mNU		= 0x1,
		mWL		= 0x2,
		mGH		= 0x4,
		mA1		= 0x8,
		mA2		= 0x10,
		mBL		= 0x20,
		mSC		= 0x40,
		mS1		= 0x80,
		mS2		= 0x100,
		mS3		= 0x200,
		mS4		= 0x400,
		mDD		= 0x800,
		mKB		= 0x1000,
		mSQ		= 0x2000,
		mRN		= 0x4000,
	}

	[Flags]
	public enum Monstats2Flags4
	{
		A1mv	= 0x0,
		A2mv	= 0x1,
		SCmv	= 0x2,
		S1mv	= 0x4,
		S2mv	= 0x8,
		S3mv	= 0x10,
		S4mv	= 0x20,
	}

	[Flags]
	public enum UniqueItemsFlags
	{
		enabled = 0x0,
		nolimit = 0x1,
		carry1 = 0x2,
		ladder = 0x4,
	}

	[Flags]
	public enum CubeItemFlags : byte
	{
		inputItemItemCodeFlag = 0x0,
		inputItemItemClassFlag = 0x1,
		inputItemAnyFlag = 0x2,
		inputItemSocketFlag = 0x4,
		inputItemNoEtherealFlag = 0x8,
		inputItemRepairFlag = 0x10,
		inputItemUniqueItemNameFlag = 0x20,
		inputItemUpgFlag = 0x40,
	}

	[Flags]
	public enum PetTypeFlags
	{
		warp = 0x0,
		range = 0x1,
		partysend = 0x2,
		unsummon = 0x4,
		automap = 0x8,
		drawhp = 0x10,
	}
	#endregion

	#region tested
	public class ItemStatCostTable
	{
		public List<ItemStatCostEntry> entries = new List<ItemStatCostEntry>();
		public IEnumerable<ItemStatCostEntry> Entries { get { return entries; } }

		public ItemStatCostTable(byte[] bytes)
		{
			uint count = BitConverter.ToUInt32(bytes, 0);
			bytes = bytes.Skip(4).ToArray();
			int size = Marshal.SizeOf(typeof(ItemStatCostEntry));
			for(uint i = 0; i < count; i++)
			{
				var bits = bytes.Take(size).ToArray();
				entries.Add(bits.PinAndCast<ItemStatCostEntry>());
				bytes = bytes.Skip(size).ToArray();
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct ItemStatCostEntry
		{
			[FieldOffset(0x0)] public short stat;
			[FieldOffset(0x4)] public ItemStatCostFlags flags;
			[FieldOffset(0x8)] public byte sendBits;
			[FieldOffset(0x9)] public byte sendParamBits;
			[FieldOffset(0xa)] public byte csvbits;
			[FieldOffset(0xb)] public byte csvparam;
			[FieldOffset(0xc)] public uint divide;
			[FieldOffset(0x10)] public uint multiply;
			[FieldOffset(0x14)] public uint add;
			[FieldOffset(0x18)] public byte valshift;
			[FieldOffset(0x19)] public byte saveBits;
			[FieldOffset(0x1a)] public byte _1_09_saveBits;
			[FieldOffset(0x1c)] public uint saveAdd;
			[FieldOffset(0x20)] public uint _1_09_saveAdd;
			[FieldOffset(0x24)] public uint saveParamBits;
			[FieldOffset(0x2c)] public uint minaccr;
			[FieldOffset(0x30)] public byte encode;
			[FieldOffset(0x32)] public short maxstat;
			[FieldOffset(0x34)] public short descpriority;
			[FieldOffset(0x36)] public byte descfunc;
			[FieldOffset(0x37)] public byte descval;
			[FieldOffset(0x38)] public short descstrpos;
			[FieldOffset(0x3a)] public short descstrneg;
			[FieldOffset(0x3c)] public short descstr2;
			[FieldOffset(0x3e)] public short dgrp;
			[FieldOffset(0x40)] public byte dgrpfunc;
			[FieldOffset(0x41)] public byte dgrpval;
			[FieldOffset(0x42)] public short dgrpstrpos;
			[FieldOffset(0x44)] public short dgrpstrneg;
			[FieldOffset(0x46)] public short dgrpstr2;
			[FieldOffset(0x48)] public short itemevent1;
			[FieldOffset(0x4a)] public short itemevent2;
			[FieldOffset(0x4c)] public short itemeventfunc1;
			[FieldOffset(0x4e)] public short itemeventfunc2;
			[FieldOffset(0x50)] public byte keepzero;
			[FieldOffset(0x54)] public byte op;
			[FieldOffset(0x55)] public byte opParam;
			[FieldOffset(0x56)] public short opBase;
			[FieldOffset(0x58)] public short opStat1;
			[FieldOffset(0x5a)] public short opStat2;
			[FieldOffset(0x5c)] public short opStat3;
			[FieldOffset(0x140)] public uint stuff;
		}
	}

	public class ExperienceTable
	{
		public LevelEntry MaxLevels;

		public List<LevelEntry> entries = new List<LevelEntry>();
		public IEnumerable<LevelEntry> Level { get { return entries; } }

		public ExperienceTable(byte[] bytes)
		{
			uint count = BitConverter.ToUInt32(bytes, 0)-1;
			bytes = bytes.Skip(4).ToArray();
			int size = Marshal.SizeOf(typeof(LevelEntry));

			var maxlevels = bytes.Take(size).ToArray();
			MaxLevels = maxlevels.PinAndCast<LevelEntry>();
			bytes = bytes.Skip(size).ToArray();

			for(uint i = 0; i < count; i++)
			{
				var bits = bytes.Take(size).ToArray();
				entries.Add(bits.PinAndCast<LevelEntry>());
				bytes = bytes.Skip(size).ToArray();
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LevelEntry
		{
			public uint Amazon;
			public uint Sorceress;
			public uint Necromancer;
			public uint Paladin;
			public uint Barbarian;
			public uint Druid;
			public uint Assassin;
			public uint ExpRatio;
		}
	}

	public class ItemTable
	{
		public ItemTable(byte[] bytes)
		{
			uint count = BitConverter.ToUInt32(bytes, 0);
			int size = Marshal.SizeOf(typeof(ItemEntry));
			bytes = bytes.Skip(4).ToArray();
			for(uint i = 0; i < count; i++)
			{
				var bits = bytes.Take(size).ToArray();
				items.Add(bits.PinAndCast<ItemEntry>());
				bytes = bytes.Skip(size).ToArray();
			}
		}

		private List<ItemEntry> items = new List<ItemEntry>();
		public IEnumerable<ItemEntry> Items { get { return items; } }

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
		public struct ItemEntry
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
			public string flippyfile; // type: DATA_ASCII length: 0x1f offset: 0x0
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
			public string invfile; // type: DATA_ASCII length: 0x1f offset: 0x20
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
			public string uniqueinvfile; // type: DATA_ASCII length: 0x1f offset: 0x40
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
			public string setinvfile; // type: DATA_ASCII length: 0x1f offset: 0x60
			public uint code; // type: ASCII_TO_CODE length: 0x0 offset: 0x80
			public uint normcode; // type: DATA_RAW length: 0x0 offset: 0x84
			public uint ubercode; // type: DATA_RAW length: 0x0 offset: 0x88
			public uint ultracode; // type: DATA_RAW length: 0x0 offset: 0x8c
			public uint alternategfx; // type: DATA_RAW length: 0x0 offset: 0x90
			public uint pSpell; // type: DATA_DWORD length: 0x0 offset: 0x94
			public short state; // type: NAME_TO_WORD length: 0x0 offset: 0x98
			public short cstate1; // type: NAME_TO_WORD length: 0x0 offset: 0x9a
			public short cstate2; // type: NAME_TO_WORD length: 0x0 offset: 0x9c
			public short stat1; // type: NAME_TO_WORD length: 0x0 offset: 0x9e
			public short stat2; // type: NAME_TO_WORD length: 0x0 offset: 0xa0
			public short stat3; // type: NAME_TO_WORD length: 0x0 offset: 0xa2
			public uint calc1; // type: CALC_TO_DWORD length: 0x0 offset: 0xa4
			public uint calc2; // type: CALC_TO_DWORD length: 0x0 offset: 0xa8
			public uint calc3; // type: CALC_TO_DWORD length: 0x0 offset: 0xac
			public uint len; // type: CALC_TO_DWORD length: 0x0 offset: 0xb0
			public short spelldesc; // type: DATA_BYTE length: 0x0 offset: 0xb4
			public short spelldescstr; // type: KEY_TO_WORD length: 0x0 offset: 0xb6
			public uint spelldesccalc; // type: CALC_TO_DWORD length: 0x0 offset: 0xb8
			public uint BetterGem; // type: DATA_RAW length: 0x0 offset: 0xbc
			public uint wclass; // type: DATA_RAW length: 0x0 offset: 0xc0
			public uint twohandedwclass; // type: DATA_RAW length: 0x0 offset: 0xc4
			public uint TMogType; // type: DATA_RAW length: 0x0 offset: 0xc8
			public uint minac; // type: DATA_DWORD length: 0x0 offset: 0xcc
			public uint maxac; // type: DATA_DWORD length: 0x0 offset: 0xd0
			public uint gambleCost; // type: DATA_DWORD length: 0x0 offset: 0xd4
			public uint speed; // type: DATA_DWORD_2 length: 0x0 offset: 0xd8
			public uint bitfield1; // type: DATA_DWORD length: 0x0 offset: 0xdc
			public uint cost; // type: DATA_DWORD length: 0x0 offset: 0xe0
			public uint minstack; // type: DATA_DWORD length: 0x0 offset: 0xe4
			public uint maxstack; // type: DATA_DWORD length: 0x0 offset: 0xe8
			public uint spawnstack; // type: DATA_DWORD length: 0x0 offset: 0xec
			public uint gemoffset; // type: DATA_DWORD_2 length: 0x0 offset: 0xf0
			public short namestr; // type: KEY_TO_WORD length: 0x0 offset: 0xf4
			public short version; // type: DATA_WORD length: 0x0 offset: 0xf6
			public short autoPrefix; // type: DATA_WORD length: 0x0 offset: 0xf8
			public short missiletype; // type: DATA_WORD length: 0x0 offset: 0xfa
			public byte rarity; // type: DATA_BYTE length: 0x0 offset: 0xfc
			public byte level; // type: DATA_BYTE length: 0x0 offset: 0xfd
			public byte mindam; // type: DATA_BYTE_2 length: 0x0 offset: 0xfe
			public byte maxdam; // type: DATA_BYTE_2 length: 0x0 offset: 0xff
			public byte minmisdam; // type: DATA_BYTE length: 0x0 offset: 0x100
			public byte maxmisdam; // type: DATA_BYTE length: 0x0 offset: 0x101
			public byte twohandmindam; // type: DATA_BYTE_2 length: 0x0 offset: 0x102
			public byte twohandmaxdam; // type: DATA_BYTE_2 length: 0x0 offset: 0x103
			public byte rangeadder; // type: DATA_BYTE_2 length: 0x0 offset: 0x104
			public short strbonus; // type: DATA_WORD length: 0x0 offset: 0x106
			public short dexbonus; // type: DATA_WORD length: 0x0 offset: 0x108
			public short reqstr; // type: DATA_WORD length: 0x0 offset: 0x10a
			public short reqdex; // type: DATA_WORD length: 0x0 offset: 0x10c
			public byte absorbs; // type: DATA_BYTE_2 length: 0x0 offset: 0x10e
			public byte invwidth; // type: DATA_BYTE length: 0x0 offset: 0x10f
			public byte invheight; // type: DATA_BYTE length: 0x0 offset: 0x110
			public byte block; // type: DATA_BYTE_2 length: 0x0 offset: 0x111
			public byte durability; // type: DATA_BYTE_2 length: 0x0 offset: 0x112
			public byte nodurability; // type: DATA_BYTE length: 0x0 offset: 0x113
			public byte missile; // type: DATA_BYTE_2 length: 0x0 offset: 0x114
			public byte component; // type: DATA_BYTE length: 0x0 offset: 0x115
			public byte rArm; // type: DATA_BYTE_2 length: 0x0 offset: 0x116
			public byte lArm; // type: DATA_BYTE_2 length: 0x0 offset: 0x117
			public byte torso; // type: DATA_BYTE_2 length: 0x0 offset: 0x118
			public byte legs; // type: DATA_BYTE_2 length: 0x0 offset: 0x119
			public byte rspad; // type: DATA_BYTE_2 length: 0x0 offset: 0x11a
			public byte lspad; // type: DATA_BYTE_2 length: 0x0 offset: 0x11b
			public byte twohanded; // type: DATA_BYTE_2 length: 0x0 offset: 0x11c
			public byte useable; // type: DATA_BYTE length: 0x0 offset: 0x11d
			public short type; // type: CODE_TO_WORD length: 0x0 offset: 0x11e
			public short type2; // type: CODE_TO_WORD length: 0x0 offset: 0x120
			public byte subtype; // type: DATA_BYTE_2 length: 0x0 offset: 0x122
			public short dropsound; // type: NAME_TO_WORD length: 0x0 offset: 0x124
			public short usesound; // type: NAME_TO_WORD length: 0x0 offset: 0x126
			public byte dropsfxframe; // type: DATA_BYTE length: 0x0 offset: 0x128
			public byte unique; // type: DATA_BYTE length: 0x0 offset: 0x129
			public byte quest; // type: DATA_BYTE length: 0x0 offset: 0x12a
			public byte questdiffcheck; // type: DATA_BYTE length: 0x0 offset: 0x12b
			public byte transparent; // type: DATA_BYTE length: 0x0 offset: 0x12c
			public byte transtbl; // type: DATA_BYTE length: 0x0 offset: 0x12d
			public byte lightradius; // type: DATA_BYTE length: 0x0 offset: 0x12f
			public byte belt; // type: DATA_BYTE length: 0x0 offset: 0x130
			public byte autobelt; // type: DATA_BYTE length: 0x0 offset: 0x131
			public byte stackable; // type: DATA_BYTE length: 0x0 offset: 0x132
			public byte spawnable; // type: DATA_BYTE length: 0x0 offset: 0x133
			public byte spellicon; // type: DATA_BYTE_2 length: 0x0 offset: 0x134
			public byte durwarning; // type: DATA_BYTE length: 0x0 offset: 0x135
			public byte qntwarning; // type: DATA_BYTE length: 0x0 offset: 0x136
			public byte hasinv; // type: DATA_BYTE_2 length: 0x0 offset: 0x137
			public byte gemsockets; // type: DATA_BYTE_2 length: 0x0 offset: 0x138
			public byte Transmogrify; // type: DATA_BYTE_2 length: 0x0 offset: 0x139
			public byte TMogMin; // type: DATA_BYTE_2 length: 0x0 offset: 0x13a
			public byte TMogMax; // type: DATA_BYTE_2 length: 0x0 offset: 0x13b
			public byte hitClass; // type: CODE_TO_BYTE length: 0x0 offset: 0x13c
			public byte oneortwohanded; // type: DATA_BYTE_2 length: 0x0 offset: 0x13d
			public byte gemapplytype; // type: DATA_BYTE length: 0x0 offset: 0x13e
			public byte levelreq; // type: DATA_BYTE length: 0x0 offset: 0x13f
			public byte magicLvl; // type: DATA_BYTE length: 0x0 offset: 0x140
			public byte Transform; // type: DATA_BYTE length: 0x0 offset: 0x141
			public byte InvTrans; // type: DATA_BYTE length: 0x0 offset: 0x142
			public byte compactsave; // type: DATA_BYTE_2 length: 0x0 offset: 0x143
			public byte SkipName; // type: DATA_BYTE length: 0x0 offset: 0x144
			public byte Nameable; // type: DATA_BYTE length: 0x0 offset: 0x145
			public byte AkaraMin; // type: DATA_BYTE length: 0x0 offset: 0x146
			public byte GheedMin; // type: DATA_BYTE length: 0x0 offset: 0x147
			public byte CharsiMin; // type: DATA_BYTE length: 0x0 offset: 0x148
			public byte FaraMin; // type: DATA_BYTE length: 0x0 offset: 0x149
			public byte LysanderMin; // type: DATA_BYTE length: 0x0 offset: 0x14a
			public byte DrognanMin; // type: DATA_BYTE length: 0x0 offset: 0x14b
			public byte HraltiMin; // type: DATA_BYTE length: 0x0 offset: 0x14c
			public byte AlkorMin; // type: DATA_BYTE length: 0x0 offset: 0x14d
			public byte OrmusMin; // type: DATA_BYTE length: 0x0 offset: 0x14e
			public byte ElzixMin; // type: DATA_BYTE length: 0x0 offset: 0x14f
			public byte AshearaMin; // type: DATA_BYTE length: 0x0 offset: 0x150
			public byte CainMin; // type: DATA_BYTE length: 0x0 offset: 0x151
			public byte HalbuMin; // type: DATA_BYTE length: 0x0 offset: 0x152
			public byte JamellaMin; // type: DATA_BYTE length: 0x0 offset: 0x153
			public byte MalahMin; // type: DATA_BYTE length: 0x0 offset: 0x154
			public byte LarzukMin; // type: DATA_BYTE length: 0x0 offset: 0x155
			public byte DrehyaMin; // type: DATA_BYTE length: 0x0 offset: 0x156
			public byte AkaraMax; // type: DATA_BYTE length: 0x0 offset: 0x157
			public byte GheedMax; // type: DATA_BYTE length: 0x0 offset: 0x158
			public byte CharsiMax; // type: DATA_BYTE length: 0x0 offset: 0x159
			public byte FaraMax; // type: DATA_BYTE length: 0x0 offset: 0x15a
			public byte LysanderMax; // type: DATA_BYTE length: 0x0 offset: 0x15b
			public byte DrognanMax; // type: DATA_BYTE length: 0x0 offset: 0x15c
			public byte HraltiMax; // type: DATA_BYTE length: 0x0 offset: 0x15d
			public byte AlkorMax; // type: DATA_BYTE length: 0x0 offset: 0x15e
			public byte OrmusMax; // type: DATA_BYTE length: 0x0 offset: 0x15f
			public byte ElzixMax; // type: DATA_BYTE length: 0x0 offset: 0x160
			public byte AshearaMax; // type: DATA_BYTE length: 0x0 offset: 0x161
			public byte CainMax; // type: DATA_BYTE length: 0x0 offset: 0x162
			public byte HalbuMax; // type: DATA_BYTE length: 0x0 offset: 0x163
			public byte JamellaMax; // type: DATA_BYTE length: 0x0 offset: 0x164
			public byte MalahMax; // type: DATA_BYTE length: 0x0 offset: 0x165
			public byte LarzukMax; // type: DATA_BYTE length: 0x0 offset: 0x166
			public byte DrehyaMax; // type: DATA_BYTE length: 0x0 offset: 0x167
			public byte AkaraMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x168
			public byte GheedMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x169
			public byte CharsiMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x16a
			public byte FaraMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x16b
			public byte LysanderMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x16c
			public byte DrognanMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x16d
			public byte HraltiMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x16e
			public byte AlkorMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x16f
			public byte OrmusMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x170
			public byte ElzixMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x171
			public byte AshearaMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x172
			public byte CainMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x173
			public byte HalbuMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x174
			public byte JamellaMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x175
			public byte MalahMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x176
			public byte LarzukMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x177
			public byte DrehyaMagicMin; // type: DATA_BYTE length: 0x0 offset: 0x178
			public byte AkaraMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x179
			public byte GheedMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x17a
			public byte CharsiMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x17b
			public byte FaraMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x17c
			public byte LysanderMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x17d
			public byte DrognanMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x17e
			public byte HraltiMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x17f
			public byte AlkorMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x180
			public byte OrmusMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x181
			public byte ElzixMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x182
			public byte AshearaMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x183
			public byte CainMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x184
			public byte HalbuMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x185
			public byte JamellaMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x186
			public byte MalahMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x187
			public byte LarzukMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x188
			public byte DrehyaMagicMax; // type: DATA_BYTE length: 0x0 offset: 0x189
			public byte AkaraMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x18a
			public byte GheedMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x18b
			public byte CharsiMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x18c
			public byte FaraMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x18d
			public byte LysanderMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x18e
			public byte DrognanMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x18f
			public byte HraltiMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x190
			public byte AlkorMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x191
			public byte OrmusMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x192
			public byte ElzixMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x193
			public byte AshearaMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x194
			public byte CainMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x195
			public byte HalbuMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x196
			public byte JamellaMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x197
			public byte MalahMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x198
			public byte LarzukMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x199
			public byte DrehyaMagicLvl; // type: DATA_BYTE length: 0x0 offset: 0x19a
			public uint NightmareUpgrade; // type: DATA_RAW length: 0x0 offset: 0x19c
			public uint HellUpgrade; // type: DATA_RAW length: 0x0 offset: 0x1a0
			public byte PermStoreItem; // type: DATA_BYTE length: 0x0 offset: 0x1a4
			public byte multibuy; // type: DATA_BYTE length: 0x0 offset: 0x1a5
			public uint unk1;
			public ushort unk2;
		}
	}

	public class CubemainTable
	{
		public List<CubemainEntry> entries = new List<CubemainEntry>();
		public IEnumerable<CubemainEntry> Entries { get { return entries; } }

		public CubemainTable(byte[] bytes)
		{
			uint count = BitConverter.ToUInt32(bytes, 0);
			bytes = bytes.Skip(4).ToArray();
			int size = Marshal.SizeOf(typeof(CubemainEntry));
			for(uint i = 0; i < count; i++)
			{
				var bits = bytes.Take(size).ToArray();
				entries.Add(bits.PinAndCast<CubemainEntry>());
				bytes = bytes.Skip(size).ToArray();
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct CubemainEntry
		{
			[FieldOffset(0x0)] byte enabled; // type: DATA_BYTE length: 0x0 offset: 0x0
			[FieldOffset(0x1)] byte ladder; // type: DATA_BYTE length: 0x0 offset: 0x1
			[FieldOffset(0x2)] byte minDiff; // type: DATA_BYTE length: 0x0 offset: 0x2
			[FieldOffset(0x3)] byte Class; // type: CODE_TO_BYTE length: 0x0 offset: 0x3
			[FieldOffset(0x4)] byte op; // type: DATA_BYTE length: 0x0 offset: 0x4
			[FieldOffset(0x8)] uint param; // type: DATA_DWORD length: 0x0 offset: 0x8
			[FieldOffset(0xc)] uint value; // type: DATA_DWORD length: 0x0 offset: 0xc
			[FieldOffset(0x10)] byte numinputs; // type: DATA_BYTE length: 0x0 offset: 0x10
			[FieldOffset(0x12)] short version; // type: DATA_WORD length: 0x0 offset: 0x12
			[FieldOffset(0x14)] CubeItemFlags item1;
			[FieldOffset(0x15)] byte input1ItemType; // type: DATA_BYTE length: 0x0 offset: 0x15
			[FieldOffset(0x16)] byte input1Item; // type: DATA_BYTE length: 0x0 offset: 0x16
			[FieldOffset(0x18)] byte input1ItemId; // type: DATA_BYTE length: 0x0 offset: 0x18
			[FieldOffset(0x1a)] byte input1Quality; // type: DATA_BYTE length: 0x0 offset: 0x1a
			[FieldOffset(0x1b)] byte input1Quantity; // type: DATA_BYTE length: 0x0 offset: 0x1b
			[FieldOffset(0x1c)] CubeItemFlags item2;
			[FieldOffset(0x1d)] byte input2itemtype; // type: DATA_BYTE length: 0x0 offset: 0x1d
			[FieldOffset(0x1e)] byte input2item; // type: DATA_BYTE length: 0x0 offset: 0x1e
			[FieldOffset(0x20)] byte input2itemid; // type: DATA_BYTE length: 0x0 offset: 0x20
			[FieldOffset(0x22)] byte input2quality; // type: DATA_BYTE length: 0x0 offset: 0x22
			[FieldOffset(0x23)] byte input2quantity; // type: DATA_BYTE length: 0x0 offset: 0x23
			[FieldOffset(0x24)] CubeItemFlags item3;
			[FieldOffset(0x25)] byte input3itemtype; // type: DATA_BYTE length: 0x0 offset: 0x25
			[FieldOffset(0x26)] byte input3item; // type: DATA_BYTE length: 0x0 offset: 0x26
			[FieldOffset(0x27)] byte input3itemid; // type: DATA_BYTE length: 0x0 offset: 0x28
			[FieldOffset(0x2a)] byte input3quality; // type: DATA_BYTE length: 0x0 offset: 0x2a
			[FieldOffset(0x2b)] byte input3quantity; // type: DATA_BYTE length: 0x0 offset: 0x2b
			[FieldOffset(0x2c)] CubeItemFlags item4;
			[FieldOffset(0x2d)] byte input4itemtype; // type: DATA_BYTE length: 0x0 offset: 0x2d
			[FieldOffset(0x2e)] byte input4item; // type: DATA_BYTE length: 0x0 offset: 0x2e
			[FieldOffset(0x30)] byte input4itemid; // type: DATA_BYTE length: 0x0 offset: 0x30
			[FieldOffset(0x32)] byte input4quality; // type: DATA_BYTE length: 0x0 offset: 0x32
			[FieldOffset(0x33)] byte input4quantity; // type: DATA_BYTE length: 0x0 offset: 0x33
			[FieldOffset(0x34)] CubeItemFlags item5;
			[FieldOffset(0x35)] byte input5itemtype; // type: DATA_BYTE length: 0x0 offset: 0x35
			[FieldOffset(0x36)] byte input5item; // type: DATA_BYTE length: 0x0 offset: 0x36
			[FieldOffset(0x38)] byte input5itemid; // type: DATA_BYTE length: 0x0 offset: 0x38
			[FieldOffset(0x3a)] byte input5quality; // type: DATA_BYTE length: 0x0 offset: 0x3a
			[FieldOffset(0x3b)] byte input5quantity; // type: DATA_BYTE length: 0x0 offset: 0x3b
			[FieldOffset(0x3c)] CubeItemFlags item6;
			[FieldOffset(0x3d)] byte input6itemtype; // type: DATA_BYTE length: 0x0 offset: 0x3d
			[FieldOffset(0x3e)] byte input6item; // type: DATA_BYTE length: 0x0 offset: 0x3e
			[FieldOffset(0x40)] byte input6itemid; // type: DATA_BYTE length: 0x0 offset: 0x40
			[FieldOffset(0x42)] byte input6quality; // type: DATA_BYTE length: 0x0 offset: 0x42
			[FieldOffset(0x43)] byte input6quantity; // type: DATA_BYTE length: 0x0 offset: 0x43
			[FieldOffset(0x44)] CubeItemFlags item7;
			[FieldOffset(0x45)] byte input7itemtype; // type: DATA_BYTE length: 0x0 offset: 0x45
			[FieldOffset(0x46)] byte input7item; // type: DATA_BYTE length: 0x0 offset: 0x46
			[FieldOffset(0x48)] byte input7itemid; // type: DATA_BYTE length: 0x0 offset: 0x48
			[FieldOffset(0x4a)] byte input7quality; // type: DATA_BYTE length: 0x0 offset: 0x4a
			[FieldOffset(0x4b)] byte input7quantity; // type: DATA_BYTE length: 0x0 offset: 0x4b
			[FieldOffset(0x4c)] byte outputitemflags; // type: DATA_BYTE length: 0x0 offset: 0x4c
			[FieldOffset(0x4d)] byte outputitemtype; // type: DATA_BYTE length: 0x0 offset: 0x4d
			[FieldOffset(0x4e)] byte outputitem; // type: DATA_BYTE length: 0x0 offset: 0x4e
			[FieldOffset(0x50)] byte outputitemid; // type: DATA_BYTE length: 0x0 offset: 0x50
			[FieldOffset(0x52)] byte outputitemquality; // type: DATA_BYTE length: 0x0 offset: 0x52
			[FieldOffset(0x53)] byte outputitemquantity; // type: DATA_BYTE length: 0x0 offset: 0x53
			[FieldOffset(0x54)] byte outputtype; // type: DATA_BYTE length: 0x0 offset: 0x54
			[FieldOffset(0x55)] byte lvl; // type: DATA_BYTE length: 0x0 offset: 0x55
			[FieldOffset(0x56)] byte plvl; // type: DATA_BYTE length: 0x0 offset: 0x56
			[FieldOffset(0x57)] byte ilvl; // type: DATA_BYTE length: 0x0 offset: 0x57	
			[FieldOffset(0x58)] byte outputitemprefixid; // type: DATA_BYTE length: 0x0 offset: 0x58
			[FieldOffset(0x5a)] byte outputitemunknown; // type: DATA_BYTE length: 0x0 offset: 0x5a
			[FieldOffset(0x5e)] byte outputitemsuffixid; // type: DATA_BYTE length: 0x0 offset: 0x5e
			[FieldOffset(0x60)] byte unknownfield; // type: DATA_BYTE length: 0x0 offset: 0x60
			[FieldOffset(0x64)] uint mod1; // type: NAME_TO_DWORD length: 0x0 offset: 0x64
			[FieldOffset(0x68)] short mod1param; // type: DATA_WORD length: 0x0 offset: 0x68
			[FieldOffset(0x6a)] short mod1min; // type: DATA_WORD length: 0x0 offset: 0x6a
			[FieldOffset(0x6c)] short mod1max; // type: DATA_WORD length: 0x0 offset: 0x6c
			[FieldOffset(0x6e)] byte mod1chance; // type: DATA_BYTE length: 0x0 offset: 0x6e
			[FieldOffset(0x70)] uint mod2; // type: NAME_TO_DWORD length: 0x0 offset: 0x70
			[FieldOffset(0x74)] short mod2param; // type: DATA_WORD length: 0x0 offset: 0x74
			[FieldOffset(0x76)] short mod2min; // type: DATA_WORD length: 0x0 offset: 0x76
			[FieldOffset(0x78)] short mod2max; // type: DATA_WORD length: 0x0 offset: 0x78
			[FieldOffset(0x7a)] byte mod2chance; // type: DATA_BYTE length: 0x0 offset: 0x7a
			[FieldOffset(0x7c)] uint mod3; // type: NAME_TO_DWORD length: 0x0 offset: 0x7c
			[FieldOffset(0x80)] short mod3param; // type: DATA_WORD length: 0x0 offset: 0x80
			[FieldOffset(0x82)] short mod3min; // type: DATA_WORD length: 0x0 offset: 0x82
			[FieldOffset(0x84)] short mod3max; // type: DATA_WORD length: 0x0 offset: 0x84
			[FieldOffset(0x86)] byte mod3chance; // type: DATA_BYTE length: 0x0 offset: 0x86
			[FieldOffset(0x88)] uint mod4; // type: NAME_TO_DWORD length: 0x0 offset: 0x88
			[FieldOffset(0x8c)] short mod4param; // type: DATA_WORD length: 0x0 offset: 0x8c
			[FieldOffset(0x8e)] short mod4min; // type: DATA_WORD length: 0x0 offset: 0x8e
			[FieldOffset(0x90)] short mod4max; // type: DATA_WORD length: 0x0 offset: 0x90
			[FieldOffset(0x92)] byte mod4chance; // type: DATA_BYTE length: 0x0 offset: 0x92
			[FieldOffset(0x94)] uint mod5; // type: NAME_TO_DWORD length: 0x0 offset: 0x94
			[FieldOffset(0x98)] short mod5param; // type: DATA_WORD length: 0x0 offset: 0x98
			[FieldOffset(0x9a)] short mod5min; // type: DATA_WORD length: 0x0 offset: 0x9a
			[FieldOffset(0x9c)] short mod5max; // type: DATA_WORD length: 0x0 offset: 0x9c
			[FieldOffset(0x9e)] byte mod5chance; // type: DATA_BYTE length: 0x0 offset: 0x9e
			[FieldOffset(0xa9)] byte blvl; // type: DATA_BYTE length: 0x0 offset: 0xa9
			[FieldOffset(0xaa)] byte bplvl; // type: DATA_BYTE length: 0x0 offset: 0xaa
			[FieldOffset(0xab)] byte bilvl; // type: DATA_BYTE length: 0x0 offset: 0xab
			[FieldOffset(0xb8)] uint bmod1; // type: NAME_TO_DWORD length: 0x0 offset: 0xb8
			[FieldOffset(0xbc)] short bmod1param; // type: DATA_WORD length: 0x0 offset: 0xbc
			[FieldOffset(0xbe)] short bmod1min; // type: DATA_WORD length: 0x0 offset: 0xbe
			[FieldOffset(0xc0)] short bmod1max; // type: DATA_WORD length: 0x0 offset: 0xc0
			[FieldOffset(0xc2)] byte bmod1chance; // type: DATA_BYTE length: 0x0 offset: 0xc2
			[FieldOffset(0xc4)] uint bmod2; // type: NAME_TO_DWORD length: 0x0 offset: 0xc4
			[FieldOffset(0xc8)] short bmod2param; // type: DATA_WORD length: 0x0 offset: 0xc8
			[FieldOffset(0xca)] short bmod2min; // type: DATA_WORD length: 0x0 offset: 0xca
			[FieldOffset(0xcc)] short bmod2max; // type: DATA_WORD length: 0x0 offset: 0xcc
			[FieldOffset(0xce)] byte bmod2chance; // type: DATA_BYTE length: 0x0 offset: 0xce
			[FieldOffset(0xd0)] uint bmod3; // type: NAME_TO_DWORD length: 0x0 offset: 0xd0
			[FieldOffset(0xd4)] short bmod3param; // type: DATA_WORD length: 0x0 offset: 0xd4
			[FieldOffset(0xd6)] short bmod3min; // type: DATA_WORD length: 0x0 offset: 0xd6
			[FieldOffset(0xd8)] short bmod3max; // type: DATA_WORD length: 0x0 offset: 0xd8
			[FieldOffset(0xda)] byte bmod3chance; // type: DATA_BYTE length: 0x0 offset: 0xda
			[FieldOffset(0xdc)] uint bmod4; // type: NAME_TO_DWORD length: 0x0 offset: 0xdc
			[FieldOffset(0xe0)] short bmod4param; // type: DATA_WORD length: 0x0 offset: 0xe0
			[FieldOffset(0xe2)] short bmod4min; // type: DATA_WORD length: 0x0 offset: 0xe2
			[FieldOffset(0xe4)] short bmod4max; // type: DATA_WORD length: 0x0 offset: 0xe4
			[FieldOffset(0xe6)] byte bmod4chance; // type: DATA_BYTE length: 0x0 offset: 0xe6
			[FieldOffset(0xe8)] uint bmod5; // type: NAME_TO_DWORD length: 0x0 offset: 0xe8
			[FieldOffset(0xec)] short bmod5param; // type: DATA_WORD length: 0x0 offset: 0xec
			[FieldOffset(0xee)] short bmod5min; // type: DATA_WORD length: 0x0 offset: 0xee
			[FieldOffset(0xf0)] short bmod5max; // type: DATA_WORD length: 0x0 offset: 0xf0
			[FieldOffset(0xf2)] byte bmod5chance; // type: DATA_BYTE length: 0x0 offset: 0xf2
			[FieldOffset(0xfd)] byte clvl; // type: DATA_BYTE length: 0x0 offset: 0xfd
			[FieldOffset(0xfe)] byte cplvl; // type: DATA_BYTE length: 0x0 offset: 0xfe
			[FieldOffset(0xff)] byte cilvl; // type: DATA_BYTE length: 0x0 offset: 0xff
			[FieldOffset(0x10c)] uint cmod1; // type: NAME_TO_DWORD length: 0x0 offset: 0x10c
			[FieldOffset(0x110)] short cmod1param; // type: DATA_WORD length: 0x0 offset: 0x110
			[FieldOffset(0x112)] short cmod1min; // type: DATA_WORD length: 0x0 offset: 0x112
			[FieldOffset(0x114)] short cmod1max; // type: DATA_WORD length: 0x0 offset: 0x114
			[FieldOffset(0x116)] byte cmod1chance; // type: DATA_BYTE length: 0x0 offset: 0x116
			[FieldOffset(0x118)] uint cmod2; // type: NAME_TO_DWORD length: 0x0 offset: 0x118
			[FieldOffset(0x11c)] short cmod2param; // type: DATA_WORD length: 0x0 offset: 0x11c
			[FieldOffset(0x11e)] short cmod2min; // type: DATA_WORD length: 0x0 offset: 0x11e
			[FieldOffset(0x120)] short cmod2max; // type: DATA_WORD length: 0x0 offset: 0x120
			[FieldOffset(0x122)] byte cmod2chance; // type: DATA_BYTE length: 0x0 offset: 0x122
			[FieldOffset(0x124)] uint cmod3; // type: NAME_TO_DWORD length: 0x0 offset: 0x124
			[FieldOffset(0x148)] short cmod3param; // type: DATA_WORD length: 0x0 offset: 0x128
			[FieldOffset(0x12a)] short cmod3min; // type: DATA_WORD length: 0x0 offset: 0x12a
			[FieldOffset(0x12c)] short cmod3max; // type: DATA_WORD length: 0x0 offset: 0x12c
			[FieldOffset(0x12e)] byte cmod3chance; // type: DATA_BYTE length: 0x0 offset: 0x12e
			[FieldOffset(0x130)] uint cmod4; // type: NAME_TO_DWORD length: 0x0 offset: 0x130
			[FieldOffset(0x134)] short cmod4param; // type: DATA_WORD length: 0x0 offset: 0x134
			[FieldOffset(0x136)] short cmod4min; // type: DATA_WORD length: 0x0 offset: 0x136
			[FieldOffset(0x138)] short cmod4max; // type: DATA_WORD length: 0x0 offset: 0x138
			[FieldOffset(0x13a)] byte cmod4chance; // type: DATA_BYTE length: 0x0 offset: 0x13a
			[FieldOffset(0x13c)] uint cmod5; // type: NAME_TO_DWORD length: 0x0 offset: 0x13c
			[FieldOffset(0x140)] short cmod5param; // type: DATA_WORD length: 0x0 offset: 0x140
			[FieldOffset(0x142)] short cmod5min; // type: DATA_WORD length: 0x0 offset: 0x142
			[FieldOffset(0x144)] short cmod5max; // type: DATA_WORD length: 0x0 offset: 0x144
			[FieldOffset(0x146)] byte cmod5chance; // type: DATA_BYTE length: 0x0 offset: 0x146
			[FieldOffset(0x148)] byte end; // type: END length: 0x0 offset: 0x148
		}
	}

	public class MonstatsTable
	{
		private List<MonstatsEntry> entries = new List<MonstatsEntry>();
		public IEnumerable<MonstatsEntry> Entries { get { return entries; } }

		public MonstatsTable(byte[] bytes)
		{
			uint count = BitConverter.ToUInt32(bytes, 0);
			int size = Marshal.SizeOf(typeof(MonstatsEntry));
			bytes = bytes.Skip(4).ToArray();
			for(uint i = 0; i < count; i++)
			{
				var bits = bytes.Take(size).ToArray();
				entries.Add(bits.PinAndCast<MonstatsEntry>());
				bytes = bytes.Skip(size).ToArray();
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct MonstatsEntry
		{
			[FieldOffset(0x0)] short Id; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
			[FieldOffset(0x2)] short BaseId; // type: NAME_TO_WORD length: 0x0 offset: 0x2
			[FieldOffset(0x4)] short NextInClass; // type: NAME_TO_WORD length: 0x0 offset: 0x4
			[FieldOffset(0x6)] short NameStr; // type: KEY_TO_WORD length: 0x0 offset: 0x6
			[FieldOffset(0x8)] short DescStr; // type: KEY_TO_WORD length: 0x0 offset: 0x8
			[FieldOffset(0xc)] MonstatsFlags flags;
			[FieldOffset(0x10)] uint Code; // type: DATA_RAW length: 0x0 offset: 0x10
			[FieldOffset(0x14)] short MonSound; // type: NAME_TO_WORD length: 0x0 offset: 0x14
			[FieldOffset(0x16)] short UMonSound; // type: NAME_TO_WORD length: 0x0 offset: 0x16
			[FieldOffset(0x18)] short MonStatsEx; // type: NAME_TO_WORD length: 0x0 offset: 0x18
			[FieldOffset(0x1a)] short MonProp; // type: NAME_TO_WORD length: 0x0 offset: 0x1a
			[FieldOffset(0x1c)] short MonType; // type: NAME_TO_WORD length: 0x0 offset: 0x1c
			[FieldOffset(0x1e)] short AI; // type: NAME_TO_WORD length: 0x0 offset: 0x1e
			[FieldOffset(0x20)] short spawn; // type: NAME_TO_WORD length: 0x0 offset: 0x20
			[FieldOffset(0x22)] byte spawnx; // type: DATA_BYTE length: 0x0 offset: 0x22
			[FieldOffset(0x23)] byte spawny; // type: DATA_BYTE length: 0x0 offset: 0x23
			[FieldOffset(0x24)] byte spawnmode; // type: CODE_TO_BYTE length: 0x0 offset: 0x24
			[FieldOffset(0x26)] short minion1; // type: NAME_TO_WORD length: 0x0 offset: 0x26
			[FieldOffset(0x28)] short minion2; // type: NAME_TO_WORD length: 0x0 offset: 0x28
			[FieldOffset(0x2c)] byte PartyMin; // type: DATA_BYTE length: 0x0 offset: 0x2c
			[FieldOffset(0x2d)] byte PartyMax; // type: DATA_BYTE length: 0x0 offset: 0x2d
			[FieldOffset(0x2e)] byte Rarity; // type: DATA_BYTE length: 0x0 offset: 0x2e
			[FieldOffset(0x2f)] byte MinGrp; // type: DATA_BYTE length: 0x0 offset: 0x2f
			[FieldOffset(0x30)] byte MaxGrp; // type: DATA_BYTE length: 0x0 offset: 0x30
			[FieldOffset(0x31)] byte sparsePopulate; // type: DATA_BYTE length: 0x0 offset: 0x31
			[FieldOffset(0x32)] short Velocity; // type: DATA_WORD length: 0x0 offset: 0x32
			[FieldOffset(0x34)] short Run; // type: DATA_WORD length: 0x0 offset: 0x34
			[FieldOffset(0x3a)] short MissA1; // type: NAME_TO_WORD length: 0x0 offset: 0x3a
			[FieldOffset(0x3c)] short MissA2; // type: NAME_TO_WORD length: 0x0 offset: 0x3c
			[FieldOffset(0x3e)] short MissS1; // type: NAME_TO_WORD length: 0x0 offset: 0x3e
			[FieldOffset(0x40)] short MissS2; // type: NAME_TO_WORD length: 0x0 offset: 0x40
			[FieldOffset(0x42)] short MissS3; // type: NAME_TO_WORD length: 0x0 offset: 0x42
			[FieldOffset(0x44)] short MissS4; // type: NAME_TO_WORD length: 0x0 offset: 0x44
			[FieldOffset(0x46)] short MissC; // type: NAME_TO_WORD length: 0x0 offset: 0x46
			[FieldOffset(0x48)] short MissSQ; // type: NAME_TO_WORD length: 0x0 offset: 0x48
			[FieldOffset(0x4c)] byte Align; // type: DATA_BYTE length: 0x0 offset: 0x4c
			[FieldOffset(0x4d)] byte TransLvl; // type: DATA_BYTE length: 0x0 offset: 0x4d
			[FieldOffset(0x4e)] byte threat; // type: DATA_BYTE length: 0x0 offset: 0x4e
			[FieldOffset(0x4f)] byte aidel; // type: DATA_BYTE length: 0x0 offset: 0x4f
			[FieldOffset(0x50)] byte aidelN; // type: DATA_BYTE length: 0x0 offset: 0x50
			[FieldOffset(0x51)] byte aidelH; // type: DATA_BYTE length: 0x0 offset: 0x51
			[FieldOffset(0x52)] byte aidist; // type: DATA_BYTE length: 0x0 offset: 0x52
			[FieldOffset(0x53)] byte aidistN; // type: DATA_BYTE length: 0x0 offset: 0x53
			[FieldOffset(0x54)] byte aidistH; // type: DATA_BYTE length: 0x0 offset: 0x54
			[FieldOffset(0x56)] short aip1; // type: DATA_WORD length: 0x0 offset: 0x56
			[FieldOffset(0x58)] short aip1N; // type: DATA_WORD length: 0x0 offset: 0x58
			[FieldOffset(0x5a)] short aip1H; // type: DATA_WORD length: 0x0 offset: 0x5a
			[FieldOffset(0x5c)] short aip2; // type: DATA_WORD length: 0x0 offset: 0x5c
			[FieldOffset(0x5e)] short aip2N; // type: DATA_WORD length: 0x0 offset: 0x5e
			[FieldOffset(0x60)] short aip2H; // type: DATA_WORD length: 0x0 offset: 0x60
			[FieldOffset(0x62)] short aip3; // type: DATA_WORD length: 0x0 offset: 0x62
			[FieldOffset(0x64)] short aip3N; // type: DATA_WORD length: 0x0 offset: 0x64
			[FieldOffset(0x66)] short aip3H; // type: DATA_WORD length: 0x0 offset: 0x66
			[FieldOffset(0x68)] short aip4; // type: DATA_WORD length: 0x0 offset: 0x68
			[FieldOffset(0x6a)] short aip4N; // type: DATA_WORD length: 0x0 offset: 0x6a
			[FieldOffset(0x6c)] short aip4H; // type: DATA_WORD length: 0x0 offset: 0x6c
			[FieldOffset(0x6e)] short aip5; // type: DATA_WORD length: 0x0 offset: 0x6e
			[FieldOffset(0x70)] short aip5N; // type: DATA_WORD length: 0x0 offset: 0x70
			[FieldOffset(0x72)] short aip5H; // type: DATA_WORD length: 0x0 offset: 0x72
			[FieldOffset(0x74)] short aip6; // type: DATA_WORD length: 0x0 offset: 0x74
			[FieldOffset(0x76)] short aip6N; // type: DATA_WORD length: 0x0 offset: 0x76
			[FieldOffset(0x78)] short aip6H; // type: DATA_WORD length: 0x0 offset: 0x78
			[FieldOffset(0x7a)] short aip7; // type: DATA_WORD length: 0x0 offset: 0x7a
			[FieldOffset(0x7c)] short aip7N; // type: DATA_WORD length: 0x0 offset: 0x7c
			[FieldOffset(0x7e)] short aip7H; // type: DATA_WORD length: 0x0 offset: 0x7e
			[FieldOffset(0x80)] short aip8; // type: DATA_WORD length: 0x0 offset: 0x80
			[FieldOffset(0x82)] short aip8N; // type: DATA_WORD length: 0x0 offset: 0x82
			[FieldOffset(0x84)] short aip8H; // type: DATA_WORD length: 0x0 offset: 0x84
			[FieldOffset(0x86)] short TreasureClass1; // type: NAME_TO_WORD length: 0x0 offset: 0x86
			[FieldOffset(0x88)] short TreasureClass2; // type: NAME_TO_WORD length: 0x0 offset: 0x88
			[FieldOffset(0x8a)] short TreasureClass3; // type: NAME_TO_WORD length: 0x0 offset: 0x8a
			[FieldOffset(0x8c)] short TreasureClass4; // type: NAME_TO_WORD length: 0x0 offset: 0x8c
			[FieldOffset(0x8e)] short TreasureClass1N; // type: NAME_TO_WORD length: 0x0 offset: 0x8e
			[FieldOffset(0x90)] short TreasureClass2N; // type: NAME_TO_WORD length: 0x0 offset: 0x90
			[FieldOffset(0x92)] short TreasureClass3N; // type: NAME_TO_WORD length: 0x0 offset: 0x92
			[FieldOffset(0x94)] short TreasureClass4N; // type: NAME_TO_WORD length: 0x0 offset: 0x94
			[FieldOffset(0x96)] short TreasureClass1H; // type: NAME_TO_WORD length: 0x0 offset: 0x96
			[FieldOffset(0x98)] short TreasureClass2H; // type: NAME_TO_WORD length: 0x0 offset: 0x98
			[FieldOffset(0x9a)] short TreasureClass3H; // type: NAME_TO_WORD length: 0x0 offset: 0x9a
			[FieldOffset(0x9c)] short TreasureClass4H; // type: NAME_TO_WORD length: 0x0 offset: 0x9c
			[FieldOffset(0x9e)] byte TCQuestId; // type: DATA_BYTE length: 0x0 offset: 0x9e
			[FieldOffset(0x9f)] byte TCQuestCP; // type: DATA_BYTE length: 0x0 offset: 0x9f
			[FieldOffset(0xa0)] byte Drain; // type: DATA_BYTE length: 0x0 offset: 0xa0
			[FieldOffset(0xa1)] byte DrainN; // type: DATA_BYTE length: 0x0 offset: 0xa1
			[FieldOffset(0xa2)] byte DrainH; // type: DATA_BYTE length: 0x0 offset: 0xa2
			[FieldOffset(0xa3)] byte ToBlock; // type: DATA_BYTE length: 0x0 offset: 0xa3
			[FieldOffset(0xa4)] byte ToBlockN; // type: DATA_BYTE length: 0x0 offset: 0xa4
			[FieldOffset(0xa5)] byte ToBlockH; // type: DATA_BYTE length: 0x0 offset: 0xa5
			[FieldOffset(0xa6)] byte Crit; // type: DATA_BYTE length: 0x0 offset: 0xa6
			[FieldOffset(0xa8)] short SkillDamage; // type: NAME_TO_WORD length: 0x0 offset: 0xa8
			[FieldOffset(0xaa)] short Level; // type: DATA_WORD length: 0x0 offset: 0xaa
			[FieldOffset(0xac)] short LevelN; // type: DATA_WORD length: 0x0 offset: 0xac
			[FieldOffset(0xae)] short LevelH; // type: DATA_WORD length: 0x0 offset: 0xae
			[FieldOffset(0xb0)] short MinHP; // type: DATA_WORD length: 0x0 offset: 0xb0
			[FieldOffset(0xb2)] short MinHPN; // type: DATA_WORD length: 0x0 offset: 0xb2
			[FieldOffset(0xb4)] short MinHPH; // type: DATA_WORD length: 0x0 offset: 0xb4
			[FieldOffset(0xb6)] short MaxHP; // type: DATA_WORD length: 0x0 offset: 0xb6
			[FieldOffset(0xb8)] short MaxHPN; // type: DATA_WORD length: 0x0 offset: 0xb8
			[FieldOffset(0xba)] short MaxHPH; // type: DATA_WORD length: 0x0 offset: 0xba
			[FieldOffset(0xbc)] short AC; // type: DATA_WORD length: 0x0 offset: 0xbc
			[FieldOffset(0xbe)] short ACN; // type: DATA_WORD length: 0x0 offset: 0xbe
			[FieldOffset(0xc0)] short ACH; // type: DATA_WORD length: 0x0 offset: 0xc0
			[FieldOffset(0xc2)] short A1TH; // type: DATA_WORD length: 0x0 offset: 0xc2
			[FieldOffset(0xc4)] short A1THN; // type: DATA_WORD length: 0x0 offset: 0xc4
			[FieldOffset(0xc6)] short A1THH; // type: DATA_WORD length: 0x0 offset: 0xc6
			[FieldOffset(0xc8)] short A2TH; // type: DATA_WORD length: 0x0 offset: 0xc8
			[FieldOffset(0xca)] short A2THN; // type: DATA_WORD length: 0x0 offset: 0xca
			[FieldOffset(0xcc)] short A2THH; // type: DATA_WORD length: 0x0 offset: 0xcc
			[FieldOffset(0xce)] short S1TH; // type: DATA_WORD length: 0x0 offset: 0xce
			[FieldOffset(0xd0)] short S1THN; // type: DATA_WORD length: 0x0 offset: 0xd0
			[FieldOffset(0xd2)] short S1THH; // type: DATA_WORD length: 0x0 offset: 0xd2
			[FieldOffset(0xd4)] short Exp; // type: DATA_WORD length: 0x0 offset: 0xd4
			[FieldOffset(0xd6)] short ExpN; // type: DATA_WORD length: 0x0 offset: 0xd6
			[FieldOffset(0xd8)] short ExpH; // type: DATA_WORD length: 0x0 offset: 0xd8
			[FieldOffset(0xda)] short A1MinD; // type: DATA_WORD length: 0x0 offset: 0xda
			[FieldOffset(0xdc)] short A1MinDN; // type: DATA_WORD length: 0x0 offset: 0xdc
			[FieldOffset(0xde)] short A1MinDH; // type: DATA_WORD length: 0x0 offset: 0xde
			[FieldOffset(0xe0)] short A1MaxD; // type: DATA_WORD length: 0x0 offset: 0xe0
			[FieldOffset(0xe2)] short A1MaxDN; // type: DATA_WORD length: 0x0 offset: 0xe2
			[FieldOffset(0xe4)] short A1MaxDH; // type: DATA_WORD length: 0x0 offset: 0xe4
			[FieldOffset(0xe6)] short A2MinD; // type: DATA_WORD length: 0x0 offset: 0xe6
			[FieldOffset(0xe8)] short A2MinDN; // type: DATA_WORD length: 0x0 offset: 0xe8
			[FieldOffset(0xea)] short A2MinDH; // type: DATA_WORD length: 0x0 offset: 0xea
			[FieldOffset(0xec)] short A2MaxD; // type: DATA_WORD length: 0x0 offset: 0xec
			[FieldOffset(0xee)] short A2MaxDN; // type: DATA_WORD length: 0x0 offset: 0xee
			[FieldOffset(0xf0)] short A2MaxDH; // type: DATA_WORD length: 0x0 offset: 0xf0
			[FieldOffset(0xf2)] short S1MinD; // type: DATA_WORD length: 0x0 offset: 0xf2
			[FieldOffset(0xf4)] short S1MinDN; // type: DATA_WORD length: 0x0 offset: 0xf4
			[FieldOffset(0xf6)] short S1MinDH; // type: DATA_WORD length: 0x0 offset: 0xf6
			[FieldOffset(0xf8)] short S1MaxD; // type: DATA_WORD length: 0x0 offset: 0xf8
			[FieldOffset(0xfa)] short S1MaxDN; // type: DATA_WORD length: 0x0 offset: 0xfa
			[FieldOffset(0xfc)] short S1MaxDH; // type: DATA_WORD length: 0x0 offset: 0xfc
			[FieldOffset(0xfe)] byte El1Mode; // type: CODE_TO_BYTE length: 0x0 offset: 0xfe
			[FieldOffset(0xff)] byte El2Mode; // type: CODE_TO_BYTE length: 0x0 offset: 0xff
			[FieldOffset(0x100)] byte El3Mode; // type: CODE_TO_BYTE length: 0x0 offset: 0x100
			[FieldOffset(0x101)] byte El1Type; // type: CODE_TO_BYTE length: 0x0 offset: 0x101
			[FieldOffset(0x102)] byte El2Type; // type: CODE_TO_BYTE length: 0x0 offset: 0x102
			[FieldOffset(0x103)] byte El3Type; // type: CODE_TO_BYTE length: 0x0 offset: 0x103
			[FieldOffset(0x104)] byte El1Pct; // type: DATA_BYTE length: 0x0 offset: 0x104
			[FieldOffset(0x105)] byte El1PctN; // type: DATA_BYTE length: 0x0 offset: 0x105
			[FieldOffset(0x106)] byte El1PctH; // type: DATA_BYTE length: 0x0 offset: 0x106
			[FieldOffset(0x107)] byte El2Pct; // type: DATA_BYTE length: 0x0 offset: 0x107
			[FieldOffset(0x108)] byte El2PctN; // type: DATA_BYTE length: 0x0 offset: 0x108
			[FieldOffset(0x109)] byte El2PctH; // type: DATA_BYTE length: 0x0 offset: 0x109
			[FieldOffset(0x10a)] byte El3Pct; // type: DATA_BYTE length: 0x0 offset: 0x10a
			[FieldOffset(0x10b)] byte El3PctN; // type: DATA_BYTE length: 0x0 offset: 0x10b
			[FieldOffset(0x10c)] byte El3PctH; // type: DATA_BYTE length: 0x0 offset: 0x10c
			[FieldOffset(0x10e)] short El1MinD; // type: DATA_WORD length: 0x0 offset: 0x10e
			[FieldOffset(0x110)] short El1MinDN; // type: DATA_WORD length: 0x0 offset: 0x110
			[FieldOffset(0x112)] short El1MinDH; // type: DATA_WORD length: 0x0 offset: 0x112
			[FieldOffset(0x114)] short El2MinD; // type: DATA_WORD length: 0x0 offset: 0x114
			[FieldOffset(0x116)] short El2MinDN; // type: DATA_WORD length: 0x0 offset: 0x116
			[FieldOffset(0x118)] short El2MinDH; // type: DATA_WORD length: 0x0 offset: 0x118
			[FieldOffset(0x11a)] short El3MinD; // type: DATA_WORD length: 0x0 offset: 0x11a
			[FieldOffset(0x11c)] short El3MinDN; // type: DATA_WORD length: 0x0 offset: 0x11c
			[FieldOffset(0x11e)] short El3MinDH; // type: DATA_WORD length: 0x0 offset: 0x11e
			[FieldOffset(0x120)] short El1MaxD; // type: DATA_WORD length: 0x0 offset: 0x120
			[FieldOffset(0x122)] short El1MaxDN; // type: DATA_WORD length: 0x0 offset: 0x122
			[FieldOffset(0x124)] short El1MaxDH; // type: DATA_WORD length: 0x0 offset: 0x124
			[FieldOffset(0x126)] short El2MaxD; // type: DATA_WORD length: 0x0 offset: 0x126
			[FieldOffset(0x128)] short El2MaxDN; // type: DATA_WORD length: 0x0 offset: 0x128
			[FieldOffset(0x12a)] short El2MaxDH; // type: DATA_WORD length: 0x0 offset: 0x12a
			[FieldOffset(0x12c)] short El3MaxD; // type: DATA_WORD length: 0x0 offset: 0x12c
			[FieldOffset(0x12e)] short El3MaxDN; // type: DATA_WORD length: 0x0 offset: 0x12e
			[FieldOffset(0x130)] short El3MaxDH; // type: DATA_WORD length: 0x0 offset: 0x130
			[FieldOffset(0x132)] short El1Dur; // type: DATA_WORD length: 0x0 offset: 0x132
			[FieldOffset(0x134)] short El1DurN; // type: DATA_WORD length: 0x0 offset: 0x134
			[FieldOffset(0x136)] short El1DurH; // type: DATA_WORD length: 0x0 offset: 0x136
			[FieldOffset(0x138)] short El2Dur; // type: DATA_WORD length: 0x0 offset: 0x138
			[FieldOffset(0x13a)] short El2DurN; // type: DATA_WORD length: 0x0 offset: 0x13a
			[FieldOffset(0x13c)] short El2DurH; // type: DATA_WORD length: 0x0 offset: 0x13c
			[FieldOffset(0x13e)] short El3Dur; // type: DATA_WORD length: 0x0 offset: 0x13e
			[FieldOffset(0x140)] short El3DurN; // type: DATA_WORD length: 0x0 offset: 0x140
			[FieldOffset(0x142)] short El3DurH; // type: DATA_WORD length: 0x0 offset: 0x142
			[FieldOffset(0x144)] short ResDm; // type: DATA_WORD length: 0x0 offset: 0x144
			[FieldOffset(0x146)] short ResDmN; // type: DATA_WORD length: 0x0 offset: 0x146
			[FieldOffset(0x148)] short ResDmH; // type: DATA_WORD length: 0x0 offset: 0x148
			[FieldOffset(0x14a)] short ResMa; // type: DATA_WORD length: 0x0 offset: 0x14a
			[FieldOffset(0x14c)] short ResMaN; // type: DATA_WORD length: 0x0 offset: 0x14c
			[FieldOffset(0x14e)] short ResMaH; // type: DATA_WORD length: 0x0 offset: 0x14e
			[FieldOffset(0x150)] short ResFi; // type: DATA_WORD length: 0x0 offset: 0x150
			[FieldOffset(0x152)] short ResFiN; // type: DATA_WORD length: 0x0 offset: 0x152
			[FieldOffset(0x154)] short ResFiH; // type: DATA_WORD length: 0x0 offset: 0x154
			[FieldOffset(0x156)] short ResLi; // type: DATA_WORD length: 0x0 offset: 0x156
			[FieldOffset(0x158)] short ResLiN; // type: DATA_WORD length: 0x0 offset: 0x158
			[FieldOffset(0x15a)] short ResLiH; // type: DATA_WORD length: 0x0 offset: 0x15a
			[FieldOffset(0x15c)] short ResCo; // type: DATA_WORD length: 0x0 offset: 0x15c
			[FieldOffset(0x15e)] short ResCoN; // type: DATA_WORD length: 0x0 offset: 0x15e
			[FieldOffset(0x160)] short ResCoH; // type: DATA_WORD length: 0x0 offset: 0x160
			[FieldOffset(0x162)] short ResPo; // type: DATA_WORD length: 0x0 offset: 0x162
			[FieldOffset(0x164)] short ResPoN; // type: DATA_WORD length: 0x0 offset: 0x164
			[FieldOffset(0x166)] short ResPoH; // type: DATA_WORD length: 0x0 offset: 0x166
			[FieldOffset(0x168)] byte ColdEffect; // type: DATA_BYTE length: 0x0 offset: 0x168
			[FieldOffset(0x169)] byte ColdEffectN; // type: DATA_BYTE length: 0x0 offset: 0x169
			[FieldOffset(0x16a)] byte ColdEffectH; // type: DATA_BYTE length: 0x0 offset: 0x16a
			[FieldOffset(0x16c)] uint SendSkills; // type: DATA_DWORD_2 length: 0x0 offset: 0x16c
			[FieldOffset(0x170)] short Skill1; // type: NAME_TO_WORD length: 0x0 offset: 0x170
			[FieldOffset(0x172)] short Skill2; // type: NAME_TO_WORD length: 0x0 offset: 0x172
			[FieldOffset(0x174)] short Skill3; // type: NAME_TO_WORD length: 0x0 offset: 0x174
			[FieldOffset(0x176)] short Skill4; // type: NAME_TO_WORD length: 0x0 offset: 0x176
			[FieldOffset(0x178)] short Skill5; // type: NAME_TO_WORD length: 0x0 offset: 0x178
			[FieldOffset(0x17a)] short Skill6; // type: NAME_TO_WORD length: 0x0 offset: 0x17a
			[FieldOffset(0x17c)] short Skill7; // type: NAME_TO_WORD length: 0x0 offset: 0x17c
			[FieldOffset(0x17e)] short Skill8; // type: NAME_TO_WORD length: 0x0 offset: 0x17e
			[FieldOffset(0x198)] byte Sk1lvl; // type: DATA_BYTE length: 0x0 offset: 0x198
			[FieldOffset(0x199)] byte Sk2lvl; // type: DATA_BYTE length: 0x0 offset: 0x199
			[FieldOffset(0x19a)] byte Sk3lvl; // type: DATA_BYTE length: 0x0 offset: 0x19a
			[FieldOffset(0x19b)] byte Sk4lvl; // type: DATA_BYTE length: 0x0 offset: 0x19b
			[FieldOffset(0x19c)] byte Sk5lvl; // type: DATA_BYTE length: 0x0 offset: 0x19c
			[FieldOffset(0x19d)] byte Sk6lvl; // type: DATA_BYTE length: 0x0 offset: 0x19d
			[FieldOffset(0x19e)] byte Sk7lvl; // type: DATA_BYTE length: 0x0 offset: 0x19e
			[FieldOffset(0x19f)] byte Sk8lvl; // type: DATA_BYTE length: 0x0 offset: 0x19f
			[FieldOffset(0x1a0)] uint DamageRegen; // type: DATA_DWORD length: 0x0 offset: 0x1a0
			[FieldOffset(0x1a4)] byte SplEndDeath; // type: DATA_BYTE length: 0x0 offset: 0x1a4
			[FieldOffset(0x1a5)] byte SplGetModeChart; // type: DATA_BYTE length: 0x0 offset: 0x1a5
			[FieldOffset(0x1a6)] byte SplEndGeneric; // type: DATA_BYTE length: 0x0 offset: 0x1a6
			[FieldOffset(0x1a7)] byte SplClientEnd; // type: DATA_BYTE length: 0x0 offset: 0x1a7
			[FieldOffset(0x1a8)] byte end; // type: END length: 0x0 offset: 0x1a8
		}
	}
	#endregion

	#region unnecessary
	[StructLayout(LayoutKind.Sequential)]
	public struct SkillDescTable
	{
		short skilldesc; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
		byte skillpage; // type: DATA_BYTE length: 0x0 offset: 0x2
		byte skillrow; // type: DATA_BYTE length: 0x0 offset: 0x3
		byte skillcolumn; // type: DATA_BYTE length: 0x0 offset: 0x4
		byte ListRow; // type: DATA_BYTE length: 0x0 offset: 0x5
		byte ListPool; // type: DATA_BYTE length: 0x0 offset: 0x6
		byte iconcel; // type: DATA_BYTE length: 0x0 offset: 0x7
		short strName; // type: KEY_TO_WORD length: 0x0 offset: 0x8
		short strShort; // type: KEY_TO_WORD length: 0x0 offset: 0xa
		short strLong; // type: KEY_TO_WORD length: 0x0 offset: 0xc
		short strAlt; // type: KEY_TO_WORD length: 0x0 offset: 0xe
		short strMana; // type: KEY_TO_WORD length: 0x0 offset: 0x10
		short descdam; // type: DATA_WORD length: 0x0 offset: 0x12
		short descatt; // type: DATA_WORD length: 0x0 offset: 0x14
		uint ddamCalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x18
		uint ddamCalc2; // type: CALC_TO_DWORD length: 0x0 offset: 0x1c
		byte p1dmelem; // type: CODE_TO_BYTE length: 0x0 offset: 0x20
		byte p2dmelem; // type: CODE_TO_BYTE length: 0x0 offset: 0x21
		byte p3dmelem; // type: CODE_TO_BYTE length: 0x0 offset: 0x22
		uint p1dmmin; // type: CALC_TO_DWORD length: 0x0 offset: 0x24
		uint p2dmmin; // type: CALC_TO_DWORD length: 0x0 offset: 0x28
		uint p3dmmin; // type: CALC_TO_DWORD length: 0x0 offset: 0x2c
		uint p1dmmax; // type: CALC_TO_DWORD length: 0x0 offset: 0x30
		uint p2dmmax; // type: CALC_TO_DWORD length: 0x0 offset: 0x34
		uint p3dmmax; // type: CALC_TO_DWORD length: 0x0 offset: 0x38
		short descmissile1; // type: NAME_TO_WORD length: 0x0 offset: 0x3c
		short descmissile2; // type: NAME_TO_WORD length: 0x0 offset: 0x3e
		short descmissile3; // type: NAME_TO_WORD length: 0x0 offset: 0x40
		byte descline1; // type: DATA_BYTE length: 0x0 offset: 0x42
		byte descline2; // type: DATA_BYTE length: 0x0 offset: 0x43
		byte descline3; // type: DATA_BYTE length: 0x0 offset: 0x44
		byte descline4; // type: DATA_BYTE length: 0x0 offset: 0x45
		byte descline5; // type: DATA_BYTE length: 0x0 offset: 0x46
		byte descline6; // type: DATA_BYTE length: 0x0 offset: 0x47
		byte dsc2line1; // type: DATA_BYTE length: 0x0 offset: 0x48
		byte dsc2line2; // type: DATA_BYTE length: 0x0 offset: 0x49
		byte dsc2line3; // type: DATA_BYTE length: 0x0 offset: 0x4a
		byte dsc2line4; // type: DATA_BYTE length: 0x0 offset: 0x4b
		byte dsc3line1; // type: DATA_BYTE length: 0x0 offset: 0x4c
		byte dsc3line2; // type: DATA_BYTE length: 0x0 offset: 0x4d
		byte dsc3line3; // type: DATA_BYTE length: 0x0 offset: 0x4e
		byte dsc3line4; // type: DATA_BYTE length: 0x0 offset: 0x4f
		byte dsc3line5; // type: DATA_BYTE length: 0x0 offset: 0x50
		byte dsc3line6; // type: DATA_BYTE length: 0x0 offset: 0x51
		byte dsc3line7; // type: DATA_BYTE length: 0x0 offset: 0x52
		short desctexta1; // type: KEY_TO_WORD length: 0x0 offset: 0x54
		short desctexta2; // type: KEY_TO_WORD length: 0x0 offset: 0x56
		short desctexta3; // type: KEY_TO_WORD length: 0x0 offset: 0x58
		short desctexta4; // type: KEY_TO_WORD length: 0x0 offset: 0x5a
		short desctexta5; // type: KEY_TO_WORD length: 0x0 offset: 0x5c
		short desctexta6; // type: KEY_TO_WORD length: 0x0 offset: 0x5e
		short dsc2texta1; // type: KEY_TO_WORD length: 0x0 offset: 0x60
		short dsc2texta2; // type: KEY_TO_WORD length: 0x0 offset: 0x62
		short dsc2texta3; // type: KEY_TO_WORD length: 0x0 offset: 0x64
		short dsc2texta4; // type: KEY_TO_WORD length: 0x0 offset: 0x66
		short dsc3texta1; // type: KEY_TO_WORD length: 0x0 offset: 0x68
		short dsc3texta2; // type: KEY_TO_WORD length: 0x0 offset: 0x6a
		short dsc3texta3; // type: KEY_TO_WORD length: 0x0 offset: 0x6c
		short dsc3texta4; // type: KEY_TO_WORD length: 0x0 offset: 0x6e
		short dsc3texta5; // type: KEY_TO_WORD length: 0x0 offset: 0x70
		short dsc3texta6; // type: KEY_TO_WORD length: 0x0 offset: 0x72
		short dsc3texta7; // type: KEY_TO_WORD length: 0x0 offset: 0x74
		short desctextb1; // type: KEY_TO_WORD length: 0x0 offset: 0x76
		short desctextb2; // type: KEY_TO_WORD length: 0x0 offset: 0x78
		short desctextb3; // type: KEY_TO_WORD length: 0x0 offset: 0x7a
		short desctextb4; // type: KEY_TO_WORD length: 0x0 offset: 0x7c
		short desctextb5; // type: KEY_TO_WORD length: 0x0 offset: 0x7e
		short desctextb6; // type: KEY_TO_WORD length: 0x0 offset: 0x80
		short dsc2textb1; // type: KEY_TO_WORD length: 0x0 offset: 0x82
		short dsc2textb2; // type: KEY_TO_WORD length: 0x0 offset: 0x84
		short dsc2textb3; // type: KEY_TO_WORD length: 0x0 offset: 0x86
		short dsc2textb4; // type: KEY_TO_WORD length: 0x0 offset: 0x88
		short dsc3textb1; // type: KEY_TO_WORD length: 0x0 offset: 0x8a
		short dsc3textb2; // type: KEY_TO_WORD length: 0x0 offset: 0x8c
		short dsc3textb3; // type: KEY_TO_WORD length: 0x0 offset: 0x8e
		short dsc3textb4; // type: KEY_TO_WORD length: 0x0 offset: 0x90
		short dsc3textb5; // type: KEY_TO_WORD length: 0x0 offset: 0x92
		short dsc3textb6; // type: KEY_TO_WORD length: 0x0 offset: 0x94
		short dsc3textb7; // type: KEY_TO_WORD length: 0x0 offset: 0x96
		uint desccalca1; // type: CALC_TO_DWORD length: 0x0 offset: 0x98
		uint desccalca2; // type: CALC_TO_DWORD length: 0x0 offset: 0x9c
		uint desccalca3; // type: CALC_TO_DWORD length: 0x0 offset: 0xa0
		uint desccalca4; // type: CALC_TO_DWORD length: 0x0 offset: 0xa4
		uint desccalca5; // type: CALC_TO_DWORD length: 0x0 offset: 0xa8
		uint desccalca6; // type: CALC_TO_DWORD length: 0x0 offset: 0xac
		uint dsc2calca1; // type: CALC_TO_DWORD length: 0x0 offset: 0xb0
		uint dsc2calca2; // type: CALC_TO_DWORD length: 0x0 offset: 0xb4
		uint dsc2calca3; // type: CALC_TO_DWORD length: 0x0 offset: 0xb8
		uint dsc2calca4; // type: CALC_TO_DWORD length: 0x0 offset: 0xbc
		uint dsc3calca1; // type: CALC_TO_DWORD length: 0x0 offset: 0xc0
		uint dsc3calca2; // type: CALC_TO_DWORD length: 0x0 offset: 0xc4
		uint dsc3calca3; // type: CALC_TO_DWORD length: 0x0 offset: 0xc8
		uint dsc3calca4; // type: CALC_TO_DWORD length: 0x0 offset: 0xcc
		uint dsc3calca5; // type: CALC_TO_DWORD length: 0x0 offset: 0xd0
		uint dsc3calca6; // type: CALC_TO_DWORD length: 0x0 offset: 0xd4
		uint dsc3calca7; // type: CALC_TO_DWORD length: 0x0 offset: 0xd8
		uint desccalcb1; // type: CALC_TO_DWORD length: 0x0 offset: 0xdc
		uint desccalcb2; // type: CALC_TO_DWORD length: 0x0 offset: 0xe0
		uint desccalcb3; // type: CALC_TO_DWORD length: 0x0 offset: 0xe4
		uint desccalcb4; // type: CALC_TO_DWORD length: 0x0 offset: 0xe8
		uint desccalcb5; // type: CALC_TO_DWORD length: 0x0 offset: 0xec
		uint desccalcb6; // type: CALC_TO_DWORD length: 0x0 offset: 0xf0
		uint dsc2calcb1; // type: CALC_TO_DWORD length: 0x0 offset: 0xf4
		uint dsc2calcb2; // type: CALC_TO_DWORD length: 0x0 offset: 0xf8
		uint dsc2calcb3; // type: CALC_TO_DWORD length: 0x0 offset: 0xfc
		uint dsc2calcb4; // type: CALC_TO_DWORD length: 0x0 offset: 0x100
		uint dsc3calcb1; // type: CALC_TO_DWORD length: 0x0 offset: 0x104
		uint dsc3calcb2; // type: CALC_TO_DWORD length: 0x0 offset: 0x108
		uint dsc3calcb3; // type: CALC_TO_DWORD length: 0x0 offset: 0x10c
		uint dsc3calcb4; // type: CALC_TO_DWORD length: 0x0 offset: 0x110
		uint dsc3calcb5; // type: CALC_TO_DWORD length: 0x0 offset: 0x114
		uint dsc3calcb6; // type: CALC_TO_DWORD length: 0x0 offset: 0x118
		uint dsc3calcb7; // type: CALC_TO_DWORD length: 0x0 offset: 0x11c
		byte end; // type: END length: 0x0 offset: 0x120
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SkillsTable
	{
		short skill; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
		SkillsFlags1 flags1;
		/*uint decquant:1; // type: DATA_BIT length: 0x0 offset: 0x4
		uint lob:1; // type: DATA_BIT length: 0x1 offset: 0x4
		uint progressive:1; // type: DATA_BIT length: 0x2 offset: 0x4
		uint finishing:1; // type: DATA_BIT length: 0x3 offset: 0x4
		uint passive:1; // type: DATA_BIT length: 0x4 offset: 0x4
		uint aura:1; // type: DATA_BIT length: 0x5 offset: 0x4
		uint periodic:1; // type: DATA_BIT length: 0x6 offset: 0x4
		uint prgstack:1; // type: DATA_BIT length: 0x7 offset: 0x4
		uint InTown:1; // type: DATA_BIT length: 0x8 offset: 0x4
		uint Kick:1; // type: DATA_BIT length: 0x9 offset: 0x4
		uint InGame:1; // type: DATA_BIT length: 0xa offset: 0x4
		uint repeat:1; // type: DATA_BIT length: 0xb offset: 0x4
		uint stsuccessonly:1; // type: DATA_BIT length: 0xc offset: 0x4
		uint stsounddelay:1; // type: DATA_BIT length: 0xd offset: 0x4
		uint weaponsnd:1; // type: DATA_BIT length: 0xe offset: 0x4
		uint immediate:1; // type: DATA_BIT length: 0xf offset: 0x4
		uint noammo:1; // type: DATA_BIT length: 0x10 offset: 0x4
		uint enhanceable:1; // type: DATA_BIT length: 0x11 offset: 0x4
		uint durability:1; // type: DATA_BIT length: 0x12 offset: 0x4
		uint UseAttackRate:1; // type: DATA_BIT length: 0x13 offset: 0x4
		uint TargetableOnly:1; // type: DATA_BIT length: 0x14 offset: 0x4
		uint SearchEnemyXY:1; // type: DATA_BIT length: 0x15 offset: 0x4
		uint SearchEnemyNear:1; // type: DATA_BIT length: 0x16 offset: 0x4
		uint SearchOpenXY:1; // type: DATA_BIT length: 0x17 offset: 0x4
		uint TargetCorpse:1; // type: DATA_BIT length: 0x18 offset: 0x4
		uint TargetPet:1; // type: DATA_BIT length: 0x19 offset: 0x4
		uint TargetAlly:1; // type: DATA_BIT length: 0x1a offset: 0x4
		uint TargetItem:1; // type: DATA_BIT length: 0x1b offset: 0x4
		uint AttackNoMana:1; // type: DATA_BIT length: 0x1c offset: 0x4
		uint ItemTgtDo:1; // type: DATA_BIT length: 0x1d offset: 0x4
		uint leftskill:1; // type: DATA_BIT length: 0x1e offset: 0x4
		uint interrupt:1; // type: DATA_BIT length: 0x1f offset: 0x4*/
		SkillsFlags2 flags2;
		/*uint TgtPlaceCheck:1; // type: DATA_BIT length: 0x0 offset: 0x8
		uint ItemCheckStart:1; // type: DATA_BIT length: 0x1 offset: 0x8
		uint ItemCltCheckStart:1; // type: DATA_BIT length: 0x2 offset: 0x8
		uint general:1; // type: DATA_BIT length: 0x3 offset: 0x8
		uint scroll:1; // type: DATA_BIT length: 0x4 offset: 0x8
		uint usemanaondo:1; // type: DATA_BIT length: 0x5 offset: 0x8
		uint warp:1; // type: DATA_BIT length: 0x6 offset: 0x8*/
		byte charclass; // type: CODE_TO_BYTE length: 0x0 offset: 0xc
		byte anim; // type: CODE_TO_BYTE length: 0x0 offset: 0x10
		byte monanim; // type: CODE_TO_BYTE length: 0x0 offset: 0x11
		byte seqtrans; // type: CODE_TO_BYTE length: 0x0 offset: 0x12
		byte seqnum; // type: DATA_BYTE length: 0x0 offset: 0x13
		byte range; // type: CODE_TO_BYTE length: 0x0 offset: 0x14
		byte SelectProc; // type: DATA_BYTE length: 0x0 offset: 0x15
		byte seqinput; // type: DATA_BYTE length: 0x0 offset: 0x16
		short itypea1; // type: CODE_TO_WORD length: 0x0 offset: 0x18
		short itypea2; // type: CODE_TO_WORD length: 0x0 offset: 0x1a
		short itypea3; // type: CODE_TO_WORD length: 0x0 offset: 0x1c
		short itypeb1; // type: CODE_TO_WORD length: 0x0 offset: 0x1e
		short itypeb2; // type: CODE_TO_WORD length: 0x0 offset: 0x20
		short itypeb3; // type: CODE_TO_WORD length: 0x0 offset: 0x22
		short etypea1; // type: CODE_TO_WORD length: 0x0 offset: 0x24
		short etypea2; // type: CODE_TO_WORD length: 0x0 offset: 0x26
		short etypeb1; // type: CODE_TO_WORD length: 0x0 offset: 0x28
		short etypeb2; // type: CODE_TO_WORD length: 0x0 offset: 0x2a
		short srvstfunc; // type: DATA_WORD length: 0x0 offset: 0x2c
		short srvdofunc; // type: DATA_WORD length: 0x0 offset: 0x2e
		short srvprgfunc1; // type: DATA_WORD length: 0x0 offset: 0x30
		short srvprgfunc2; // type: DATA_WORD length: 0x0 offset: 0x32
		short srvprgfunc3; // type: DATA_WORD length: 0x0 offset: 0x34
		uint prgcalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x38
		uint prgcalc2; // type: CALC_TO_DWORD length: 0x0 offset: 0x3c
		uint prgcalc3; // type: CALC_TO_DWORD length: 0x0 offset: 0x40
		byte prgdam; // type: DATA_BYTE length: 0x0 offset: 0x44
		short srvmissile; // type: NAME_TO_WORD length: 0x0 offset: 0x46
		short srvmissilea; // type: NAME_TO_WORD length: 0x0 offset: 0x48
		short srvmissileb; // type: NAME_TO_WORD length: 0x0 offset: 0x4a
		short srvmissilec; // type: NAME_TO_WORD length: 0x0 offset: 0x4c
		short srvoverlay; // type: NAME_TO_WORD length: 0x0 offset: 0x4e
		uint aurafilter; // type: DATA_DWORD length: 0x0 offset: 0x50
		short aurastat1; // type: NAME_TO_WORD length: 0x0 offset: 0x54
		short aurastat2; // type: NAME_TO_WORD length: 0x0 offset: 0x56
		short aurastat3; // type: NAME_TO_WORD length: 0x0 offset: 0x58
		short aurastat4; // type: NAME_TO_WORD length: 0x0 offset: 0x5a
		short aurastat5; // type: NAME_TO_WORD length: 0x0 offset: 0x5c
		short aurastat6; // type: NAME_TO_WORD length: 0x0 offset: 0x5e
		uint auralencalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x60
		uint aurarangecalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x64
		uint aurastatcalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x68
		uint aurastatcalc2; // type: CALC_TO_DWORD length: 0x0 offset: 0x6c
		uint aurastatcalc3; // type: CALC_TO_DWORD length: 0x0 offset: 0x70
		uint aurastatcalc4; // type: CALC_TO_DWORD length: 0x0 offset: 0x74
		uint aurastatcalc5; // type: CALC_TO_DWORD length: 0x0 offset: 0x78
		uint aurastatcalc6; // type: CALC_TO_DWORD length: 0x0 offset: 0x7c
		short aurastate; // type: NAME_TO_WORD length: 0x0 offset: 0x80
		short auratargetstate; // type: NAME_TO_WORD length: 0x0 offset: 0x82
		short auraevent1; // type: NAME_TO_WORD length: 0x0 offset: 0x84
		short auraevent2; // type: NAME_TO_WORD length: 0x0 offset: 0x86
		short auraevent3; // type: NAME_TO_WORD length: 0x0 offset: 0x88
		short auraeventfunc1; // type: DATA_WORD length: 0x0 offset: 0x8a
		short auraeventfunc2; // type: DATA_WORD length: 0x0 offset: 0x8c
		short auraeventfunc3; // type: DATA_WORD length: 0x0 offset: 0x8e
		short auratgtevent; // type: NAME_TO_WORD length: 0x0 offset: 0x90
		short auratgteventfunc; // type: DATA_WORD length: 0x0 offset: 0x92
		short passivestate; // type: NAME_TO_WORD length: 0x0 offset: 0x94
		short passiveitype; // type: CODE_TO_WORD length: 0x0 offset: 0x96
		short passivestat1; // type: NAME_TO_WORD length: 0x0 offset: 0x98
		short passivestat2; // type: NAME_TO_WORD length: 0x0 offset: 0x9a
		short passivestat3; // type: NAME_TO_WORD length: 0x0 offset: 0x9c
		short passivestat4; // type: NAME_TO_WORD length: 0x0 offset: 0x9e
		short passivestat5; // type: NAME_TO_WORD length: 0x0 offset: 0xa0
		uint passivecalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0xa4
		uint passivecalc2; // type: CALC_TO_DWORD length: 0x0 offset: 0xa8
		uint passivecalc3; // type: CALC_TO_DWORD length: 0x0 offset: 0xac
		uint passivecalc4; // type: CALC_TO_DWORD length: 0x0 offset: 0xb0
		uint passivecalc5; // type: CALC_TO_DWORD length: 0x0 offset: 0xb4
		short passiveevent; // type: NAME_TO_WORD length: 0x0 offset: 0xb8
		short passiveeventfunc; // type: DATA_WORD length: 0x0 offset: 0xba
		short summon; // type: NAME_TO_WORD length: 0x0 offset: 0xbc
		short pettype; // type: NAME_TO_WORD_2 length: 0x0 offset: 0xbe
		byte summode; // type: CODE_TO_BYTE length: 0x0 offset: 0xbf
		uint petmax; // type: CALC_TO_DWORD length: 0x0 offset: 0xc0
		short sumskill1; // type: NAME_TO_WORD length: 0x0 offset: 0xc4
		short sumskill2; // type: NAME_TO_WORD length: 0x0 offset: 0xc6
		short sumskill3; // type: NAME_TO_WORD length: 0x0 offset: 0xc8
		short sumskill4; // type: NAME_TO_WORD length: 0x0 offset: 0xca
		short sumskill5; // type: NAME_TO_WORD length: 0x0 offset: 0xcc
		uint sumsk1calc; // type: CALC_TO_DWORD length: 0x0 offset: 0xd0
		uint sumsk2calc; // type: CALC_TO_DWORD length: 0x0 offset: 0xd4
		uint sumsk3calc; // type: CALC_TO_DWORD length: 0x0 offset: 0xd8
		uint sumsk4calc; // type: CALC_TO_DWORD length: 0x0 offset: 0xdc
		uint sumsk5calc; // type: CALC_TO_DWORD length: 0x0 offset: 0xe0
		short sumumod; // type: DATA_WORD length: 0x0 offset: 0xe4
		short sumoverlay; // type: NAME_TO_WORD length: 0x0 offset: 0xe6
		short cltmissile; // type: NAME_TO_WORD length: 0x0 offset: 0xe8
		short cltmissilea; // type: NAME_TO_WORD length: 0x0 offset: 0xea
		short cltmissileb; // type: NAME_TO_WORD length: 0x0 offset: 0xec
		short cltmissilec; // type: NAME_TO_WORD length: 0x0 offset: 0xee
		short cltmissiled; // type: NAME_TO_WORD length: 0x0 offset: 0xf0
		short cltstfunc; // type: DATA_WORD length: 0x0 offset: 0xf2
		short cltdofunc; // type: DATA_WORD length: 0x0 offset: 0xf4
		short cltprgfunc1; // type: DATA_WORD length: 0x0 offset: 0xf6
		short cltprgfunc2; // type: DATA_WORD length: 0x0 offset: 0xf8
		short cltprgfunc3; // type: DATA_WORD length: 0x0 offset: 0xfa
		short stsound; // type: NAME_TO_WORD length: 0x0 offset: 0xfc
		short stsoundclass; // type: NAME_TO_WORD length: 0x0 offset: 0xfe
		short dosound; // type: NAME_TO_WORD length: 0x0 offset: 0x100
		short dosoundA; // type: NAME_TO_WORD length: 0x0 offset: 0x102
		short dosoundB; // type: NAME_TO_WORD length: 0x0 offset: 0x104
		short castoverlay; // type: NAME_TO_WORD length: 0x0 offset: 0x106
		short tgtoverlay; // type: NAME_TO_WORD length: 0x0 offset: 0x108
		short tgtsound; // type: NAME_TO_WORD length: 0x0 offset: 0x10a
		short prgoverlay; // type: NAME_TO_WORD length: 0x0 offset: 0x10c
		short prgsound; // type: NAME_TO_WORD length: 0x0 offset: 0x10e
		short cltoverlaya; // type: NAME_TO_WORD length: 0x0 offset: 0x110
		short cltoverlayb; // type: NAME_TO_WORD length: 0x0 offset: 0x112
		uint cltcalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x114
		uint cltcalc2; // type: CALC_TO_DWORD length: 0x0 offset: 0x118
		uint cltcalc3; // type: CALC_TO_DWORD length: 0x0 offset: 0x11c
		byte ItemTarget; // type: DATA_BYTE length: 0x0 offset: 0x120
		short ItemCastSound; // type: NAME_TO_WORD length: 0x0 offset: 0x122
		short ItemCastOverlay; // type: NAME_TO_WORD length: 0x0 offset: 0x124
		uint perdelay; // type: CALC_TO_DWORD length: 0x0 offset: 0x128
		short maxlvl; // type: DATA_WORD length: 0x0 offset: 0x12c
		short ResultFlags; // type: DATA_WORD length: 0x0 offset: 0x12e
		uint HitFlags; // type: DATA_DWORD_2 length: 0x0 offset: 0x130
		uint HitClass; // type: DATA_DWORD_2 length: 0x0 offset: 0x134
		uint calc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x138
		uint calc2; // type: CALC_TO_DWORD length: 0x0 offset: 0x13c
		uint calc3; // type: CALC_TO_DWORD length: 0x0 offset: 0x140
		uint calc4; // type: CALC_TO_DWORD length: 0x0 offset: 0x144
		uint Param1; // type: DATA_DWORD_2 length: 0x0 offset: 0x148
		uint Param2; // type: DATA_DWORD_2 length: 0x0 offset: 0x14c
		uint Param3; // type: DATA_DWORD_2 length: 0x0 offset: 0x150
		uint Param4; // type: DATA_DWORD_2 length: 0x0 offset: 0x154
		uint Param5; // type: DATA_DWORD_2 length: 0x0 offset: 0x158
		uint Param6; // type: DATA_DWORD_2 length: 0x0 offset: 0x15c
		uint Param7; // type: DATA_DWORD_2 length: 0x0 offset: 0x160
		uint Param8; // type: DATA_DWORD_2 length: 0x0 offset: 0x164
		byte weapsel; // type: DATA_BYTE length: 0x0 offset: 0x168
		short ItemEffect; // type: DATA_WORD length: 0x0 offset: 0x16a
		short ItemCltEffect; // type: DATA_WORD length: 0x0 offset: 0x16c
		uint skpoints; // type: CALC_TO_DWORD length: 0x0 offset: 0x170
		short reqlevel; // type: DATA_WORD length: 0x0 offset: 0x174
		short reqstr; // type: DATA_WORD length: 0x0 offset: 0x176
		short reqdex; // type: DATA_WORD length: 0x0 offset: 0x178
		short reqint; // type: DATA_WORD length: 0x0 offset: 0x17a
		short reqvit; // type: DATA_WORD length: 0x0 offset: 0x17c
		short reqskill1; // type: NAME_TO_WORD length: 0x0 offset: 0x17e
		short reqskill2; // type: NAME_TO_WORD length: 0x0 offset: 0x180
		short reqskill3; // type: NAME_TO_WORD length: 0x0 offset: 0x182
		short startmana; // type: DATA_WORD length: 0x0 offset: 0x184
		short minmana; // type: DATA_WORD length: 0x0 offset: 0x186
		short manashift; // type: DATA_WORD length: 0x0 offset: 0x188
		short mana; // type: DATA_WORD length: 0x0 offset: 0x18a
		short lvlmana; // type: DATA_WORD length: 0x0 offset: 0x18c
		byte attackrank; // type: DATA_BYTE length: 0x0 offset: 0x18e
		byte lineofsight; // type: DATA_BYTE length: 0x0 offset: 0x18f
		uint delay; // type: CALC_TO_DWORD length: 0x0 offset: 0x190
		short skilldesc; // type: NAME_TO_WORD length: 0x0 offset: 0x194
		uint ToHit; // type: DATA_DWORD length: 0x0 offset: 0x198
		uint LevToHit; // type: DATA_DWORD length: 0x0 offset: 0x19c
		uint ToHitCalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x1a0
		byte HitShift; // type: DATA_BYTE length: 0x0 offset: 0x1a4
		byte SrcDam; // type: DATA_BYTE length: 0x0 offset: 0x1a5
		uint MinDam; // type: DATA_DWORD length: 0x0 offset: 0x1a8
		uint MaxDam; // type: DATA_DWORD length: 0x0 offset: 0x1ac
		uint MinLevDam1; // type: DATA_DWORD length: 0x0 offset: 0x1b0
		uint MinLevDam2; // type: DATA_DWORD length: 0x0 offset: 0x1b4
		uint MinLevDam3; // type: DATA_DWORD length: 0x0 offset: 0x1b8
		uint MinLevDam4; // type: DATA_DWORD length: 0x0 offset: 0x1bc
		uint MinLevDam5; // type: DATA_DWORD length: 0x0 offset: 0x1c0
		uint MaxLevDam1; // type: DATA_DWORD length: 0x0 offset: 0x1c4
		uint MaxLevDam2; // type: DATA_DWORD length: 0x0 offset: 0x1c8
		uint MaxLevDam3; // type: DATA_DWORD length: 0x0 offset: 0x1cc
		uint MaxLevDam4; // type: DATA_DWORD length: 0x0 offset: 0x1d0
		uint MaxLevDam5; // type: DATA_DWORD length: 0x0 offset: 0x1d4
		uint DmgSymPerCalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x1d8
		byte EType; // type: CODE_TO_BYTE length: 0x0 offset: 0x1dc
		uint EMin; // type: DATA_DWORD length: 0x0 offset: 0x1e0
		uint EMax; // type: DATA_DWORD length: 0x0 offset: 0x1e4
		uint EMinLev1; // type: DATA_DWORD length: 0x0 offset: 0x1e8
		uint EMinLev2; // type: DATA_DWORD length: 0x0 offset: 0x1ec
		uint EMinLev3; // type: DATA_DWORD length: 0x0 offset: 0x1f0
		uint EMinLev4; // type: DATA_DWORD length: 0x0 offset: 0x1f4
		uint EMinLev5; // type: DATA_DWORD length: 0x0 offset: 0x1f8
		uint EMaxLev1; // type: DATA_DWORD length: 0x0 offset: 0x1fc
		uint EMaxLev2; // type: DATA_DWORD length: 0x0 offset: 0x200
		uint EMaxLev3; // type: DATA_DWORD length: 0x0 offset: 0x204
		uint EMaxLev4; // type: DATA_DWORD length: 0x0 offset: 0x208
		uint EMaxLev5; // type: DATA_DWORD length: 0x0 offset: 0x20c
		uint EDmgSymPerCalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x210
		uint ELen; // type: DATA_DWORD length: 0x0 offset: 0x214
		uint ELevLen1; // type: DATA_DWORD length: 0x0 offset: 0x218
		uint ELevLen2; // type: DATA_DWORD length: 0x0 offset: 0x21c
		uint ELevLen3; // type: DATA_DWORD length: 0x0 offset: 0x220
		uint ELenSymPerCalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x224
		byte restrict; // type: DATA_BYTE length: 0x0 offset: 0x228
		short state1; // type: NAME_TO_WORD length: 0x0 offset: 0x22a
		short state2; // type: NAME_TO_WORD length: 0x0 offset: 0x22c
		short state3; // type: NAME_TO_WORD length: 0x0 offset: 0x22e
		byte aitype; // type: DATA_BYTE length: 0x0 offset: 0x230
		short aibonus; // type: DATA_WORD length: 0x0 offset: 0x232
		uint costMult; // type: DATA_DWORD length: 0x0 offset: 0x234
		uint costAdd; // type: DATA_DWORD length: 0x0 offset: 0x238
		byte end; // type: END length: 0x0 offset: 0x23c
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ObjectsTable
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3f)]
		string Name; // type: DATA_ASCII length: 0x3f offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
		string Token; // type: DATA_ASCII length: 0x2 offset: 0xc0
		byte SpawnMax; // type: DATA_BYTE length: 0x0 offset: 0xc3
		byte Selectable0; // type: DATA_BYTE length: 0x0 offset: 0xc4
		byte Selectable1; // type: DATA_BYTE length: 0x0 offset: 0xc5
		byte Selectable2; // type: DATA_BYTE length: 0x0 offset: 0xc6
		byte Selectable3; // type: DATA_BYTE length: 0x0 offset: 0xc7
		byte Selectable4; // type: DATA_BYTE length: 0x0 offset: 0xc8
		byte Selectable5; // type: DATA_BYTE length: 0x0 offset: 0xc9
		byte Selectable6; // type: DATA_BYTE length: 0x0 offset: 0xca
		byte Selectable7; // type: DATA_BYTE length: 0x0 offset: 0xcb
		byte TrapProb; // type: DATA_BYTE length: 0x0 offset: 0xcc
		uint SizeX; // type: DATA_DWORD length: 0x0 offset: 0xd0
		uint SizeY; // type: DATA_DWORD length: 0x0 offset: 0xd4
		uint FrameCnt0; // type: DATA_DWORD length: 0x0 offset: 0xd8
		uint FrameCnt1; // type: DATA_DWORD length: 0x0 offset: 0xdc
		uint FrameCnt2; // type: DATA_DWORD length: 0x0 offset: 0xe0
		uint FrameCnt3; // type: DATA_DWORD length: 0x0 offset: 0xe4
		uint FrameCnt4; // type: DATA_DWORD length: 0x0 offset: 0xe8
		uint FrameCnt5; // type: DATA_DWORD length: 0x0 offset: 0xec
		uint FrameCnt6; // type: DATA_DWORD length: 0x0 offset: 0xf0
		uint FrameCnt7; // type: DATA_DWORD length: 0x0 offset: 0xf4
		short FrameDelta0; // type: DATA_WORD length: 0x0 offset: 0xf8
		short FrameDelta1; // type: DATA_WORD length: 0x0 offset: 0xfa
		short FrameDelta2; // type: DATA_WORD length: 0x0 offset: 0xfc
		short FrameDelta3; // type: DATA_WORD length: 0x0 offset: 0xfe
		short FrameDelta4; // type: DATA_WORD length: 0x0 offset: 0x100
		short FrameDelta5; // type: DATA_WORD length: 0x0 offset: 0x102
		short FrameDelta6; // type: DATA_WORD length: 0x0 offset: 0x104
		short FrameDelta7; // type: DATA_WORD length: 0x0 offset: 0x106
		byte CycleAnim0; // type: DATA_BYTE length: 0x0 offset: 0x108
		byte CycleAnim1; // type: DATA_BYTE length: 0x0 offset: 0x109
		byte CycleAnim2; // type: DATA_BYTE length: 0x0 offset: 0x10a
		byte CycleAnim3; // type: DATA_BYTE length: 0x0 offset: 0x10b
		byte CycleAnim4; // type: DATA_BYTE length: 0x0 offset: 0x10c
		byte CycleAnim5; // type: DATA_BYTE length: 0x0 offset: 0x10d
		byte CycleAnim6; // type: DATA_BYTE length: 0x0 offset: 0x10e
		byte CycleAnim7; // type: DATA_BYTE length: 0x0 offset: 0x10f
		byte Lit0; // type: DATA_BYTE length: 0x0 offset: 0x110
		byte Lit1; // type: DATA_BYTE length: 0x0 offset: 0x111
		byte Lit2; // type: DATA_BYTE length: 0x0 offset: 0x112
		byte Lit3; // type: DATA_BYTE length: 0x0 offset: 0x113
		byte Lit4; // type: DATA_BYTE length: 0x0 offset: 0x114
		byte Lit5; // type: DATA_BYTE length: 0x0 offset: 0x115
		byte Lit6; // type: DATA_BYTE length: 0x0 offset: 0x116
		byte Lit7; // type: DATA_BYTE length: 0x0 offset: 0x117
		byte BlocksLight0; // type: DATA_BYTE length: 0x0 offset: 0x118
		byte BlocksLight1; // type: DATA_BYTE length: 0x0 offset: 0x119
		byte BlocksLight2; // type: DATA_BYTE length: 0x0 offset: 0x11a
		byte BlocksLight3; // type: DATA_BYTE length: 0x0 offset: 0x11b
		byte BlocksLight4; // type: DATA_BYTE length: 0x0 offset: 0x11c
		byte BlocksLight5; // type: DATA_BYTE length: 0x0 offset: 0x11d
		byte BlocksLight6; // type: DATA_BYTE length: 0x0 offset: 0x11e
		byte BlocksLight7; // type: DATA_BYTE length: 0x0 offset: 0x11f
		byte HasCollision0; // type: DATA_BYTE length: 0x0 offset: 0x120
		byte HasCollision1; // type: DATA_BYTE length: 0x0 offset: 0x121
		byte HasCollision2; // type: DATA_BYTE length: 0x0 offset: 0x122
		byte HasCollision3; // type: DATA_BYTE length: 0x0 offset: 0x123
		byte HasCollision4; // type: DATA_BYTE length: 0x0 offset: 0x124
		byte HasCollision5; // type: DATA_BYTE length: 0x0 offset: 0x125
		byte HasCollision6; // type: DATA_BYTE length: 0x0 offset: 0x126
		byte HasCollision7; // type: DATA_BYTE length: 0x0 offset: 0x127
		byte IsAttackable0; // type: DATA_BYTE length: 0x0 offset: 0x128
		byte Start0; // type: DATA_BYTE length: 0x0 offset: 0x129
		byte Start1; // type: DATA_BYTE length: 0x0 offset: 0x12a
		byte Start2; // type: DATA_BYTE length: 0x0 offset: 0x12b
		byte Start3; // type: DATA_BYTE length: 0x0 offset: 0x12c
		byte Start4; // type: DATA_BYTE length: 0x0 offset: 0x12d
		byte Start5; // type: DATA_BYTE length: 0x0 offset: 0x12e
		byte Start6; // type: DATA_BYTE length: 0x0 offset: 0x12f
		byte Start7; // type: DATA_BYTE length: 0x0 offset: 0x130
		byte OrderFlag0; // type: DATA_BYTE length: 0x0 offset: 0x131
		byte OrderFlag1; // type: DATA_BYTE length: 0x0 offset: 0x132
		byte OrderFlag2; // type: DATA_BYTE length: 0x0 offset: 0x133
		byte OrderFlag3; // type: DATA_BYTE length: 0x0 offset: 0x134
		byte OrderFlag4; // type: DATA_BYTE length: 0x0 offset: 0x135
		byte OrderFlag5; // type: DATA_BYTE length: 0x0 offset: 0x136
		byte OrderFlag6; // type: DATA_BYTE length: 0x0 offset: 0x137
		byte OrderFlag7; // type: DATA_BYTE length: 0x0 offset: 0x138
		byte EnvEffect; // type: DATA_BYTE length: 0x0 offset: 0x139
		byte IsDoor; // type: DATA_BYTE length: 0x0 offset: 0x13a
		byte BlocksVis; // type: DATA_BYTE length: 0x0 offset: 0x13b
		byte Orientation; // type: DATA_BYTE length: 0x0 offset: 0x13c
		byte PreOperate; // type: DATA_BYTE length: 0x0 offset: 0x13d
		byte Trans; // type: DATA_BYTE length: 0x0 offset: 0x13e
		byte Mode0; // type: DATA_BYTE length: 0x0 offset: 0x13f
		byte Mode1; // type: DATA_BYTE length: 0x0 offset: 0x140
		byte Mode2; // type: DATA_BYTE length: 0x0 offset: 0x141
		byte Mode3; // type: DATA_BYTE length: 0x0 offset: 0x142
		byte Mode4; // type: DATA_BYTE length: 0x0 offset: 0x143
		byte Mode5; // type: DATA_BYTE length: 0x0 offset: 0x144
		byte Mode6; // type: DATA_BYTE length: 0x0 offset: 0x145
		byte Mode7; // type: DATA_BYTE length: 0x0 offset: 0x146
		uint Xoffset; // type: DATA_DWORD_2 length: 0x0 offset: 0x148
		uint Yoffset; // type: DATA_DWORD_2 length: 0x0 offset: 0x14c
		byte Draw; // type: DATA_BYTE length: 0x0 offset: 0x150
		byte HD; // type: DATA_BYTE length: 0x0 offset: 0x151
		byte TR; // type: DATA_BYTE length: 0x0 offset: 0x152
		byte LG; // type: DATA_BYTE length: 0x0 offset: 0x153
		byte RA; // type: DATA_BYTE length: 0x0 offset: 0x154
		byte LA; // type: DATA_BYTE length: 0x0 offset: 0x155
		byte RH; // type: DATA_BYTE length: 0x0 offset: 0x156
		byte LH; // type: DATA_BYTE length: 0x0 offset: 0x157
		byte SH; // type: DATA_BYTE length: 0x0 offset: 0x158
		byte S1; // type: DATA_BYTE length: 0x0 offset: 0x159
		byte S2; // type: DATA_BYTE length: 0x0 offset: 0x15a
		byte S3; // type: DATA_BYTE length: 0x0 offset: 0x15b
		byte S4; // type: DATA_BYTE length: 0x0 offset: 0x15c
		byte S5; // type: DATA_BYTE length: 0x0 offset: 0x15d
		byte S6; // type: DATA_BYTE length: 0x0 offset: 0x15e
		byte S7; // type: DATA_BYTE length: 0x0 offset: 0x15f
		byte S8; // type: DATA_BYTE length: 0x0 offset: 0x160
		byte TotalPieces; // type: DATA_BYTE length: 0x0 offset: 0x161
		byte XSpace; // type: DATA_BYTE length: 0x0 offset: 0x162
		byte YSpace; // type: DATA_BYTE length: 0x0 offset: 0x163
		byte Red; // type: DATA_BYTE length: 0x0 offset: 0x164
		byte Green; // type: DATA_BYTE length: 0x0 offset: 0x165
		byte Blue; // type: DATA_BYTE length: 0x0 offset: 0x166
		byte SubClass; // type: DATA_BYTE length: 0x0 offset: 0x167
		uint NameOffset; // type: DATA_DWORD_2 length: 0x0 offset: 0x168
		byte MonsterOK; // type: DATA_BYTE length: 0x0 offset: 0x16d
		byte OperateRange; // type: DATA_BYTE length: 0x0 offset: 0x16e
		byte ShrineFunction; // type: DATA_BYTE length: 0x0 offset: 0x16f
		byte Act; // type: DATA_BYTE length: 0x0 offset: 0x170
		byte Lockable; // type: DATA_BYTE length: 0x0 offset: 0x171
		byte Gore; // type: DATA_BYTE length: 0x0 offset: 0x172
		byte Restore; // type: DATA_BYTE length: 0x0 offset: 0x173
		byte RestoreVirgins; // type: DATA_BYTE length: 0x0 offset: 0x174
		byte Sync; // type: DATA_BYTE length: 0x0 offset: 0x175
		uint Parm0; // type: DATA_DWORD_2 length: 0x0 offset: 0x178
		uint Parm1; // type: DATA_DWORD_2 length: 0x0 offset: 0x17c
		uint Parm2; // type: DATA_DWORD_2 length: 0x0 offset: 0x180
		uint Parm3; // type: DATA_DWORD_2 length: 0x0 offset: 0x184
		uint Parm4; // type: DATA_DWORD_2 length: 0x0 offset: 0x188
		uint Parm5; // type: DATA_DWORD_2 length: 0x0 offset: 0x18c
		uint Parm6; // type: DATA_DWORD_2 length: 0x0 offset: 0x190
		uint Parm7; // type: DATA_DWORD_2 length: 0x0 offset: 0x194
		byte nTgtFX; // type: DATA_BYTE length: 0x0 offset: 0x198
		byte nTgtFY; // type: DATA_BYTE length: 0x0 offset: 0x199
		byte nTgtBX; // type: DATA_BYTE length: 0x0 offset: 0x19a
		byte nTgtBY; // type: DATA_BYTE length: 0x0 offset: 0x19b
		byte Damage; // type: DATA_BYTE length: 0x0 offset: 0x19c
		byte CollisionSubst; // type: DATA_BYTE length: 0x0 offset: 0x19d
		uint Left; // type: DATA_DWORD_2 length: 0x0 offset: 0x1a0
		uint Top; // type: DATA_DWORD_2 length: 0x0 offset: 0x1a4
		uint Width; // type: DATA_DWORD_2 length: 0x0 offset: 0x1a8
		uint Height; // type: DATA_DWORD_2 length: 0x0 offset: 0x1ac
		byte Beta; // type: DATA_BYTE length: 0x0 offset: 0x1b0
		byte InitFn; // type: DATA_BYTE length: 0x0 offset: 0x1b1
		byte PopulateFn; // type: DATA_BYTE length: 0x0 offset: 0x1b2
		byte OperateFn; // type: DATA_BYTE length: 0x0 offset: 0x1b3
		byte ClientFn; // type: DATA_BYTE length: 0x0 offset: 0x1b4
		byte Overlay; // type: DATA_BYTE length: 0x0 offset: 0x1b5
		byte BlockMissile; // type: DATA_BYTE length: 0x0 offset: 0x1b6
		byte DrawUnder; // type: DATA_BYTE length: 0x0 offset: 0x1b7
		byte OpenWarp; // type: DATA_BYTE length: 0x0 offset: 0x1b8
		uint AutoMap; // type: DATA_DWORD length: 0x0 offset: 0x1bc
		byte end; // type: END length: 0x0 offset: 0x1c0
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MissilesTable
	{
		short Missile; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
		MissilesFlags flags;
		/*uint LastCollide:1; // type: DATA_BIT length: 0x0 offset: 0x4
		uint Explosion:1; // type: DATA_BIT length: 0x1 offset: 0x4
		uint Pierce:1; // type: DATA_BIT length: 0x2 offset: 0x4
		uint CanSlow:1; // type: DATA_BIT length: 0x3 offset: 0x4
		uint CanDestroy:1; // type: DATA_BIT length: 0x4 offset: 0x4
		uint ClientSend:1; // type: DATA_BIT length: 0x5 offset: 0x4
		uint GetHit:1; // type: DATA_BIT length: 0x6 offset: 0x4
		uint SoftHit:1; // type: DATA_BIT length: 0x7 offset: 0x4
		uint ApplyMastery:1; // type: DATA_BIT length: 0x8 offset: 0x4
		uint ReturnFire:1; // type: DATA_BIT length: 0x9 offset: 0x4
		uint Town:1; // type: DATA_BIT length: 0xa offset: 0x4
		uint SrcTown:1; // type: DATA_BIT length: 0xb offset: 0x4
		uint NoMultiShot:1; // type: DATA_BIT length: 0xc offset: 0x4
		uint NoUniqueMod:1; // type: DATA_BIT length: 0xd offset: 0x4
		uint Half2HSrc:1; // type: DATA_BIT length: 0xe offset: 0x4
		uint MissileSkill:1; // type: DATA_BIT length: 0xf offset: 0x4*/
		short pCltDoFunc; // type: DATA_WORD length: 0x0 offset: 0x8
		short pCltHitFunc; // type: DATA_WORD length: 0x0 offset: 0xa
		short pSrvDoFunc; // type: DATA_WORD length: 0x0 offset: 0xc
		short pSrvHitFunc; // type: DATA_WORD length: 0x0 offset: 0xe
		short pSrvDmgFunc; // type: DATA_WORD length: 0x0 offset: 0x10
		short TravelSound; // type: NAME_TO_WORD length: 0x0 offset: 0x12
		short HitSound; // type: NAME_TO_WORD length: 0x0 offset: 0x14
		short ExplosionMissile; // type: NAME_TO_WORD length: 0x0 offset: 0x16
		short SubMissile1; // type: NAME_TO_WORD length: 0x0 offset: 0x18
		short SubMissile2; // type: NAME_TO_WORD length: 0x0 offset: 0x1a
		short SubMissile3; // type: NAME_TO_WORD length: 0x0 offset: 0x1c
		short CltSubMissile1; // type: NAME_TO_WORD length: 0x0 offset: 0x1e
		short CltSubMissile2; // type: NAME_TO_WORD length: 0x0 offset: 0x20
		short CltSubMissile3; // type: NAME_TO_WORD length: 0x0 offset: 0x22
		short HitSubMissile1; // type: NAME_TO_WORD length: 0x0 offset: 0x24
		short HitSubMissile2; // type: NAME_TO_WORD length: 0x0 offset: 0x26
		short HitSubMissile3; // type: NAME_TO_WORD length: 0x0 offset: 0x28
		short HitSubMissile4; // type: NAME_TO_WORD length: 0x0 offset: 0x2a
		short CltHitSubMissile1; // type: NAME_TO_WORD length: 0x0 offset: 0x2c
		short CltHitSubMissile2; // type: NAME_TO_WORD length: 0x0 offset: 0x2e
		short CltHitSubMissile3; // type: NAME_TO_WORD length: 0x0 offset: 0x30
		short CltHitSubMissile4; // type: NAME_TO_WORD length: 0x0 offset: 0x32
		short ProgSound; // type: NAME_TO_WORD length: 0x0 offset: 0x34
		short ProgOverlay; // type: NAME_TO_WORD length: 0x0 offset: 0x36
		uint Param1; // type: DATA_DWORD_2 length: 0x0 offset: 0x38
		uint Param2; // type: DATA_DWORD_2 length: 0x0 offset: 0x3c
		uint Param3; // type: DATA_DWORD_2 length: 0x0 offset: 0x40
		uint Param4; // type: DATA_DWORD_2 length: 0x0 offset: 0x44
		uint Param5; // type: DATA_DWORD_2 length: 0x0 offset: 0x48
		uint sHitPar1; // type: DATA_DWORD_2 length: 0x0 offset: 0x4c
		uint sHitPar2; // type: DATA_DWORD_2 length: 0x0 offset: 0x50
		uint sHitPar3; // type: DATA_DWORD_2 length: 0x0 offset: 0x54
		uint CltParam1; // type: DATA_DWORD_2 length: 0x0 offset: 0x58
		uint CltParam2; // type: DATA_DWORD_2 length: 0x0 offset: 0x5c
		uint CltParam3; // type: DATA_DWORD_2 length: 0x0 offset: 0x60
		uint CltParam4; // type: DATA_DWORD_2 length: 0x0 offset: 0x64
		uint CltParam5; // type: DATA_DWORD_2 length: 0x0 offset: 0x68
		uint cHitPar1; // type: DATA_DWORD_2 length: 0x0 offset: 0x6c
		uint cHitPar2; // type: DATA_DWORD_2 length: 0x0 offset: 0x70
		uint cHitPar3; // type: DATA_DWORD_2 length: 0x0 offset: 0x74
		uint dParam1; // type: DATA_DWORD_2 length: 0x0 offset: 0x78
		uint dParam2; // type: DATA_DWORD_2 length: 0x0 offset: 0x7c
		uint SrvCalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x80
		uint CltCalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x84
		uint SHitCalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x88
		uint CHitCalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x8c
		uint DmgCalc1; // type: CALC_TO_DWORD length: 0x0 offset: 0x90
		byte HitClass; // type: DATA_BYTE length: 0x0 offset: 0x94
		short Range; // type: DATA_WORD length: 0x0 offset: 0x96
		short LevRange; // type: DATA_WORD length: 0x0 offset: 0x98
		byte Vel; // type: DATA_BYTE length: 0x0 offset: 0x9a
		byte VelLev; // type: DATA_BYTE length: 0x0 offset: 0x9b
		byte MaxVel; // type: DATA_BYTE length: 0x0 offset: 0x9c
		short Accel; // type: DATA_WORD length: 0x0 offset: 0x9e
		short animrate; // type: DATA_WORD length: 0x0 offset: 0xa0
		short xoffset; // type: DATA_WORD length: 0x0 offset: 0xa2
		short yoffset; // type: DATA_WORD length: 0x0 offset: 0xa4
		short zoffset; // type: DATA_WORD length: 0x0 offset: 0xa6
		uint HitFlags; // type: DATA_DWORD_2 length: 0x0 offset: 0xa8
		short ResultFlags; // type: DATA_WORD length: 0x0 offset: 0xac
		byte KnockBack; // type: DATA_BYTE length: 0x0 offset: 0xae
		uint MinDamage; // type: DATA_DWORD_2 length: 0x0 offset: 0xb0
		uint MaxDamage; // type: DATA_DWORD_2 length: 0x0 offset: 0xb4
		uint MinLevDam1; // type: DATA_DWORD_2 length: 0x0 offset: 0xb8
		uint MinLevDam2; // type: DATA_DWORD_2 length: 0x0 offset: 0xbc
		uint MinLevDam3; // type: DATA_DWORD_2 length: 0x0 offset: 0xc0
		uint MinLevDam4; // type: DATA_DWORD_2 length: 0x0 offset: 0xc4
		uint MinLevDam5; // type: DATA_DWORD_2 length: 0x0 offset: 0xc8
		uint MaxLevDam1; // type: DATA_DWORD_2 length: 0x0 offset: 0xcc
		uint MaxLevDam2; // type: DATA_DWORD_2 length: 0x0 offset: 0xd0
		uint MaxLevDam3; // type: DATA_DWORD_2 length: 0x0 offset: 0xd4
		uint MaxLevDam4; // type: DATA_DWORD_2 length: 0x0 offset: 0xd8
		uint MaxLevDam5; // type: DATA_DWORD_2 length: 0x0 offset: 0xdc
		uint DmgSymPerCalc; // type: CALC_TO_DWORD length: 0x0 offset: 0xe0
		byte EType; // type: CODE_TO_BYTE length: 0x0 offset: 0xe4
		uint EMin; // type: DATA_DWORD_2 length: 0x0 offset: 0xe8
		uint EMax; // type: DATA_DWORD_2 length: 0x0 offset: 0xec
		uint MinELev1; // type: DATA_DWORD_2 length: 0x0 offset: 0xf0
		uint MinELev2; // type: DATA_DWORD_2 length: 0x0 offset: 0xf4
		uint MinELev3; // type: DATA_DWORD_2 length: 0x0 offset: 0xf8
		uint MinELev4; // type: DATA_DWORD_2 length: 0x0 offset: 0xfc
		uint MinELev5; // type: DATA_DWORD_2 length: 0x0 offset: 0x100
		uint MaxELev1; // type: DATA_DWORD_2 length: 0x0 offset: 0x104
		uint MaxELev2; // type: DATA_DWORD_2 length: 0x0 offset: 0x108
		uint MaxELev3; // type: DATA_DWORD_2 length: 0x0 offset: 0x10c
		uint MaxELev4; // type: DATA_DWORD_2 length: 0x0 offset: 0x110
		uint MaxELev5; // type: DATA_DWORD_2 length: 0x0 offset: 0x114
		uint EDmgSymPerCalc; // type: CALC_TO_DWORD length: 0x0 offset: 0x118
		uint ELen; // type: DATA_DWORD_2 length: 0x0 offset: 0x11c
		uint ELevLen1; // type: DATA_DWORD_2 length: 0x0 offset: 0x120
		uint ELevLen2; // type: DATA_DWORD_2 length: 0x0 offset: 0x124
		uint ELevLen3; // type: DATA_DWORD_2 length: 0x0 offset: 0x128
		byte CltSrcTown; // type: DATA_BYTE length: 0x0 offset: 0x12c
		byte SrcDamage; // type: DATA_BYTE length: 0x0 offset: 0x12d
		byte SrcMissDmg; // type: DATA_BYTE length: 0x0 offset: 0x12e
		byte Holy; // type: DATA_BYTE length: 0x0 offset: 0x12f
		byte Light; // type: DATA_BYTE length: 0x0 offset: 0x130
		byte Flicker; // type: DATA_BYTE length: 0x0 offset: 0x131
		byte Red; // type: DATA_BYTE length: 0x0 offset: 0x132
		byte Green; // type: DATA_BYTE length: 0x0 offset: 0x133
		byte Blue; // type: DATA_BYTE length: 0x0 offset: 0x134
		byte InitSteps; // type: DATA_BYTE length: 0x0 offset: 0x135
		byte Activate; // type: DATA_BYTE length: 0x0 offset: 0x136
		byte LoopAnim; // type: DATA_BYTE length: 0x0 offset: 0x137
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x40)]
		string CellFile; // type: DATA_ASCII length: 0x40 offset: 0x138
		byte AnimLen; // type: DATA_BYTE length: 0x0 offset: 0x178
		uint RandStart; // type: DATA_DWORD_2 length: 0x0 offset: 0x17c
		byte SubLoop; // type: DATA_BYTE length: 0x0 offset: 0x180
		byte SubStart; // type: DATA_BYTE length: 0x0 offset: 0x181
		byte SubStop; // type: DATA_BYTE length: 0x0 offset: 0x182
		byte CollideType; // type: DATA_BYTE length: 0x0 offset: 0x183
		byte Collision; // type: DATA_BYTE length: 0x0 offset: 0x184
		byte ClientCol; // type: DATA_BYTE length: 0x0 offset: 0x185
		byte CollideKill; // type: DATA_BYTE length: 0x0 offset: 0x186
		byte CollideFriend; // type: DATA_BYTE length: 0x0 offset: 0x187
		byte NextHit; // type: DATA_BYTE length: 0x0 offset: 0x188
		byte NextDelay; // type: DATA_BYTE length: 0x0 offset: 0x189
		byte Size; // type: DATA_BYTE length: 0x0 offset: 0x18a
		byte ToHit; // type: DATA_BYTE length: 0x0 offset: 0x18b
		byte AlwaysExplode; // type: DATA_BYTE length: 0x0 offset: 0x18c
		byte Trans; // type: DATA_BYTE length: 0x0 offset: 0x18d
		byte Qty; // type: DATA_BYTE length: 0x0 offset: 0x18e
		uint SpecialSetup; // type: DATA_DWORD_2 length: 0x0 offset: 0x190
		short Skill; // type: NAME_TO_WORD length: 0x0 offset: 0x194
		byte HitShift; // type: DATA_BYTE length: 0x0 offset: 0x196
		uint DamageRate; // type: DATA_DWORD_2 length: 0x0 offset: 0x19c
		byte NumDirections; // type: DATA_BYTE length: 0x0 offset: 0x1a0
		byte AnimSpeed; // type: DATA_BYTE length: 0x0 offset: 0x1a1
		byte LocalBlood; // type: DATA_BYTE length: 0x0 offset: 0x1a2
		byte end; // type: END length: 0x0 offset: 0x1a4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Monstats2Table
	{
		short Id; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
		Monstats2Flags1 flags1;
		/*uint noGfxHitTest:1; // type: DATA_BIT length: 0x0 offset: 0x4
		uint noMap:1; // type: DATA_BIT length: 0x1 offset: 0x4
		uint noOvly:1; // type: DATA_BIT length: 0x2 offset: 0x4
		uint isSel:1; // type: DATA_BIT length: 0x3 offset: 0x4
		uint alSel:1; // type: DATA_BIT length: 0x4 offset: 0x4
		uint noSel:1; // type: DATA_BIT length: 0x5 offset: 0x4
		uint shiftSel:1; // type: DATA_BIT length: 0x6 offset: 0x4
		uint corpseSel:1; // type: DATA_BIT length: 0x7 offset: 0x4
		uint revive:1; // type: DATA_BIT length: 0x8 offset: 0x4
		uint isAtt:1; // type: DATA_BIT length: 0x9 offset: 0x4
		uint small:1; // type: DATA_BIT length: 0xa offset: 0x4
		uint large:1; // type: DATA_BIT length: 0xb offset: 0x4
		uint soft:1; // type: DATA_BIT length: 0xc offset: 0x4
		uint critter:1; // type: DATA_BIT length: 0xd offset: 0x4
		uint shadow:1; // type: DATA_BIT length: 0xe offset: 0x4
		uint noUniqueShift:1; // type: DATA_BIT length: 0xf offset: 0x4
		uint compositeDeath:1; // type: DATA_BIT length: 0x10 offset: 0x4
		uint inert:1; // type: DATA_BIT length: 0x11 offset: 0x4
		uint objCol:1; // type: DATA_BIT length: 0x12 offset: 0x4
		uint deadCol:1; // type: DATA_BIT length: 0x13 offset: 0x4
		uint unflatDead:1; // type: DATA_BIT length: 0x14 offset: 0x4*/
		byte SizeX; // type: DATA_BYTE length: 0x0 offset: 0x8
		byte SizeY; // type: DATA_BYTE length: 0x0 offset: 0x9
		byte spawnCol; // type: DATA_BYTE length: 0x0 offset: 0xa
		byte Height; // type: DATA_BYTE length: 0x0 offset: 0xb
		byte overlayHeight; // type: DATA_BYTE length: 0x0 offset: 0xc
		byte pixHeight; // type: DATA_BYTE length: 0x0 offset: 0xd
		byte MeleeRng; // type: DATA_BYTE length: 0x0 offset: 0xe
		uint BaseW; // type: DATA_RAW length: 0x0 offset: 0x10
		byte HitClass; // type: DATA_BYTE length: 0x0 offset: 0x14
		Monstats2Flags2 flags2;
		/*uint HD:1; // type: DATA_BIT length: 0x0 offset: 0xe8
		uint TR:1; // type: DATA_BIT length: 0x1 offset: 0xe8
		uint LG:1; // type: DATA_BIT length: 0x2 offset: 0xe8
		uint RA:1; // type: DATA_BIT length: 0x3 offset: 0xe8
		uint LA:1; // type: DATA_BIT length: 0x4 offset: 0xe8
		uint RH:1; // type: DATA_BIT length: 0x5 offset: 0xe8
		uint LH:1; // type: DATA_BIT length: 0x6 offset: 0xe8
		uint SH:1; // type: DATA_BIT length: 0x7 offset: 0xe8
		uint S1:1; // type: DATA_BIT length: 0x8 offset: 0xe8
		uint S2:1; // type: DATA_BIT length: 0x9 offset: 0xe8
		uint S3:1; // type: DATA_BIT length: 0xa offset: 0xe8
		uint S4:1; // type: DATA_BIT length: 0xb offset: 0xe8
		uint S5:1; // type: DATA_BIT length: 0xc offset: 0xe8
		uint S6:1; // type: DATA_BIT length: 0xd offset: 0xe8
		uint S7:1; // type: DATA_BIT length: 0xe offset: 0xe8
		uint S8:1; // type: DATA_BIT length: 0xf offset: 0xe8*/
		byte TotalPieces; // type: DATA_BYTE length: 0x0 offset: 0xec
		Monstats2Flags3 flags3;
		/*uint mDT:1; // type: DATA_BIT length: 0x0 offset: 0xf0
		uint mNU:1; // type: DATA_BIT length: 0x1 offset: 0xf0
		uint mWL:1; // type: DATA_BIT length: 0x2 offset: 0xf0
		uint mGH:1; // type: DATA_BIT length: 0x3 offset: 0xf0
		uint mA1:1; // type: DATA_BIT length: 0x4 offset: 0xf0
		uint mA2:1; // type: DATA_BIT length: 0x5 offset: 0xf0
		uint mBL:1; // type: DATA_BIT length: 0x6 offset: 0xf0
		uint mSC:1; // type: DATA_BIT length: 0x7 offset: 0xf0
		uint mS1:1; // type: DATA_BIT length: 0x8 offset: 0xf0
		uint mS2:1; // type: DATA_BIT length: 0x9 offset: 0xf0
		uint mS3:1; // type: DATA_BIT length: 0xa offset: 0xf0
		uint mS4:1; // type: DATA_BIT length: 0xb offset: 0xf0
		uint mDD:1; // type: DATA_BIT length: 0xc offset: 0xf0
		uint mKB:1; // type: DATA_BIT length: 0xd offset: 0xf0
		uint mSQ:1; // type: DATA_BIT length: 0xe offset: 0xf0
		uint mRN:1; // type: DATA_BIT length: 0xf offset: 0xf0*/
		byte dDT; // type: DATA_BYTE length: 0x0 offset: 0xf4
		byte dNU; // type: DATA_BYTE length: 0x0 offset: 0xf5
		byte dWL; // type: DATA_BYTE length: 0x0 offset: 0xf6
		byte dGH; // type: DATA_BYTE length: 0x0 offset: 0xf7
		byte dA1; // type: DATA_BYTE length: 0x0 offset: 0xf8
		byte dA2; // type: DATA_BYTE length: 0x0 offset: 0xf9
		byte dBL; // type: DATA_BYTE length: 0x0 offset: 0xfa
		byte dSC; // type: DATA_BYTE length: 0x0 offset: 0xfb
		byte dS1; // type: DATA_BYTE length: 0x0 offset: 0xfc
		byte dS2; // type: DATA_BYTE length: 0x0 offset: 0xfd
		byte dS3; // type: DATA_BYTE length: 0x0 offset: 0xfe
		byte dS4; // type: DATA_BYTE length: 0x0 offset: 0xff
		byte dDD; // type: DATA_BYTE length: 0x0 offset: 0x100
		byte dKB; // type: DATA_BYTE length: 0x0 offset: 0x101
		byte dSQ; // type: DATA_BYTE length: 0x0 offset: 0x102
		byte dRN; // type: DATA_BYTE length: 0x0 offset: 0x103
		Monstats2Flags4 flags4;
		/*uint A1mv:1; // type: DATA_BIT length: 0x4 offset: 0x104
		uint A2mv:1; // type: DATA_BIT length: 0x5 offset: 0x104
		uint SCmv:1; // type: DATA_BIT length: 0x7 offset: 0x104
		uint S1mv:1; // type: DATA_BIT length: 0x8 offset: 0x104
		uint S2mv:1; // type: DATA_BIT length: 0x9 offset: 0x104
		uint S3mv:1; // type: DATA_BIT length: 0xa offset: 0x104
		uint S4mv:1; // type: DATA_BIT length: 0xb offset: 0x104*/
		byte InfernoLen; // type: DATA_BYTE length: 0x0 offset: 0x108
		byte InfernoAnim; // type: DATA_BYTE length: 0x0 offset: 0x109
		byte InfernoRollback; // type: DATA_BYTE length: 0x0 offset: 0x10a
		byte ResurrectMode; // type: CODE_TO_BYTE length: 0x0 offset: 0x10b
		short ResurrectSkill; // type: NAME_TO_WORD length: 0x0 offset: 0x10c
		short htTop; // type: DATA_WORD length: 0x0 offset: 0x10e
		short htLeft; // type: DATA_WORD length: 0x0 offset: 0x110
		short htWidth; // type: DATA_WORD length: 0x0 offset: 0x112
		short htHeight; // type: DATA_WORD length: 0x0 offset: 0x114
		uint automapCel; // type: DATA_DWORD length: 0x0 offset: 0x118
		byte localBlood; // type: DATA_BYTE length: 0x0 offset: 0x11c
		byte bleed; // type: DATA_BYTE length: 0x0 offset: 0x11d
		byte light; // type: DATA_BYTE length: 0x0 offset: 0x11e
		byte lightR; // type: DATA_BYTE length: 0x0 offset: 0x11f
		byte lightG; // type: DATA_BYTE length: 0x0 offset: 0x120
		byte lightB; // type: DATA_BYTE length: 0x0 offset: 0x121
		byte Utrans; // type: DATA_BYTE length: 0x0 offset: 0x122
		byte UtransN; // type: DATA_BYTE length: 0x0 offset: 0x123
		byte UtransH; // type: DATA_BYTE length: 0x0 offset: 0x124
		uint Heart; // type: DATA_RAW length: 0x0 offset: 0x128
		uint BodyPart; // type: DATA_RAW length: 0x0 offset: 0x12c
		byte restore; // type: DATA_BYTE length: 0x0 offset: 0x130
		byte end; // type: END length: 0x0 offset: 0x134
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelsTable
	{
		byte Id; // type: DATA_BYTE length: 0x0 offset: 0x0
		byte Pal; // type: DATA_BYTE length: 0x0 offset: 0x2
		byte Act; // type: DATA_BYTE length: 0x0 offset: 0x3
		byte Teleport; // type: DATA_BYTE length: 0x0 offset: 0x4
		byte Rain; // type: DATA_BYTE length: 0x0 offset: 0x5
		byte Mud; // type: DATA_BYTE length: 0x0 offset: 0x6
		byte NoPer; // type: DATA_BYTE length: 0x0 offset: 0x7
		byte IsInside; // type: DATA_BYTE length: 0x0 offset: 0x8
		byte DrawEdges; // type: DATA_BYTE length: 0x0 offset: 0x9
		uint WarpDist; // type: DATA_DWORD length: 0x0 offset: 0xc
		short MonLvl1; // type: DATA_WORD length: 0x0 offset: 0x10
		short MonLvl2; // type: DATA_WORD length: 0x0 offset: 0x12
		short MonLvl3; // type: DATA_WORD length: 0x0 offset: 0x14
		short MonLvl1Ex; // type: DATA_WORD length: 0x0 offset: 0x16
		short MonLvl2Ex; // type: DATA_WORD length: 0x0 offset: 0x18
		short MonLvl3Ex; // type: DATA_WORD length: 0x0 offset: 0x1a
		uint MonDen; // type: DATA_DWORD length: 0x0 offset: 0x1c
		uint MonDenN; // type: DATA_DWORD length: 0x0 offset: 0x20
		uint MonDenH; // type: DATA_DWORD length: 0x0 offset: 0x24
		byte MonUMin; // type: DATA_BYTE length: 0x0 offset: 0x28
		byte MonUMinN; // type: DATA_BYTE length: 0x0 offset: 0x29
		byte MonUMinH; // type: DATA_BYTE length: 0x0 offset: 0x2a
		byte MonUMax; // type: DATA_BYTE length: 0x0 offset: 0x2b
		byte MonUMaxN; // type: DATA_BYTE length: 0x0 offset: 0x2c
		byte MonUMaxH; // type: DATA_BYTE length: 0x0 offset: 0x2d
		byte MonWndr; // type: DATA_BYTE length: 0x0 offset: 0x2e
		byte MonSpcWalk; // type: DATA_BYTE length: 0x0 offset: 0x2f
		byte Quest; // type: DATA_BYTE length: 0x0 offset: 0x30
		byte rangedspawn; // type: DATA_BYTE length: 0x0 offset: 0x31
		byte NumMon; // type: DATA_BYTE length: 0x0 offset: 0x32
		short mon1; // type: NAME_TO_WORD length: 0x0 offset: 0x36
		short mon2; // type: NAME_TO_WORD length: 0x0 offset: 0x38
		short mon3; // type: NAME_TO_WORD length: 0x0 offset: 0x3a
		short mon4; // type: NAME_TO_WORD length: 0x0 offset: 0x3c
		short mon5; // type: NAME_TO_WORD length: 0x0 offset: 0x3e
		short mon6; // type: NAME_TO_WORD length: 0x0 offset: 0x40
		short mon7; // type: NAME_TO_WORD length: 0x0 offset: 0x42
		short mon8; // type: NAME_TO_WORD length: 0x0 offset: 0x44
		short mon9; // type: NAME_TO_WORD length: 0x0 offset: 0x46
		short mon10; // type: NAME_TO_WORD length: 0x0 offset: 0x48
		short mon11; // type: NAME_TO_WORD length: 0x0 offset: 0x4a
		short mon12; // type: NAME_TO_WORD length: 0x0 offset: 0x4c
		short mon13; // type: NAME_TO_WORD length: 0x0 offset: 0x4e
		short mon14; // type: NAME_TO_WORD length: 0x0 offset: 0x50
		short mon15; // type: NAME_TO_WORD length: 0x0 offset: 0x52
		short mon16; // type: NAME_TO_WORD length: 0x0 offset: 0x54
		short mon17; // type: NAME_TO_WORD length: 0x0 offset: 0x56
		short mon18; // type: NAME_TO_WORD length: 0x0 offset: 0x58
		short mon19; // type: NAME_TO_WORD length: 0x0 offset: 0x5a
		short mon20; // type: NAME_TO_WORD length: 0x0 offset: 0x5c
		short mon21; // type: NAME_TO_WORD length: 0x0 offset: 0x5e
		short mon22; // type: NAME_TO_WORD length: 0x0 offset: 0x60
		short mon23; // type: NAME_TO_WORD length: 0x0 offset: 0x62
		short mon24; // type: NAME_TO_WORD length: 0x0 offset: 0x64
		short mon25; // type: NAME_TO_WORD length: 0x0 offset: 0x66
		short nmon1; // type: NAME_TO_WORD length: 0x0 offset: 0x68
		short nmon2; // type: NAME_TO_WORD length: 0x0 offset: 0x6a
		short nmon3; // type: NAME_TO_WORD length: 0x0 offset: 0x6c
		short nmon4; // type: NAME_TO_WORD length: 0x0 offset: 0x6e
		short nmon5; // type: NAME_TO_WORD length: 0x0 offset: 0x70
		short nmon6; // type: NAME_TO_WORD length: 0x0 offset: 0x72
		short nmon7; // type: NAME_TO_WORD length: 0x0 offset: 0x74
		short nmon8; // type: NAME_TO_WORD length: 0x0 offset: 0x76
		short nmon9; // type: NAME_TO_WORD length: 0x0 offset: 0x78
		short nmon10; // type: NAME_TO_WORD length: 0x0 offset: 0x7a
		short nmon11; // type: NAME_TO_WORD length: 0x0 offset: 0x7c
		short nmon12; // type: NAME_TO_WORD length: 0x0 offset: 0x7e
		short nmon13; // type: NAME_TO_WORD length: 0x0 offset: 0x80
		short nmon14; // type: NAME_TO_WORD length: 0x0 offset: 0x82
		short nmon15; // type: NAME_TO_WORD length: 0x0 offset: 0x84
		short nmon16; // type: NAME_TO_WORD length: 0x0 offset: 0x86
		short nmon17; // type: NAME_TO_WORD length: 0x0 offset: 0x88
		short nmon18; // type: NAME_TO_WORD length: 0x0 offset: 0x8a
		short nmon19; // type: NAME_TO_WORD length: 0x0 offset: 0x8c
		short nmon20; // type: NAME_TO_WORD length: 0x0 offset: 0x8e
		short nmon21; // type: NAME_TO_WORD length: 0x0 offset: 0x90
		short nmon22; // type: NAME_TO_WORD length: 0x0 offset: 0x92
		short nmon23; // type: NAME_TO_WORD length: 0x0 offset: 0x94
		short nmon24; // type: NAME_TO_WORD length: 0x0 offset: 0x96
		short nmon25; // type: NAME_TO_WORD length: 0x0 offset: 0x98
		short umon1; // type: NAME_TO_WORD length: 0x0 offset: 0x9a
		short umon2; // type: NAME_TO_WORD length: 0x0 offset: 0x9c
		short umon3; // type: NAME_TO_WORD length: 0x0 offset: 0x9e
		short umon4; // type: NAME_TO_WORD length: 0x0 offset: 0xa0
		short umon5; // type: NAME_TO_WORD length: 0x0 offset: 0xa2
		short umon6; // type: NAME_TO_WORD length: 0x0 offset: 0xa4
		short umon7; // type: NAME_TO_WORD length: 0x0 offset: 0xa6
		short umon8; // type: NAME_TO_WORD length: 0x0 offset: 0xa8
		short umon9; // type: NAME_TO_WORD length: 0x0 offset: 0xaa
		short umon10; // type: NAME_TO_WORD length: 0x0 offset: 0xac
		short umon11; // type: NAME_TO_WORD length: 0x0 offset: 0xae
		short umon12; // type: NAME_TO_WORD length: 0x0 offset: 0xb0
		short umon13; // type: NAME_TO_WORD length: 0x0 offset: 0xb2
		short umon14; // type: NAME_TO_WORD length: 0x0 offset: 0xb4
		short umon15; // type: NAME_TO_WORD length: 0x0 offset: 0xb6
		short umon16; // type: NAME_TO_WORD length: 0x0 offset: 0xb8
		short umon17; // type: NAME_TO_WORD length: 0x0 offset: 0xba
		short umon18; // type: NAME_TO_WORD length: 0x0 offset: 0xbc
		short umon19; // type: NAME_TO_WORD length: 0x0 offset: 0xbe
		short umon20; // type: NAME_TO_WORD length: 0x0 offset: 0xc0
		short umon21; // type: NAME_TO_WORD length: 0x0 offset: 0xc2
		short umon22; // type: NAME_TO_WORD length: 0x0 offset: 0xc4
		short umon23; // type: NAME_TO_WORD length: 0x0 offset: 0xc6
		short umon24; // type: NAME_TO_WORD length: 0x0 offset: 0xc8
		short umon25; // type: NAME_TO_WORD length: 0x0 offset: 0xca
		short cmon1; // type: NAME_TO_WORD length: 0x0 offset: 0xcc
		short cmon2; // type: NAME_TO_WORD length: 0x0 offset: 0xce
		short cmon3; // type: NAME_TO_WORD length: 0x0 offset: 0xd0
		short cmon4; // type: NAME_TO_WORD length: 0x0 offset: 0xd2
		short cpct1; // type: DATA_WORD length: 0x0 offset: 0xd4
		short cpct2; // type: DATA_WORD length: 0x0 offset: 0xd6
		short cpct3; // type: DATA_WORD length: 0x0 offset: 0xd8
		short cpct4; // type: DATA_WORD length: 0x0 offset: 0xda
		short camt1; // type: DATA_WORD length: 0x0 offset: 0xdc
		short camt2; // type: DATA_WORD length: 0x0 offset: 0xdc
		short camt3; // type: DATA_WORD length: 0x0 offset: 0xdc
		short camt4; // type: DATA_WORD length: 0x0 offset: 0xdc
		byte Waypoint; // type: DATA_BYTE length: 0x0 offset: 0xe4
		byte ObjGrp0; // type: DATA_BYTE length: 0x0 offset: 0xe5
		byte ObjGrp1; // type: DATA_BYTE length: 0x0 offset: 0xe6
		byte ObjGrp2; // type: DATA_BYTE length: 0x0 offset: 0xe7
		byte ObjGrp3; // type: DATA_BYTE length: 0x0 offset: 0xe8
		byte ObjGrp4; // type: DATA_BYTE length: 0x0 offset: 0xe9
		byte ObjGrp5; // type: DATA_BYTE length: 0x0 offset: 0xea
		byte ObjGrp6; // type: DATA_BYTE length: 0x0 offset: 0xeb
		byte ObjGrp7; // type: DATA_BYTE length: 0x0 offset: 0xec
		byte ObjPrb0; // type: DATA_BYTE length: 0x0 offset: 0xed
		byte ObjPrb1; // type: DATA_BYTE length: 0x0 offset: 0xee
		byte ObjPrb2; // type: DATA_BYTE length: 0x0 offset: 0xef
		byte ObjPrb3; // type: DATA_BYTE length: 0x0 offset: 0xf0
		byte ObjPrb4; // type: DATA_BYTE length: 0x0 offset: 0xf1
		byte ObjPrb5; // type: DATA_BYTE length: 0x0 offset: 0xf2
		byte ObjPrb6; // type: DATA_BYTE length: 0x0 offset: 0xf3
		byte ObjPrb7; // type: DATA_BYTE length: 0x0 offset: 0xf4
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x27)]
		string LevelName; // type: DATA_ASCII length: 0x27 offset: 0xf5
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x27)]
		string LevelWarp; // type: DATA_ASCII length: 0x27 offset: 0x11d
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x27)]
		string EntryFile; // type: DATA_ASCII length: 0x27 offset: 0x145
		uint Themes; // type: DATA_DWORD length: 0x0 offset: 0x210
		uint FloorFilter; // type: DATA_DWORD length: 0x0 offset: 0x214
		uint BlankScreen; // type: DATA_DWORD length: 0x0 offset: 0x218
		byte SoundEnv; // type: DATA_BYTE length: 0x0 offset: 0x21c
		byte end; // type: END length: 0x0 offset: 0x220
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelDefsTable
	{
		uint QuestFlag; // type: DATA_DWORD_2 length: 0x0 offset: 0x0
		uint QuestFlagEx; // type: DATA_DWORD_2 length: 0x0 offset: 0x4
		uint Layer; // type: DATA_DWORD_2 length: 0x0 offset: 0x8
		uint SizeX; // type: DATA_DWORD_2 length: 0x0 offset: 0xc
		uint SizeXN; // type: DATA_DWORD_2 length: 0x0 offset: 0x10
		uint SizeXH; // type: DATA_DWORD_2 length: 0x0 offset: 0x14
		uint SizeY; // type: DATA_DWORD_2 length: 0x0 offset: 0x18
		uint SizeYN; // type: DATA_DWORD_2 length: 0x0 offset: 0x1c
		uint SizeYH; // type: DATA_DWORD_2 length: 0x0 offset: 0x20
		uint OffsetX; // type: DATA_DWORD_2 length: 0x0 offset: 0x24
		uint OffsetY; // type: DATA_DWORD_2 length: 0x0 offset: 0x28
		uint Depend; // type: DATA_DWORD_2 length: 0x0 offset: 0x2c
		uint DrlgType; // type: DATA_DWORD_2 length: 0x0 offset: 0x30
		uint LevelType; // type: DATA_DWORD_2 length: 0x0 offset: 0x34
		uint SubType; // type: DATA_DWORD_2 length: 0x0 offset: 0x38
		uint SubTheme; // type: DATA_DWORD_2 length: 0x0 offset: 0x3c
		uint SubWaypoint; // type: DATA_DWORD_2 length: 0x0 offset: 0x40
		uint SubShrine; // type: DATA_DWORD_2 length: 0x0 offset: 0x44
		uint Vis0; // type: DATA_DWORD_2 length: 0x0 offset: 0x48
		uint Vis1; // type: DATA_DWORD_2 length: 0x0 offset: 0x4c
		uint Vis2; // type: DATA_DWORD_2 length: 0x0 offset: 0x50
		uint Vis3; // type: DATA_DWORD_2 length: 0x0 offset: 0x54
		uint Vis4; // type: DATA_DWORD_2 length: 0x0 offset: 0x58
		uint Vis5; // type: DATA_DWORD_2 length: 0x0 offset: 0x5c
		uint Vis6; // type: DATA_DWORD_2 length: 0x0 offset: 0x60
		uint Vis7; // type: DATA_DWORD_2 length: 0x0 offset: 0x64
		uint Warp0; // type: DATA_DWORD_2 length: 0x0 offset: 0x68
		uint Warp1; // type: DATA_DWORD_2 length: 0x0 offset: 0x6c
		uint Warp2; // type: DATA_DWORD_2 length: 0x0 offset: 0x70
		uint Warp3; // type: DATA_DWORD_2 length: 0x0 offset: 0x74
		uint Warp4; // type: DATA_DWORD_2 length: 0x0 offset: 0x78
		uint Warp5; // type: DATA_DWORD_2 length: 0x0 offset: 0x7c
		uint Warp6; // type: DATA_DWORD_2 length: 0x0 offset: 0x80
		uint Warp7; // type: DATA_DWORD_2 length: 0x0 offset: 0x84
		byte Intensity; // type: DATA_BYTE length: 0x0 offset: 0x88// WRONG
		byte Red; // type: DATA_BYTE length: 0x0 offset: 0x89
		byte Green; // type: DATA_BYTE length: 0x0 offset: 0x8a
		byte Blue; // type: DATA_BYTE length: 0x0 offset: 0x8b
		uint Portal; // type: DATA_DWORD_2 length: 0x0 offset: 0x8c
		uint Position; // type: DATA_DWORD_2 length: 0x0 offset: 0x90
		uint SaveMonsters; // type: DATA_DWORD_2 length: 0x0 offset: 0x94
		uint LOSDraw; // type: DATA_DWORD_2 length: 0x0 offset: 0x98
		byte end; // type: END length: 0x0 offset: 0x9c
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelMazeTable
	{
		uint Level; // type: DATA_DWORD length: 0x0 offset: 0x0
		uint Rooms; // type: DATA_DWORD length: 0x0 offset: 0x4
		uint RoomsN; // type: DATA_DWORD length: 0x0 offset: 0x8
		uint RoomsH; // type: DATA_DWORD length: 0x0 offset: 0xc
		uint SizeX; // type: DATA_DWORD length: 0x0 offset: 0x10
		uint SizeY; // type: DATA_DWORD length: 0x0 offset: 0x14
		uint Merge; // type: DATA_DWORD length: 0x0 offset: 0x18
		byte end; // type: END length: 0x0 offset: 0x1c
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelSubTable
	{
		uint Type; // type: DATA_DWORD length: 0x0 offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File; // type: DATA_ASCII length: 0x3b offset: 0x4
		uint CheckAll; // type: DATA_DWORD_2 length: 0x0 offset: 0x40
		uint BordType; // type: DATA_DWORD_2 length: 0x0 offset: 0x44
		uint Dt1Mask; // type: DATA_DWORD length: 0x0 offset: 0x48
		uint GridSize; // type: DATA_DWORD length: 0x0 offset: 0x4c
		uint Prob0; // type: DATA_DWORD length: 0x0 offset: 0x11c
		uint Prob1; // type: DATA_DWORD length: 0x0 offset: 0x120
		uint Prob2; // type: DATA_DWORD length: 0x0 offset: 0x124
		uint Prob3; // type: DATA_DWORD length: 0x0 offset: 0x128
		uint Prob4; // type: DATA_DWORD length: 0x0 offset: 0x12c
		uint Trials0; // type: DATA_DWORD_2 length: 0x0 offset: 0x130
		uint Trials1; // type: DATA_DWORD_2 length: 0x0 offset: 0x134
		uint Trials2; // type: DATA_DWORD_2 length: 0x0 offset: 0x138
		uint Trials3; // type: DATA_DWORD_2 length: 0x0 offset: 0x13c
		uint Trials4; // type: DATA_DWORD_2 length: 0x0 offset: 0x140
		uint Max0; // type: DATA_DWORD length: 0x0 offset: 0x144
		uint Max1; // type: DATA_DWORD length: 0x0 offset: 0x148
		uint Max2; // type: DATA_DWORD length: 0x0 offset: 0x14c
		uint Max3; // type: DATA_DWORD length: 0x0 offset: 0x150
		uint Max4; // type: DATA_DWORD length: 0x0 offset: 0x154
		uint Expansion; // type: DATA_DWORD length: 0x0 offset: 0x158
		byte end; // type: END length: 0x0 offset: 0x15c
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelWarpTable
	{
		uint Id; // type: DATA_DWORD_2 length: 0x0 offset: 0x0
		uint SelectX; // type: DATA_DWORD_2 length: 0x0 offset: 0x4
		uint SelectY; // type: DATA_DWORD_2 length: 0x0 offset: 0x8
		uint SelectDX; // type: DATA_DWORD_2 length: 0x0 offset: 0xc
		uint SelectDY; // type: DATA_DWORD_2 length: 0x0 offset: 0x10
		uint ExitWalkX; // type: DATA_DWORD_2 length: 0x0 offset: 0x14
		uint ExitWalkY; // type: DATA_DWORD_2 length: 0x0 offset: 0x18
		uint OffsetX; // type: DATA_DWORD_2 length: 0x0 offset: 0x1c
		uint OffsetY; // type: DATA_DWORD_2 length: 0x0 offset: 0x20
		uint LitVersion; // type: DATA_DWORD_2 length: 0x0 offset: 0x24
		uint Tiles; // type: DATA_DWORD_2 length: 0x0 offset: 0x28
		char Direction; // type: DATA_ASCII length: 0x1 offset: 0x2c
		byte end; // type: END length: 0x0 offset: 0x30
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelPrestTable
	{
		uint Def; // type: DATA_DWORD length: 0x0 offset: 0x0
		uint LevelId; // type: DATA_DWORD length: 0x0 offset: 0x4
		uint Populate; // type: DATA_DWORD length: 0x0 offset: 0x8
		uint Logicals; // type: DATA_DWORD length: 0x0 offset: 0xc
		uint Outdoors; // type: DATA_DWORD length: 0x0 offset: 0x10
		uint Animate; // type: DATA_DWORD length: 0x0 offset: 0x14
		uint KillEdge; // type: DATA_DWORD length: 0x0 offset: 0x18
		uint FillBlanks; // type: DATA_DWORD length: 0x0 offset: 0x1c
		uint Expansion; // type: DATA_DWORD length: 0x0 offset: 0x20
		uint SizeX; // type: DATA_DWORD length: 0x0 offset: 0x28
		uint SizeY; // type: DATA_DWORD length: 0x0 offset: 0x2c
		uint AutoMap; // type: DATA_DWORD length: 0x0 offset: 0x30
		uint Scan; // type: DATA_DWORD length: 0x0 offset: 0x34
		uint Pops; // type: DATA_DWORD_2 length: 0x0 offset: 0x38
		uint PopPad; // type: DATA_DWORD_2 length: 0x0 offset: 0x3c
		uint Files; // type: DATA_DWORD length: 0x0 offset: 0x40
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File1; // type: DATA_ASCII length: 0x3b offset: 0x44
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File2; // type: DATA_ASCII length: 0x3b offset: 0x80
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File3; // type: DATA_ASCII length: 0x3b offset: 0xbc
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File4; // type: DATA_ASCII length: 0x3b offset: 0xf8
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File5; // type: DATA_ASCII length: 0x3b offset: 0x134
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File6; // type: DATA_ASCII length: 0x3b offset: 0x170
		uint Dt1Mask; // type: DATA_DWORD_2 length: 0x0 offset: 0x1ac
		byte end; // type: END length: 0x0 offset: 0x1b0
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LevelTypesTable
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File1; // type: DATA_ASCII length: 0x3b offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File2; // type: DATA_ASCII length: 0x3b offset: 0x3c
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File3; // type: DATA_ASCII length: 0x3b offset: 0x78
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File4; // type: DATA_ASCII length: 0x3b offset: 0xb4
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File5; // type: DATA_ASCII length: 0x3b offset: 0xf0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File6; // type: DATA_ASCII length: 0x3b offset: 0x12c
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File7; // type: DATA_ASCII length: 0x3b offset: 0x168
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File8; // type: DATA_ASCII length: 0x3b offset: 0x1a4
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File9; // type: DATA_ASCII length: 0x3b offset: 0x1e0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File10; // type: DATA_ASCII length: 0x3b offset: 0x21c
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File11; // type: DATA_ASCII length: 0x3b offset: 0x258
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File12; // type: DATA_ASCII length: 0x3b offset: 0x294
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File13; // type: DATA_ASCII length: 0x3b offset: 0x2d0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File14; // type: DATA_ASCII length: 0x3b offset: 0x30c
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File15; // type: DATA_ASCII length: 0x3b offset: 0x348
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File16; // type: DATA_ASCII length: 0x3b offset: 0x384
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File17; // type: DATA_ASCII length: 0x3b offset: 0x3c0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File18; // type: DATA_ASCII length: 0x3b offset: 0x3fc
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File19; // type: DATA_ASCII length: 0x3b offset: 0x438
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File20; // type: DATA_ASCII length: 0x3b offset: 0x474
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File21; // type: DATA_ASCII length: 0x3b offset: 0x4b0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File22; // type: DATA_ASCII length: 0x3b offset: 0x4ec
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File23; // type: DATA_ASCII length: 0x3b offset: 0x528
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File24; // type: DATA_ASCII length: 0x3b offset: 0x564
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File25; // type: DATA_ASCII length: 0x3b offset: 0x5a0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File26; // type: DATA_ASCII length: 0x3b offset: 0x5dc
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File27; // type: DATA_ASCII length: 0x3b offset: 0x618
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File28; // type: DATA_ASCII length: 0x3b offset: 0x654
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File29; // type: DATA_ASCII length: 0x3b offset: 0x690
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File30; // type: DATA_ASCII length: 0x3b offset: 0x6cc
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File31; // type: DATA_ASCII length: 0x3b offset: 0x708
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3b)]
		string File32; // type: DATA_ASCII length: 0x3b offset: 0x744
		byte Act; // type: DATA_BYTE length: 0x0 offset: 0x780
		uint Expansion; // type: DATA_DWORD length: 0x0 offset: 0x784
		byte end; // type: END length: 0x0 offset: 0x788
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CharStatsTable
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		string wclass; // type: DATA_ASCII length: 0x20 offset: 0x00
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x0f)]
		string Class; // type: DATA_ASCII length: 0xf offset: 0x20
		byte str; // type: DATA_BYTE length: 0x0 offset: 0x30
		byte dex; // type: DATA_BYTE length: 0x0 offset: 0x31
		byte intel; // type: DATA_BYTE length: 0x0 offset: 0x32
		byte vit; // type: DATA_BYTE length: 0x0 offset: 0x33
		byte stamina; // type: DATA_BYTE length: 0x0 offset: 0x34
		byte hpadd; // type: DATA_BYTE length: 0x0 offset: 0x35
		byte PercentStr; // type: DATA_BYTE length: 0x0 offset: 0x36
		byte PercentInt; // type: DATA_BYTE length: 0x0 offset: 0x37
		byte PercentDex; // type: DATA_BYTE length: 0x0 offset: 0x38
		byte PercentVit; // type: DATA_BYTE length: 0x0 offset: 0x39
		byte ManaRegen; // type: DATA_BYTE length: 0x0 offset: 0x3a
		uint ToHitFactor; // type: DATA_DWORD_2 length: 0x0 offset: 0x3c
		byte WalkVelocity; // type: DATA_BYTE length: 0x0 offset: 0x40
		byte RunVelocity; // type: DATA_BYTE length: 0x0 offset: 0x41
		byte RunDrain; // type: DATA_BYTE length: 0x0 offset: 0x42
		byte LifePerLevel; // type: DATA_BYTE length: 0x0 offset: 0x43
		byte StaminaPerLevel; // type: DATA_BYTE length: 0x0 offset: 0x44
		byte ManaPerLevel; // type: DATA_BYTE length: 0x0 offset: 0x45
		byte LifePerVitality; // type: DATA_BYTE length: 0x0 offset: 0x46
		byte StaminaPerVitality; // type: DATA_BYTE length: 0x0 offset: 0x47
		byte ManaPerMagic; // type: DATA_BYTE length: 0x0 offset: 0x48
		byte BlockFactor; // type: DATA_BYTE length: 0x0 offset: 0x49
		uint basewclass; // type: DATA_RAW length: 0x0 offset: 0x4c
		byte StatPerLevel; // type: DATA_BYTE length: 0x0 offset: 0x50
		short StrAllSkills; // type: KEY_TO_WORD length: 0x0 offset: 0x52
		short StrSkillTab1; // type: KEY_TO_WORD length: 0x0 offset: 0x54
		short StrSkillTab2; // type: KEY_TO_WORD length: 0x0 offset: 0x56
		short StrSkillTab3; // type: KEY_TO_WORD length: 0x0 offset: 0x58
		short StrClassOnly; // type: KEY_TO_WORD length: 0x0 offset: 0x5a
		uint item1; // type: DATA_RAW length: 0x0 offset: 0x5c
		byte item1loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x60
		byte item1count; // type: DATA_BYTE length: 0x0 offset: 0x61
		uint item2; // type: DATA_RAW length: 0x0 offset: 0x64
		byte item2loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x68
		byte item2count; // type: DATA_BYTE length: 0x0 offset: 0x69
		uint item3; // type: DATA_RAW length: 0x0 offset: 0x6c
		byte item3loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x70
		byte item3count; // type: DATA_BYTE length: 0x0 offset: 0x71
		uint item4; // type: DATA_RAW length: 0x0 offset: 0x74
		byte item4loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x78
		byte item4count; // type: DATA_BYTE length: 0x0 offset: 0x79
		uint item5; // type: DATA_RAW length: 0x0 offset: 0x7c
		byte item5loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x80
		byte item5count; // type: DATA_BYTE length: 0x0 offset: 0x81
		uint item6; // type: DATA_RAW length: 0x0 offset: 0x84
		byte item6loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x88
		byte item6count; // type: DATA_BYTE length: 0x0 offset: 0x89
		uint item7; // type: DATA_RAW length: 0x0 offset: 0x8c
		byte item7loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x90
		byte item7count; // type: DATA_BYTE length: 0x0 offset: 0x91
		uint item8; // type: DATA_RAW length: 0x0 offset: 0x94
		byte item8loc; // type: CODE_TO_BYTE length: 0x0 offset: 0x98
		byte item8count; // type: DATA_BYTE length: 0x0 offset: 0x99
		uint item9; // type: DATA_RAW length: 0x0 offset: 0x9c
		byte item9loc; // type: CODE_TO_BYTE length: 0x0 offset: 0xa0
		byte item9count; // type: DATA_BYTE length: 0x0 offset: 0xa1
		uint item10; // type: DATA_RAW length: 0x0 offset: 0xa4
		byte item10loc; // type: CODE_TO_BYTE length: 0x0 offset: 0xa8
		byte item10count; // type: DATA_BYTE length: 0x0 offset: 0xa9
		short StartSkill; // type: NAME_TO_WORD length: 0x0 offset: 0xac
		short Skill1; // type: NAME_TO_WORD length: 0x0 offset: 0xae
		short Skill2; // type: NAME_TO_WORD length: 0x0 offset: 0xb0
		short Skill3; // type: NAME_TO_WORD length: 0x0 offset: 0xb2
		short Skill4; // type: NAME_TO_WORD length: 0x0 offset: 0xb4
		short Skill5; // type: NAME_TO_WORD length: 0x0 offset: 0xb6
		short Skill6; // type: NAME_TO_WORD length: 0x0 offset: 0xb8
		short Skill7; // type: NAME_TO_WORD length: 0x0 offset: 0xba
		short Skill8; // type: NAME_TO_WORD length: 0x0 offset: 0xbc
		short Skill9; // type: NAME_TO_WORD length: 0x0 offset: 0xbe
		short Skill10; // type: NAME_TO_WORD length: 0x0 offset: 0xc0
		byte end; // type: END length: 0x0 offset: 0xc4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SetItemsTable
	{
		short index; // type: CODE_TO_WORD length: 0x3 offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string name; // type: DATA_ASCII length: 0x1F offset: 0x2
		short version; // type: DATA_WORD length: 0x0 offset: 0x22
		short namestr; // type: DATA_WORD length: 0x0 offset: 0x24
		uint item; // type: DATA_RAW length: 0x0 offset: 0x28
		short set; // type: NAME_TO_WORD length: 0x0 offset: 0x2c
		short lvl; // type: DATA_WORD length: 0x0 offset: 0x30
		short lvlReq; // type: DATA_WORD length: 0x0 offset: 0x32
		uint rarity; // type: DATA_DWORD length: 0x0 offset: 0x34
		uint costMult; // type: DATA_DWORD_2 length: 0x0 offset: 0x38
		uint costAdd; // type: DATA_DWORD_2 length: 0x0 offset: 0x3c
		byte chrtransform; // type: CODE_TO_BYTE length: 0x0 offset: 0x40
		byte invtransform; // type: CODE_TO_BYTE length: 0x0 offset: 0x41
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string flippyfile; // type: DATA_ASCII length: 0x1f offset: 0x42
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invfile; // type: DATA_ASCII length: 0x1f offset: 0x62
		short dropsound; // type: NAME_TO_WORD length: 0x0 offset: 0x82
		short usesound; // type: NAME_TO_WORD length: 0x0 offset: 0x84
		byte dropsfxframe; // type: DATA_BYTE length: 0x0 offset: 0x86
		byte addFunc; // type: DATA_BYTE length: 0x0 offset: 0x87
		uint prop1; // type: NAME_TO_DWORD length: 0x0 offset: 0x88
		uint par1; // type: CALC_TO_DWORD length: 0x0 offset: 0x8c
		uint min1; // type: DATA_DWORD_2 length: 0x0 offset: 0x90
		uint max1; // type: DATA_DWORD_2 length: 0x0 offset: 0x94
		uint prop2; // type: NAME_TO_DWORD length: 0x0 offset: 0x98
		uint par2; // type: CALC_TO_DWORD length: 0x0 offset: 0x9c
		uint min2; // type: DATA_DWORD_2 length: 0x0 offset: 0xa0
		uint max2; // type: DATA_DWORD_2 length: 0x0 offset: 0xa4
		uint prop3; // type: NAME_TO_DWORD length: 0x0 offset: 0xa8
		uint par3; // type: CALC_TO_DWORD length: 0x0 offset: 0xac
		uint min3; // type: DATA_DWORD_2 length: 0x0 offset: 0xb0
		uint max3; // type: DATA_DWORD_2 length: 0x0 offset: 0xb4
		uint prop4; // type: NAME_TO_DWORD length: 0x0 offset: 0xb8
		uint par4; // type: CALC_TO_DWORD length: 0x0 offset: 0xbc
		uint min4; // type: DATA_DWORD_2 length: 0x0 offset: 0xc0
		uint max4; // type: DATA_DWORD_2 length: 0x0 offset: 0xc4
		uint prop5; // type: NAME_TO_DWORD length: 0x0 offset: 0xc8
		uint par5; // type: CALC_TO_DWORD length: 0x0 offset: 0xcc
		uint min5; // type: DATA_DWORD_2 length: 0x0 offset: 0xd0
		uint max5; // type: DATA_DWORD_2 length: 0x0 offset: 0xd4
		uint prop6; // type: NAME_TO_DWORD length: 0x0 offset: 0xd8
		uint par6; // type: CALC_TO_DWORD length: 0x0 offset: 0xdc
		uint min6; // type: DATA_DWORD_2 length: 0x0 offset: 0xe0
		uint max6; // type: DATA_DWORD_2 length: 0x0 offset: 0xe4
		uint prop7; // type: NAME_TO_DWORD length: 0x0 offset: 0xe8
		uint par7; // type: CALC_TO_DWORD length: 0x0 offset: 0xec
		uint min7; // type: DATA_DWORD_2 length: 0x0 offset: 0xf0
		uint max7; // type: DATA_DWORD_2 length: 0x0 offset: 0xf4
		uint prop8; // type: NAME_TO_DWORD length: 0x0 offset: 0xf8
		uint par8; // type: CALC_TO_DWORD length: 0x0 offset: 0xfc
		uint min8; // type: DATA_DWORD_2 length: 0x0 offset: 0x100
		uint max8; // type: DATA_DWORD_2 length: 0x0 offset: 0x104
		uint prop9; // type: NAME_TO_DWORD length: 0x0 offset: 0x108
		uint par9; // type: CALC_TO_DWORD length: 0x0 offset: 0x10c
		uint min9; // type: DATA_DWORD_2 length: 0x0 offset: 0x110
		uint max9; // type: DATA_DWORD_2 length: 0x0 offset: 0x114
		uint aprop1a; // type: NAME_TO_DWORD length: 0x0 offset: 0x118
		uint apar1a; // type: CALC_TO_DWORD length: 0x0 offset: 0x11c
		uint amin1a; // type: DATA_DWORD_2 length: 0x0 offset: 0x120
		uint amax1a; // type: DATA_DWORD_2 length: 0x0 offset: 0x124
		uint aprop1b; // type: NAME_TO_DWORD length: 0x0 offset: 0x128
		uint apar1b; // type: CALC_TO_DWORD length: 0x0 offset: 0x12c
		uint amin1b; // type: DATA_DWORD_2 length: 0x0 offset: 0x130
		uint amax1b; // type: DATA_DWORD_2 length: 0x0 offset: 0x134
		uint aprop2a; // type: NAME_TO_DWORD length: 0x0 offset: 0x138
		uint apar2a; // type: CALC_TO_DWORD length: 0x0 offset: 0x13c
		uint amin2a; // type: DATA_DWORD_2 length: 0x0 offset: 0x140
		uint amax2a; // type: DATA_DWORD_2 length: 0x0 offset: 0x144
		uint aprop2b; // type: NAME_TO_DWORD length: 0x0 offset: 0x148
		uint apar2b; // type: CALC_TO_DWORD length: 0x0 offset: 0x14c
		uint amin2b; // type: DATA_DWORD_2 length: 0x0 offset: 0x150
		uint amax2b; // type: DATA_DWORD_2 length: 0x0 offset: 0x154
		uint aprop3a; // type: NAME_TO_DWORD length: 0x0 offset: 0x158
		uint apar3a; // type: CALC_TO_DWORD length: 0x0 offset: 0x15c
		uint amin3a; // type: DATA_DWORD_2 length: 0x0 offset: 0x160
		uint amax3a; // type: DATA_DWORD_2 length: 0x0 offset: 0x164
		uint aprop3b; // type: NAME_TO_DWORD length: 0x0 offset: 0x168
		uint apar3b; // type: CALC_TO_DWORD length: 0x0 offset: 0x16c
		uint amin3b; // type: DATA_DWORD_2 length: 0x0 offset: 0x170
		uint amax3b; // type: DATA_DWORD_2 length: 0x0 offset: 0x174
		uint aprop4a; // type: NAME_TO_DWORD length: 0x0 offset: 0x178
		uint apar4a; // type: CALC_TO_DWORD length: 0x0 offset: 0x17c
		uint amin4a; // type: DATA_DWORD_2 length: 0x0 offset: 0x180
		uint amax4a; // type: DATA_DWORD_2 length: 0x0 offset: 0x184
		uint aprop4b; // type: NAME_TO_DWORD length: 0x0 offset: 0x188
		uint apar4b; // type: CALC_TO_DWORD length: 0x0 offset: 0x18c
		uint amin4b; // type: DATA_DWORD_2 length: 0x0 offset: 0x190
		uint amax4b; // type: DATA_DWORD_2 length: 0x0 offset: 0x194
		uint aprop5a; // type: NAME_TO_DWORD length: 0x0 offset: 0x198
		uint apar5a; // type: CALC_TO_DWORD length: 0x0 offset: 0x19c
		uint amin5a; // type: DATA_DWORD_2 length: 0x0 offset: 0x1a0
		uint amax5a; // type: DATA_DWORD_2 length: 0x0 offset: 0x1a4
		uint aprop5b; // type: NAME_TO_DWORD length: 0x0 offset: 0x1a8
		uint apar5b; // type: CALC_TO_DWORD length: 0x0 offset: 0x1ac
		uint amin5b; // type: DATA_DWORD_2 length: 0x0 offset: 0x1b0
		uint amax5b; // type: DATA_DWORD_2 length: 0x0 offset: 0x1b4
		byte end; // type: END length: 0x0 offset: 0x1b8
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct UniqueItemsTable
	{
		short index; // type: DATA_WORD length: 0x0 offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string name; // type: DATA_ASCII length: 0x1F offset: 0x2
		short namestr; // type: DATA_WORD length: 0x0 offset: 0x22
		short version; // type: DATA_WORD length: 0x0 offset: 0x24
		uint code; // type: DATA_RAW length: 0x0 offset: 0x28
		UniqueItemsFlags flags;
		/*uint enabled:1; // type: DATA_BIT length: 0x0 offset: 0x2c
		uint nolimit:1; // type: DATA_BIT length: 0x1 offset: 0x2c
		uint carry1:1; // type: DATA_BIT length: 0x2 offset: 0x2c
		uint ladder:1; // type: DATA_BIT length: 0x3 offset: 0x2c*/
		short rarity; // type: DATA_WORD length: 0x0 offset: 0x30
		short lvl; // type: DATA_WORD length: 0x0 offset: 0x34
		short lvlReq; // type: DATA_WORD length: 0x0 offset: 0x36
		byte chrtransform; // type: CODE_TO_BYTE length: 0x0 offset: 0x38
		byte invtransform; // type: CODE_TO_BYTE length: 0x0 offset: 0x39
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string flippyfile; // type: DATA_ASCII length: 0x1f offset: 0x3a
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invfile; // type: DATA_ASCII length: 0x1f offset: 0x5a
		uint costMult; // type: DATA_DWORD_2 length: 0x0 offset: 0x7c
		uint costAdd; // type: DATA_DWORD_2 length: 0x0 offset: 0x80
		short dropsound; // type: NAME_TO_WORD length: 0x0 offset: 0x84
		short usesound; // type: NAME_TO_WORD length: 0x0 offset: 0x86
		byte dropsfxframe; // type: DATA_BYTE length: 0x0 offset: 0x88
		uint prop1; // type: NAME_TO_DWORD length: 0x0 offset: 0x8c
		uint par1; // type: CALC_TO_DWORD length: 0x0 offset: 0x90
		uint min1; // type: DATA_DWORD_2 length: 0x0 offset: 0x94
		uint max1; // type: DATA_DWORD_2 length: 0x0 offset: 0x98
		uint prop2; // type: NAME_TO_DWORD length: 0x0 offset: 0x9c
		uint par2; // type: CALC_TO_DWORD length: 0x0 offset: 0xa0
		uint min2; // type: DATA_DWORD_2 length: 0x0 offset: 0xa4
		uint max2; // type: DATA_DWORD_2 length: 0x0 offset: 0xa8
		uint prop3; // type: NAME_TO_DWORD length: 0x0 offset: 0xac
		uint par3; // type: CALC_TO_DWORD length: 0x0 offset: 0xb0
		uint min3; // type: DATA_DWORD_2 length: 0x0 offset: 0xb4
		uint max3; // type: DATA_DWORD_2 length: 0x0 offset: 0xb8
		uint prop4; // type: NAME_TO_DWORD length: 0x0 offset: 0xbc
		uint par4; // type: CALC_TO_DWORD length: 0x0 offset: 0xc0
		uint min4; // type: DATA_DWORD_2 length: 0x0 offset: 0xc4
		uint max4; // type: DATA_DWORD_2 length: 0x0 offset: 0xc8
		uint prop5; // type: NAME_TO_DWORD length: 0x0 offset: 0xcc
		uint par5; // type: CALC_TO_DWORD length: 0x0 offset: 0xd0
		uint min5; // type: DATA_DWORD_2 length: 0x0 offset: 0xd4
		uint max5; // type: DATA_DWORD_2 length: 0x0 offset: 0xd8
		uint prop6; // type: NAME_TO_DWORD length: 0x0 offset: 0xdc
		uint par6; // type: CALC_TO_DWORD length: 0x0 offset: 0xe0
		uint min6; // type: DATA_DWORD_2 length: 0x0 offset: 0xe4
		uint max6; // type: DATA_DWORD_2 length: 0x0 offset: 0xe8
		uint prop7; // type: NAME_TO_DWORD length: 0x0 offset: 0xec
		uint par7; // type: CALC_TO_DWORD length: 0x0 offset: 0xf0
		uint min7; // type: DATA_DWORD_2 length: 0x0 offset: 0xf4
		uint max7; // type: DATA_DWORD_2 length: 0x0 offset: 0xf8
		uint prop8; // type: NAME_TO_DWORD length: 0x0 offset: 0xfc
		uint par8; // type: CALC_TO_DWORD length: 0x0 offset: 0x100
		uint min8; // type: DATA_DWORD_2 length: 0x0 offset: 0x104
		uint max8; // type: DATA_DWORD_2 length: 0x0 offset: 0x108
		uint prop9; // type: NAME_TO_DWORD length: 0x0 offset: 0x10c
		uint par9; // type: CALC_TO_DWORD length: 0x0 offset: 0x110
		uint min9; // type: DATA_DWORD_2 length: 0x0 offset: 0x114
		uint max9; // type: DATA_DWORD_2 length: 0x0 offset: 0x118
		uint prop10; // type: NAME_TO_DWORD length: 0x0 offset: 0x11c
		uint par10; // type: CALC_TO_DWORD length: 0x0 offset: 0x120
		uint min10; // type: DATA_DWORD_2 length: 0x0 offset: 0x124
		uint max10; // type: DATA_DWORD_2 length: 0x0 offset: 0x128
		uint prop11; // type: NAME_TO_DWORD length: 0x0 offset: 0x12c
		uint par11; // type: CALC_TO_DWORD length: 0x0 offset: 0x130
		uint min11; // type: DATA_DWORD_2 length: 0x0 offset: 0x134
		uint max11; // type: DATA_DWORD_2 length: 0x0 offset: 0x138
		uint prop12; // type: NAME_TO_DWORD length: 0x0 offset: 0x13c
		uint par12; // type: CALC_TO_DWORD length: 0x0 offset: 0x140
		uint min12; // type: DATA_DWORD_2 length: 0x0 offset: 0x144
		uint max12; // type: DATA_DWORD_2 length: 0x0 offset: 0x148
		byte end; // type: END length: 0x0 offset: 0x14c
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SetsTable
	{
		short index; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
		short name; // type: KEY_TO_WORD length: 0x0 offset: 0x2
		short version; // type: DATA_WORD length: 0x0 offset: 0x4
		uint pcode2a; // type: NAME_TO_DWORD length: 0x0 offset: 0x10
		uint pparam2a; // type: CALC_TO_DWORD length: 0x0 offset: 0x14
		uint pmin2a; // type: DATA_DWORD length: 0x0 offset: 0x18
		uint pmax2a; // type: DATA_DWORD length: 0x0 offset: 0x1c
		uint pcode2b; // type: NAME_TO_DWORD length: 0x0 offset: 0x20
		uint pparam2b; // type: CALC_TO_DWORD length: 0x0 offset: 0x24
		uint pmin2b; // type: DATA_DWORD length: 0x0 offset: 0x28
		uint pmax2b; // type: DATA_DWORD length: 0x0 offset: 0x2c
		uint pcode3a; // type: NAME_TO_DWORD length: 0x0 offset: 0x30
		uint pparam3a; // type: CALC_TO_DWORD length: 0x0 offset: 0x34
		uint pmin3a; // type: DATA_DWORD length: 0x0 offset: 0x38
		uint pmax3a; // type: DATA_DWORD length: 0x0 offset: 0x3c
		uint pcode3b; // type: NAME_TO_DWORD length: 0x0 offset: 0x40
		uint pparam3b; // type: CALC_TO_DWORD length: 0x0 offset: 0x44
		uint pmin3b; // type: DATA_DWORD length: 0x0 offset: 0x48
		uint pmax3b; // type: DATA_DWORD length: 0x0 offset: 0x4c
		uint pcode4a; // type: NAME_TO_DWORD length: 0x0 offset: 0x50
		uint pparam4a; // type: CALC_TO_DWORD length: 0x0 offset: 0x54
		uint pmin4a; // type: DATA_DWORD length: 0x0 offset: 0x58
		uint pmax4a; // type: DATA_DWORD length: 0x0 offset: 0x5c
		uint pcode4b; // type: NAME_TO_DWORD length: 0x0 offset: 0x60
		uint pparam4b; // type: CALC_TO_DWORD length: 0x0 offset: 0x64
		uint pmin4b; // type: DATA_DWORD length: 0x0 offset: 0x68
		uint pmax4b; // type: DATA_DWORD length: 0x0 offset: 0x6c
		uint pcode5a; // type: NAME_TO_DWORD length: 0x0 offset: 0x70
		uint pparam5a; // type: CALC_TO_DWORD length: 0x0 offset: 0x74
		uint pmin5a; // type: DATA_DWORD length: 0x0 offset: 0x78
		uint pmax5a; // type: DATA_DWORD length: 0x0 offset: 0x7c
		uint pcode5b; // type: NAME_TO_DWORD length: 0x0 offset: 0x80
		uint pparam5b; // type: CALC_TO_DWORD length: 0x0 offset: 0x84
		uint pmin5b; // type: DATA_DWORD length: 0x0 offset: 0x88
		uint pmax5b; // type: DATA_DWORD length: 0x0 offset: 0x8c
		uint fcode1; // type: NAME_TO_DWORD length: 0x0 offset: 0x90
		uint fparam1; // type: CALC_TO_DWORD length: 0x0 offset: 0x94
		uint fmin1; // type: DATA_DWORD length: 0x0 offset: 0x98
		uint fmax1; // type: DATA_DWORD length: 0x0 offset: 0x9c
		uint fcode2; // type: NAME_TO_DWORD length: 0x0 offset: 0xa0
		uint fparam2; // type: CALC_TO_DWORD length: 0x0 offset: 0xa4
		uint fmin2; // type: DATA_DWORD length: 0x0 offset: 0xa8
		uint fmax2; // type: DATA_DWORD length: 0x0 offset: 0xac
		uint fcode3; // type: NAME_TO_DWORD length: 0x0 offset: 0xb0
		uint fparam3; // type: CALC_TO_DWORD length: 0x0 offset: 0xb4
		uint fmin3; // type: DATA_DWORD length: 0x0 offset: 0xb8
		uint fmax3; // type: DATA_DWORD length: 0x0 offset: 0xbc
		uint fcode4; // type: NAME_TO_DWORD length: 0x0 offset: 0xc0
		uint fparam4; // type: CALC_TO_DWORD length: 0x0 offset: 0xc4
		uint fmin4; // type: DATA_DWORD length: 0x0 offset: 0xc8
		uint fmax4; // type: DATA_DWORD length: 0x0 offset: 0xcc
		uint fcode5; // type: NAME_TO_DWORD length: 0x0 offset: 0xd0
		uint fparam5; // type: CALC_TO_DWORD length: 0x0 offset: 0xd4
		uint fmin5; // type: DATA_DWORD length: 0x0 offset: 0xd8
		uint fmax5; // type: DATA_DWORD length: 0x0 offset: 0xdc
		uint fcode6; // type: NAME_TO_DWORD length: 0x0 offset: 0xe0
		uint fparam6; // type: CALC_TO_DWORD length: 0x0 offset: 0xe4
		uint fmin6; // type: DATA_DWORD length: 0x0 offset: 0xe8
		uint fmax6; // type: DATA_DWORD length: 0x0 offset: 0xec
		uint fcode7; // type: NAME_TO_DWORD length: 0x0 offset: 0xf0
		uint fparam7; // type: CALC_TO_DWORD length: 0x0 offset: 0xf4
		uint fmin7; // type: DATA_DWORD length: 0x0 offset: 0xf8
		uint fmax7; // type: DATA_DWORD length: 0x0 offset: 0xfc
		uint fcode8; // type: NAME_TO_DWORD length: 0x0 offset: 0x100
		uint fparam8; // type: CALC_TO_DWORD length: 0x0 offset: 0x104
		uint fmin8; // type: DATA_DWORD length: 0x0 offset: 0x108
		uint fmax8; // type: DATA_DWORD length: 0x0 offset: 0x10c
		byte end; // type: END length: 0x0 offset: 0x128
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ItemTypesTable
	{
		uint code; // type: ASCII_TO_CODE length: 0x0 offset: 0x0
		short equiv1; // type: CODE_TO_WORD length: 0x0 offset: 0x4
		short equiv2; // type: CODE_TO_WORD length: 0x0 offset: 0x6
		byte repair; // type: DATA_BYTE length: 0x0 offset: 0x8
		byte body; // type: DATA_BYTE length: 0x0 offset: 0x9
		byte bodyloc1; // type: CODE_TO_BYTE length: 0x0 offset: 0xa
		byte bodyloc2; // type: CODE_TO_BYTE length: 0x0 offset: 0xb
		short shoots; // type: CODE_TO_WORD length: 0x0 offset: 0xc
		short quiver; // type: CODE_TO_WORD length: 0x0 offset: 0xe
		byte throwable; // type: DATA_BYTE length: 0x0 offset: 0x10
		byte reload; // type: DATA_BYTE length: 0x0 offset: 0x11
		byte reequip; // type: DATA_BYTE length: 0x0 offset: 0x12
		byte autostack; // type: DATA_BYTE length: 0x0 offset: 0x13
		byte magic; // type: DATA_BYTE length: 0x0 offset: 0x14
		byte rare; // type: DATA_BYTE length: 0x0 offset: 0x15
		byte normal; // type: DATA_BYTE length: 0x0 offset: 0x16
		byte charm; // type: DATA_BYTE length: 0x0 offset: 0x17
		byte gem; // type: DATA_BYTE length: 0x0 offset: 0x18
		byte beltable; // type: DATA_BYTE length: 0x0 offset: 0x19
		byte maxsock1; // type: DATA_BYTE length: 0x0 offset: 0x1a
		byte maxsock25; // type: DATA_BYTE length: 0x0 offset: 0x1b
		byte maxsock40; // type: DATA_BYTE length: 0x0 offset: 0x1c
		byte treasureclass; // type: DATA_BYTE length: 0x0 offset: 0x1d
		byte rarity; // type: DATA_BYTE length: 0x0 offset: 0x1e
		byte staffmods; // type: CODE_TO_BYTE length: 0x0 offset: 0x1f
		byte costformula; // type: DATA_BYTE length: 0x0 offset: 0x20
		byte Class; // type: CODE_TO_BYTE length: 0x0 offset: 0x21
		byte storepage; // type: CODE_TO_BYTE length: 0x0 offset: 0x22
		byte varinvgfx; // type: DATA_BYTE length: 0x0 offset: 0x23
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invgfx1; // type: DATA_ASCII length: 0x1f offset: 0x24
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invgfx2; // type: DATA_ASCII length: 0x1f offset: 0x44
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invgfx3; // type: DATA_ASCII length: 0x1f offset: 0x64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invgfx4; // type: DATA_ASCII length: 0x1f offset: 0x84
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invgfx5; // type: DATA_ASCII length: 0x1f offset: 0xa4
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string invgfx6; // type: DATA_ASCII length: 0x1f offset: 0xc4
		byte end; // type: END length: 0x0 offset: 0xe4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RunesTable
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3f)]
		string name; // type: DATA_ASCII length: 0x3f offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x3f)]
		string runeName; // type: DATA_ASCII length: 0x3f offset: 0x40
		byte complete; // type: DATA_BYTE length: 0x0 offset: 0x80
		byte server; // type: DATA_BYTE length: 0x0 offset: 0x81
		short itype1; // type: CODE_TO_WORD length: 0x0 offset: 0x86
		short itype2; // type: CODE_TO_WORD length: 0x0 offset: 0x88
		short itype3; // type: CODE_TO_WORD length: 0x0 offset: 0x8a
		short itype4; // type: CODE_TO_WORD length: 0x0 offset: 0x8c
		short itype5; // type: CODE_TO_WORD length: 0x0 offset: 0x8e
		short itype6; // type: CODE_TO_WORD length: 0x0 offset: 0x90
		short etype1; // type: CODE_TO_WORD length: 0x0 offset: 0x92
		short etype2; // type: CODE_TO_WORD length: 0x0 offset: 0x94
		short etype3; // type: CODE_TO_WORD length: 0x0 offset: 0x96
		uint rune1; // type: UNKNOWN_11 length: 0x0 offset: 0x98
		uint rune2; // type: UNKNOWN_11 length: 0x0 offset: 0x9c
		uint rune3; // type: UNKNOWN_11 length: 0x0 offset: 0xa0
		uint rune4; // type: UNKNOWN_11 length: 0x0 offset: 0xa4
		uint rune5; // type: UNKNOWN_11 length: 0x0 offset: 0xa8
		uint rune6; // type: UNKNOWN_11 length: 0x0 offset: 0xac
		uint t1code1; // type: NAME_TO_DWORD length: 0x0 offset: 0xb0
		uint t1param1; // type: CALC_TO_DWORD length: 0x0 offset: 0xb4
		uint t1min1; // type: DATA_DWORD_2 length: 0x0 offset: 0xb8
		uint t1max1; // type: DATA_DWORD_2 length: 0x0 offset: 0xbc
		uint t1code2; // type: NAME_TO_DWORD length: 0x0 offset: 0xc0
		uint t1param2; // type: CALC_TO_DWORD length: 0x0 offset: 0xc4
		uint t1min2; // type: DATA_DWORD_2 length: 0x0 offset: 0xc8
		uint t1max2; // type: DATA_DWORD_2 length: 0x0 offset: 0xcc
		uint t1code3; // type: NAME_TO_DWORD length: 0x0 offset: 0xd0
		uint t1param3; // type: CALC_TO_DWORD length: 0x0 offset: 0xd4
		uint t1min3; // type: DATA_DWORD_2 length: 0x0 offset: 0xd8
		uint t1max3; // type: DATA_DWORD_2 length: 0x0 offset: 0xdc
		uint t1code4; // type: NAME_TO_DWORD length: 0x0 offset: 0xe0
		uint t1param4; // type: CALC_TO_DWORD length: 0x0 offset: 0xe4
		uint t1min4; // type: DATA_DWORD_2 length: 0x0 offset: 0xe8
		uint t1max4; // type: DATA_DWORD_2 length: 0x0 offset: 0xec
		uint t1code5; // type: NAME_TO_DWORD length: 0x0 offset: 0xf0
		uint t1param5; // type: CALC_TO_DWORD length: 0x0 offset: 0xf4
		uint t1min5; // type: DATA_DWORD_2 length: 0x0 offset: 0xf8
		uint t1max5; // type: DATA_DWORD_2 length: 0x0 offset: 0xfc
		uint t1code6; // type: NAME_TO_DWORD length: 0x0 offset: 0x100
		uint t1param6; // type: CALC_TO_DWORD length: 0x0 offset: 0x104
		uint t1min6; // type: DATA_DWORD_2 length: 0x0 offset: 0x108
		uint t1max6; // type: DATA_DWORD_2 length: 0x0 offset: 0x10c
		uint t1code7; // type: NAME_TO_DWORD length: 0x0 offset: 0x110
		uint t1param7; // type: CALC_TO_DWORD length: 0x0 offset: 0x114
		uint t1min7; // type: DATA_DWORD_2 length: 0x0 offset: 0x118
		uint t1max7; // type: DATA_DWORD_2 length: 0x0 offset: 0x11c
		byte end; // type: END length: 0x0 offset: 0x120
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct GemsTable
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1f)]
		string name; // type: DATA_ASCII length: 0x1f offset: 0x0
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x5)]
		string letter; // type: DATA_ASCII length: 0x5 offset: 0x20
		uint code; // type: UNKNOWN_11 length: 0x0 offset: 0x28
		byte nummods; // type: DATA_BYTE length: 0x0 offset: 0x2e
		byte transform; // type: DATA_BYTE length: 0x0 offset: 0x2f
		uint weaponmod1code; // type: NAME_TO_DWORD length: 0x0 offset: 0x30
		uint weaponmod1param; // type: CALC_TO_DWORD length: 0x0 offset: 0x34
		uint weaponmod1min; // type: DATA_DWORD_2 length: 0x0 offset: 0x38
		uint weaponmod1max; // type: DATA_DWORD_2 length: 0x0 offset: 0x3c
		uint weaponmod2code; // type: NAME_TO_DWORD length: 0x0 offset: 0x40
		uint weaponmod2param; // type: CALC_TO_DWORD length: 0x0 offset: 0x44
		uint weaponmod2min; // type: DATA_DWORD_2 length: 0x0 offset: 0x48
		uint weaponmod2max; // type: DATA_DWORD_2 length: 0x0 offset: 0x4c
		uint weaponmod3code; // type: NAME_TO_DWORD length: 0x0 offset: 0x50
		uint weaponmod3param; // type: CALC_TO_DWORD length: 0x0 offset: 0x54
		uint weaponmod3min; // type: DATA_DWORD_2 length: 0x0 offset: 0x58
		uint weaponmod3max; // type: DATA_DWORD_2 length: 0x0 offset: 0x5c
		uint helmmod1code; // type: NAME_TO_DWORD length: 0x0 offset: 0x60
		uint helmmod1param; // type: CALC_TO_DWORD length: 0x0 offset: 0x64
		uint helmmod1min; // type: DATA_DWORD_2 length: 0x0 offset: 0x68
		uint helmmod1max; // type: DATA_DWORD_2 length: 0x0 offset: 0x6c
		uint helmmod2code; // type: NAME_TO_DWORD length: 0x0 offset: 0x70
		uint helmmod2param; // type: CALC_TO_DWORD length: 0x0 offset: 0x74
		uint helmmod2min; // type: DATA_DWORD_2 length: 0x0 offset: 0x78
		uint helmmod2max; // type: DATA_DWORD_2 length: 0x0 offset: 0x7c
		uint helmmod3code; // type: NAME_TO_DWORD length: 0x0 offset: 0x80
		uint helmmod3param; // type: CALC_TO_DWORD length: 0x0 offset: 0x84
		uint helmmod3min; // type: DATA_DWORD_2 length: 0x0 offset: 0x88
		uint helmmod3max; // type: DATA_DWORD_2 length: 0x0 offset: 0x8c
		uint shieldmod1code; // type: NAME_TO_DWORD length: 0x0 offset: 0x90
		uint shieldmod1param; // type: CALC_TO_DWORD length: 0x0 offset: 0x94
		uint shieldmod1min; // type: DATA_DWORD_2 length: 0x0 offset: 0x98
		uint shieldmod1max; // type: DATA_DWORD_2 length: 0x0 offset: 0x9c
		uint shieldmod2code; // type: NAME_TO_DWORD length: 0x0 offset: 0xa0
		uint shieldmod2param; // type: CALC_TO_DWORD length: 0x0 offset: 0xa4
		uint shieldmod2min; // type: DATA_DWORD_2 length: 0x0 offset: 0xa8
		uint shieldmod2max; // type: DATA_DWORD_2 length: 0x0 offset: 0xac
		uint shieldmod3code; // type: NAME_TO_DWORD length: 0x0 offset: 0xb0
		uint shieldmod3param; // type: CALC_TO_DWORD length: 0x0 offset: 0xb4
		uint shieldmod3min; // type: DATA_DWORD_2 length: 0x0 offset: 0xb8
		uint shieldmod3max; // type: DATA_DWORD_2 length: 0x0 offset: 0xbc
		byte end; // type: END length: 0x0 offset: 0xc0
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct PetTypeTable
	{
		short petType; // type: NAME_TO_INDEX_2 length: 0x0 offset: 0x0
		PetTypeFlags flags;
		/*uint warp:1; // type: DATA_BIT length: 0x0 offset: 0x4
		uint range:1; // type: DATA_BIT length: 0x1 offset: 0x4
		uint partysend:1; // type: DATA_BIT length: 0x2 offset: 0x4
		uint unsummon:1; // type: DATA_BIT length: 0x3 offset: 0x4
		uint automap:1; // type: DATA_BIT length: 0x4 offset: 0x4
		uint drawhp:1; // type: DATA_BIT length: 0x5 offset: 0x4*/
		short group; // type: DATA_WORD length: 0x0 offset: 0x8
		short basemax; // type: DATA_WORD length: 0x0 offset: 0xa
		short name; // type: KEY_TO_WORD length: 0x0 offset: 0xc
		byte icontype; // type: DATA_BYTE length: 0x0 offset: 0xe
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		string baseicon; // type: DATA_ASCII length: 0x20 offset: 0xf
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		string micon1; // type: DATA_ASCII length: 0x20 offset: 0x2f
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		string micon2; // type: DATA_ASCII length: 0x20 offset: 0x4f
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		string micon3; // type: DATA_ASCII length: 0x20 offset: 0x6f
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		string micon4; // type: DATA_ASCII length: 0x20 offset: 0x8f
		short mclass1; // type: DATA_WORD length: 0x0 offset: 0xb2
		short mclass2; // type: DATA_WORD length: 0x0 offset: 0xb4
		short mclass3; // type: DATA_WORD length: 0x0 offset: 0xb6
		short mclass4; // type: DATA_WORD length: 0x0 offset: 0xb8
		byte end; // type: END length: 0x0 offset: 0xe0
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SuperuniquesTable
	{
		short Superunique; // type: NAME_TO_INDEX length: 0x0 offset: 0x0
		short Name; // type: KEY_TO_WORD length: 0x0 offset: 0x2
		uint Class; // type: NAME_TO_DWORD length: 0x0 offset: 0x4
		uint hcIdx; // type: DATA_DWORD length: 0x0 offset: 0x8
		uint Mod1; // type: DATA_DWORD length: 0x0 offset: 0xc
		uint Mod2; // type: DATA_DWORD length: 0x0 offset: 0x10
		uint Mod3; // type: DATA_DWORD length: 0x0 offset: 0x14
		uint MonSound; // type: NAME_TO_DWORD length: 0x0 offset: 0x18
		uint MinGrp; // type: DATA_DWORD length: 0x0 offset: 0x1c
		uint MaxGrp; // type: DATA_DWORD length: 0x0 offset: 0x20
		byte AutoPos; // type: DATA_BYTE length: 0x0 offset: 0x24
		byte EClass; // type: DATA_BYTE length: 0x0 offset: 0x25
		byte Stacks; // type: DATA_BYTE length: 0x0 offset: 0x26
		byte Replaceable; // type: DATA_BYTE length: 0x0 offset: 0x27
		byte Utrans; // type: DATA_BYTE length: 0x0 offset: 0x28
		byte UtransN; // type: DATA_BYTE length: 0x0 offset: 0x29
		byte UtransH; // type: DATA_BYTE length: 0x0 offset: 0x2a
		short TC; // type: NAME_TO_WORD length: 0x0 offset: 0x2c
		short TCN; // type: NAME_TO_WORD length: 0x0 offset: 0x2e
		short TCH; // type: NAME_TO_WORD length: 0x0 offset: 0x30
		byte end; // type: END length: 0x0 offset: 0x34
	}
	#endregion
}
