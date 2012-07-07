using System;
using System.Collections.Generic;
using System.Linq;

namespace BNet.Objects
{
	public interface IUnit
	{
		uint Id { get; }
		uint Kind { get; }

		Point Location { get; }
	}

	public class Player : IUnit
	{
		// Players are other players in the game, including yourself
		private readonly List<Item> items = new List<Item>();

		public IEnumerable<Item> Items
		{
			get { return items.AsEnumerable(); }
		}

		public uint Id { get; protected set; }
		public uint Kind { get; protected set; }

		public Point Location { get; protected set; }
	}

	public class NPC : IUnit
	{
		// NPCs are neutral aligned monsters
		private readonly List<Item> items = new List<Item>();

		public IEnumerable<Item> Items
		{
			get { return items.AsEnumerable(); }
		}

		public uint Id { get; protected set; }
		public uint Kind { get; protected set; }

		public Point Location { get; protected set; }
	}

	public class Monster : IUnit
	{
		// Monsters are friendly or hostile aligned monsters, alignment property and owner determine which is which

		public uint Id { get; protected set; }
		public uint Kind { get; protected set; }

		public Point Location { get; protected set; }
	}

	public class Item : IUnit
	{
		// Items are equippable units
		//private readonly IUnit owner;

		//public IUnit Owner { get { return owner; } }

		public uint Id { get; internal set; }
		public uint Kind { get; internal set; }

		public Point Location { get; internal set; }

		public string Code { get; internal set; }
		public string Name { get; internal set; }
		public ItemCategory Category { get; internal set; }
		public Size Size { get; internal set; }
		public uint Version { get; internal set; }

		public uint Defense { get; internal set; }
		public uint Durability { get; internal set; }
		public uint MaxDurability { get; internal set; }
		public bool Indestructible { get; internal set; }
		public ushort Amount { get; internal set; }
		public uint Sockets { get; internal set; }

		public ItemFlags Flags { get; internal set; }

		public bool UnspecifiedDirectory { get; internal set; }

		public uint Directory { get; internal set; }
		public ItemContainer Container { get; internal set; }

		public uint EarCharacterClass { get; internal set; }
		public uint EarLevel { get; internal set; }
		public string EarName { get; internal set; }

		public uint GoldAmount { get; internal set; }
		public uint UsedSockets { get; internal set; }
		public uint Level { get; internal set; }
		public ItemQuality Quality { get; internal set; }

		public uint? Graphic { get; internal set; }
		public uint? Color { get; internal set; }

		public uint Prefix { get; internal set; }
		public uint Suffix { get; internal set; }

		public List<uint> Prefixes { get; internal set; }
		public List<uint> Suffixes { get; internal set; }

		public uint SuperiorType { get; internal set; }

		public uint SetCode { get; internal set; }
		public ushort SetMods { get; internal set; }
		public uint UniqueCode { get; internal set; }

		public uint RunewordId { get; internal set; }
		public uint RunewordParameter { get; internal set; }

		public string PersonalizedName { get; internal set; }

		public List<Property> ItemProperties { get; internal set; }

		public sealed class Property
		{
			public Property(uint stat, int param, long value, params int[] extras) { Stat = stat; Param = param; Value = value; Extras = extras; }
			public uint Stat { get; private set; }
			public int Param { get; private set; }
			public long Value { get; private set; }
			public int[] Extras { get; private set; }
		}
	}

	public class Entity : IUnit
	{
		public uint Id { get; protected set; }
		public uint Kind { get; protected set; }

		public Point Location { get; protected set; }
	}

	public sealed class Skill
	{
		public ushort Id { get; private set; }
		public byte Level { get; private set; }
	}


	[Flags]
	public enum BuyFlags : ushort
	{
		None = 0,
		FillStack = 0x8000,
	}

	public enum PlayerRelationType
	{
		Loot = 1,
		Mute = 2,
		Squelch = 3,
		Hostile = 4,
	}

	public enum PartyAction
	{
		Invite = 0x06,
		CancelInvite = 0x07,
		AcceptInvite = 0x08,
	}

	public enum ItemQuality : byte
	{
		NotApplicable,
		Inferior,
		Normal,
		Superior,
		Magical,
		Set,
		Rare,
		Unique,
		Crafted,
		SuperCrafted
	}
	public enum ItemContainer : uint
	{
		Unspecified = 0x00,
		Inventory = 0x02,
		TraderOffer = 0x04,
		ForTrade = 0x06,
		Cube = 0x08,
		Stash = 0x0A,
		Belt = 0x20,
		Item = 0x40,
		ArmorTab = 0x82,
		WeaponTab1 = 0x84,
		WeaponTab2 = 0x86,
		MiscTab = 0x88
	}
	public enum ItemAction : byte
	{
		AddToGround = 0x00,
		GroundToCursor = 0x01,
		DropToGround = 0x02,
		OnGround = 0x03,
		PutInContainer = 0x04,
		RemoveFromContainer = 0x05,
		Equip = 0x06,
		IndirectlySwapBodyItem = 0x07,
		Unequip = 0x08,
		SwapBodyItem = 0x09,
		AddQuantity = 0x0A,
		AddToShop = 0x0B,
		RemoveFromShop = 0x0C,
		SwapInContainer = 0x0D,
		PutInBelt = 0x0E,
		RemoveFromBelt = 0x0F,
		SwapInBelt = 0x10,
		AutoUnequip = 0x11,
		ToCursor = 0x12,
		ItemInSocket = 0x13,
		UpdateStats = 0x15,
		WeaponSwitch = 0x17
	}
	public enum ItemCategory : byte
	{
		Helm = 0x0,
		Armor = 0x1,
		Weapon = 0x5,
		Bow = 0x6,
		Shield = 0x7,
		Misc = 0x10
	}
	public enum ItemBuffer
	{
		Inventory = 0,
		Trade = 2,
		Cube = 3,
		Stash = 4
	}
	public enum EquipmentLocation
	{
		NotApplicable = 0,
		Helm = 1,
		Amulet = 2,
		Armor = 3,
		RightHand = 4,
		LeftHand = 5,
		RightHandRing = 6,
		LeftHandRing = 7,
		Belt = 8,
		Boots = 9,
		Gloves = 10,
		RightHandSwitch = 11,
		LeftHandSwitch = 12,
	}
	[Flags]
	public enum ItemFlags : uint
	{
		Equipped = 0x1,
		Unknown1 = 0x2,
		Unknown2 = 0x4,
		InSocket = 0x8,
		Identified = 0x10,
		Unknown3 = 0x20,
		SwitchIn = 0x40,
		SwitchOut = 0x80,
		Broken = 0x100,
		Unknown4 = 0x200,
		Potion = 0x400,
		HasSockets = 0x800,
		Unknown5 = 0x1000,
		InStore = 0x2000,
		NotInSocket = 0x4000,
		Unknown6 = 0x8000,
		IsEar = 0x10000,
		StarterItem = 0x20000,
		Unknown7 = 0x40000,
		Unknown8 = 0x80000,
		Unknown9 = 0x100000,
		SimpleItem = 0x200000,
		Ethereal = 0x400000,
		Unknown10 = 0x800000,
		Personalized = 0x1000000,
		Gambling = 0x2000000,
		Runeword = 0x4000000,
		Unknown11 = 0x8000000,
		Unknown12 = 0x10000000,
		Unknown13 = 0x20000000,
		Unknown14 = 0x40000000,
		Unknown15 = 0x80000000
	}
}
