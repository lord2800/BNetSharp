using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BNet.Objects;
using Utilities;

namespace BNet.D2GS.Incoming
{
	public class ItemAction : D2GSPacket
	{
		public Objects.ItemAction Action { get; private set; }
		public Item Item { get; private set; }
		public Player Owner { get; private set; }
		public byte OwnerAction { get; private set; }
		public byte Destination { get; private set; }

		public new static bool IsCompletePacket(byte command, List<byte> input) { return input.Count == input[1] - 1; }
		public ItemAction(D2GSIncomingPacketId id, byte[] bytes) : base(id)
		{
			SeedBytes(bytes);

			BitReader reader = new BitReader(bytes);
			Item = new Item();

			Action = (Objects.ItemAction)reader.Read<byte>();
			byte size = reader.Read<byte>();
			Item.Category = (ItemCategory)reader.Read<byte>();
			Item.Id = reader.Read<uint>();
			if(id == D2GSIncomingPacketId.OwnedItemAction)
			{
				OwnerAction = reader.Read<byte>();
				uint ownerid = reader.Read<uint>();
			}

			Item.Flags = (ItemFlags)reader.Read<uint>();

			Item.Version = reader.Read<uint>(8);

			reader.Read(2);

			Destination = reader.Read<byte>(3);
			bool onground = (Destination == 0x03);

			Item.Container = ItemContainer.Unspecified;
			Item.Directory = 0;
			if(onground)
			{
				Item.Location = new Point() { X = reader.Read<ushort>(), Y = reader.Read<ushort>() };
			}
			else
			{
				Item.Directory = reader.Read<byte>(4);
				Item.Location = new Point() { X = reader.Read<byte>(4), Y = reader.Read<byte>(3) };
				Item.Container = (ItemContainer)reader.Read<byte>(4);
			}

			#region fix location
			if(Action == Objects.ItemAction.AddToShop || Action == Objects.ItemAction.RemoveFromShop)
			{
				Item.Container = (ItemContainer)(((int)Item.Container) | 0x80);
				if((((int)Item.Container) & 1) == 1)
				{
					Item.Container = (ItemContainer)((int)Item.Container)-1;
					Item.Location = new Point() { X = Item.Location.X, Y = (ushort)(Item.Location.Y + 8) };
				}
			}
			else if(Item.Container == ItemContainer.Unspecified)
			{
				if(Item.Directory == 0)
				{
					if(Item.Flags.HasFlag(ItemFlags.InSocket))
						Item.Container = ItemContainer.Item;
					else if(Action == Objects.ItemAction.PutInBelt || Action == Objects.ItemAction.RemoveFromBelt)
					{
						Item.Container = ItemContainer.Belt;
						Item.Location = new Point() {
							Y = (ushort)(Item.Location.X / 4),
							X = (ushort)(Item.Location.X % 4)
						};
					}
				}
			}
			#endregion

			if(Item.Flags.HasFlag(ItemFlags.IsEar))
			{
				Item.EarCharacterClass = reader.Read<byte>(3);
				Item.EarLevel = reader.Read<ushort>(7);
				Item.EarName = reader.ReadString();
				return;
			}

			string code = reader.ReadString(4).Trim();

			if(String.Compare(code, "gld", StringComparison.OrdinalIgnoreCase) == 0)
			{
				bool big = reader.ReadBit();
				if(big) Item.GoldAmount = reader.Read<uint>();
				else Item.GoldAmount = reader.Read<uint>(12);
				return;
			}

			Item.UsedSockets = reader.Read<ushort>(3);
			if(Item.Flags.HasFlag(ItemFlags.SimpleItem) || Item.Flags.HasFlag(ItemFlags.Gambling))
				return;

			Item.Level = reader.Read<byte>(7);
			Item.Quality = (ItemQuality)reader.Read<byte>(4);

			bool hasgraphic = reader.ReadBit();
			Item.Graphic = hasgraphic ? new Nullable<byte>(reader.Read<byte>(3)) : null;

			bool hascolor = reader.ReadBit();
			Item.Color = hascolor ? new Nullable<ushort>(reader.Read<ushort>(11)) : null;

			if(Item.Flags.HasFlag(ItemFlags.Identified))
			{
				switch(Item.Quality)
				{
					case ItemQuality.Inferior: Item.Prefix = reader.Read<byte>(3); break;
					case ItemQuality.Superior: Item.SuperiorType = reader.Read<byte>(3); break;
					case ItemQuality.Magical: Item.Prefix = reader.Read<ushort>(11); Item.Suffix = reader.Read<ushort>(11); break;
					case ItemQuality.Crafted:
					case ItemQuality.Rare:
						Item.Prefix = (ushort)(reader.Read<ushort>(8) - 156);
						Item.Suffix = (ushort)(reader.Read<ushort>(8) - 1);
						break;
					case ItemQuality.Set: Item.SetCode = reader.Read<ushort>(12); break;
					case ItemQuality.Unique:
						if(String.Compare(code, "std", StringComparison.OrdinalIgnoreCase) != 0)
							Item.UniqueCode = reader.Read<ushort>(12);
						break;
				}
			}

			if(Item.Quality == ItemQuality.Rare || Item.Quality == ItemQuality.Crafted)
			{
				for(int i = 0; i < 3; i++)
				{
					if(reader.ReadBit()) Item.Prefixes.Add(reader.Read<ushort>(11));
					if(reader.ReadBit()) Item.Suffixes.Add(reader.Read<ushort>(11));
				}
			}

			if(Item.Flags.HasFlag(ItemFlags.Runeword))
			{
				Item.RunewordId = reader.Read<ushort>(12);
				Item.RunewordParameter = reader.Read<byte>(4);
			}

			if(Item.Flags.HasFlag(ItemFlags.Personalized))
			{
				Item.Name = reader.ReadString();
			}

			ItemData[] items = DataManager.LoadAs<ItemData>("items.dat");
			var entry = items.First(e => String.Compare(e.Code, code, StringComparison.OrdinalIgnoreCase) == 0);

			bool isarmor = entry is ArmorData;
			bool isweapon = entry is WeaponData;

			if(isarmor)
			{
				Item.Defense = (ushort)(reader.Read<ushort>(11) - 10);
			}

			if((isweapon || isarmor) && entry.NoDurability) reader.Read(8);
			else if(isarmor || isweapon)
			{
				Item.MaxDurability = reader.Read<byte>();
				Item.Indestructible = Item.MaxDurability == 0;
				Item.Durability = reader.Read<byte>();
				reader.Read(1);
			}

			if(Item.Flags.HasFlag(ItemFlags.HasSockets))
			{
				Item.Sockets = reader.Read<byte>(4);
			}

			if(!Item.Flags.HasFlag(ItemFlags.Identified))
				return;

			if(isweapon && entry.Stackable)
			{
				if(entry.Useable) reader.Read(5);
				Item.Amount = reader.Read<ushort>(9);
			}

			if(Item.Quality == ItemQuality.Set)
			{
				Item.SetMods = reader.Read<ushort>(5);
			}

			StatData[] statData = DataManager.LoadAs<StatData>("stats.dat");

			Dictionary<ushort, int[]> ParamCount = new Dictionary<ushort, int[]>()
			{
				{17,  new[]{18}}, {18,  new[]{17}},
				{48,  new[]{49}}, {50,  new[]{51}}, {52,  new[]{53}}, {54,  new[]{55, 56}}, {57,  new[]{58, 59}},
			};

			do
			{
				ushort statId = reader.Read<ushort>(9);
				if(statId == 0x1FF) break;

				var stat = statData.First(e => e.Id == statId);
				int param = 0;
				List<int> extras = new List<int>();
				long value = 0;

				if(stat.SaveParamBits > 0)
				{
					param = reader.Read<int>(stat.SaveParamBits);
				}

				if(ParamCount.ContainsKey(statId))
				{
					foreach(var s in ParamCount[statId])
					{
						var substat = statData.First(e => e.Id == s);
						extras.Add(reader.Read<int>(substat.SaveBits));
					}
				}

				if(String.Compare(stat.OpBase, "level", StringComparison.OrdinalIgnoreCase) == 0)
				{
					value = reader.Read<long>(stat.SaveBits);
				}
				else
				{
					switch(statId)
					{
						case 252: case 253: value = reader.Read<long>(stat.SaveBits); break;
						default: value = reader.Read<long>(stat.SaveBits) + stat.SaveAdd; break;
					}
				}

				Item.ItemProperties.Add(new Objects.Item.Property(statId, param, value, extras.ToArray()));
			} while(true);
		}
	}

	[PacketLength(Length = 3, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.WorldItemAction)]
	public sealed class WorldItemAction : ItemAction
	{
		public WorldItemAction(byte[] bytes) : base(D2GSIncomingPacketId.WorldItemAction, bytes) {}
		public static WorldItemAction Parse(byte[] bytes) { return new WorldItemAction(bytes); }
	}

	[PacketLength(Length = 3, FixedSize = false)]
	[D2GSPacketHandler(D2GSIncomingPacketId.OwnedItemAction)]
	public sealed class OwnedItemAction : ItemAction
	{
		public OwnedItemAction(byte[] bytes) : base(D2GSIncomingPacketId.OwnedItemAction, bytes) {}
		public static OwnedItemAction Parse(byte[] bytes) { return new OwnedItemAction(bytes); }
	}
}
