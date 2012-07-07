namespace BNet.D2GS.Outgoing
{
	using BNet;
	using Objects;
	using Exceptions;

	public sealed class PickItem : D2GSPacket
	{
		public PickItem(Item item, bool cursor) : base(D2GSOutgoingPacketId.PickItem)
		{
			Write<uint>(4);
			Write(item.Id);
			Write(cursor ? 1 : 0);
		}
	}

	public sealed class DropItem : D2GSPacket
	{
		public DropItem(Item item) : base(D2GSOutgoingPacketId.DropItem) { Write(item.Id); }
	}

	public sealed class InsertItem : D2GSPacket
	{
		public InsertItem(Item item, Point location, ItemBuffer buffer) : base(D2GSOutgoingPacketId.InsertItemToBuffer)
		{
			Write(item.Id);
			Write<uint>(location.X);
			Write<uint>(location.Y);
			Write((uint)buffer);
		}
	}

	public sealed class RemoveItem : D2GSPacket
	{
		public RemoveItem(Item item) : base(D2GSOutgoingPacketId.RemoveItemFromBuffer) { Write(item.Id); }
	}

	public sealed class EquipItem : D2GSPacket
	{
		public EquipItem(Item item, EquipmentLocation location) : base(D2GSOutgoingPacketId.EquipItem)
		{
			Write(item.Id);
			Write((ushort)location);
			Write<ushort>(0);
		}
	}

	public sealed class SwapCursorItem : D2GSPacket
	{
		public SwapCursorItem(Item item, EquipmentLocation location) : base(D2GSOutgoingPacketId.SwapCursorWithBody)
		{
			Write(item.Id);
			Write((ushort)location);
			Write<ushort>(0);
		}
	}

	public sealed class Swap2HItem : D2GSPacket
	{
		public Swap2HItem(Item item, EquipmentLocation location) : base(D2GSOutgoingPacketId.Swap2HItem)
		{
			Write(item.Id);
			Write((ushort)location);
			Write<ushort>(0);
		}
	}

	public sealed class UnequipItem : D2GSPacket
	{
		public UnequipItem(EquipmentLocation location) : base(D2GSOutgoingPacketId.RemoveBodyItem) { Write((ushort)location); }
	}

	public sealed class SwapTwo1HItemsWith2HItem : D2GSPacket
	{
		public SwapTwo1HItemsWith2HItem() : base(D2GSOutgoingPacketId.Swap1HItemWith2HItem) { throw new UnknownPacketException(); }
	}

	public sealed class SwapCursorAndBuffer : D2GSPacket
	{
		public SwapCursorAndBuffer(Item cursor, Item buffer, Point location) : base(D2GSOutgoingPacketId.SwapCursorAndBufferItem)
		{
			Write(cursor.Id);
			Write(buffer.Id);
			Write<uint>(location.X);
			Write<uint>(location.Y);
		}
	}

	public sealed class UseItem : D2GSPacket
	{
		public UseItem(Item item, Player self) : base(D2GSOutgoingPacketId.ActivateBufferItem)
		{
			Write(item.Id);
			Write(self.Location.X);
			Write<ushort>(0);
			Write(self.Location.Y);
			Write<ushort>(0);
		}
	}

	public sealed class StackItems : D2GSPacket
	{
		public StackItems(Item toStack, Item isStack) : base(D2GSOutgoingPacketId.StackItems)
		{
			Write(toStack.Id);
			Write(isStack.Id);
		}
	}

	public sealed class UnstackItems : D2GSPacket
	{
		public UnstackItems() : base(D2GSOutgoingPacketId.UnstackItems) { throw new UnknownPacketException(); }
	}

	public sealed class MoveToBelt : D2GSPacket
	{
		public MoveToBelt(Item item, Point location) : base(D2GSOutgoingPacketId.ItemToBelt)
		{
			Write(item.Id);
			Write(location.X);
			Write(location.Y);
		}
	}

	public sealed class SendItemToBelt : D2GSPacket
	{
		public SendItemToBelt(Item item) : base(D2GSOutgoingPacketId.SendItemToBelt) { Write(item.Id); }
	}

	public sealed class TakeFromBelt : D2GSPacket
	{
		public TakeFromBelt(Item item) : base(D2GSOutgoingPacketId.ItemFromBelt) { Write(item.Id); }
	}

	public sealed class SwapBeltItem : D2GSPacket
	{
		public SwapBeltItem(Item cursor, Item belt) : base(D2GSOutgoingPacketId.SwapBeltItem)
		{
			Write(cursor.Id);
			Write(belt.Id);
		}
	}

	public sealed class UseBeltItem : D2GSPacket
	{
		public UseBeltItem(Item item, bool toMerc) : base(D2GSOutgoingPacketId.UseBeltItem)
		{
			Write(item.Id);
			Write(toMerc ? 1 : 0);
			Write<uint>(0);
		}
	}

	public sealed class IdentifyItem : D2GSPacket
	{
		public IdentifyItem(Item item, Item scroll) : base(D2GSOutgoingPacketId.IdentifyItem)
		{
			Write(item.Id);
			Write(scroll.Id);
		}
	}

	public sealed class SocketItem : D2GSPacket
	{
		public SocketItem(Item item, Item socketable) : base(D2GSOutgoingPacketId.SocketItem)
		{
			Write(item.Id);
			Write(socketable.Id);
		}
	}

	public sealed class AddScrollToBook : D2GSPacket
	{
		public AddScrollToBook(Item scroll, Item book) : base(D2GSOutgoingPacketId.ScrollToBook)
		{
			Write(scroll.Id);
			Write(book.Id);
		}
	}

	public sealed class ItemToCube : D2GSPacket
	{
		public ItemToCube(Item item, Item cube) : base(D2GSOutgoingPacketId.ItemToCube)
		{
			Write(item.Id);
			Write(cube.Id);
		}
	}
}
