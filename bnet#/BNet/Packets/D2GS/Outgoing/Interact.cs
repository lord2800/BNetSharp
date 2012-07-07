namespace BNet.D2GS.Outgoing
{
	using Objects;
	using Exceptions;

	public sealed class Interact : D2GSPacket
	{
		public Interact(IUnit unit) : base(D2GSOutgoingPacketId.EntityInteract)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class InitiateChat : D2GSPacket
	{
		public InitiateChat(NPC npc) : base(D2GSOutgoingPacketId.InitiateEntityChat)
		{
			Write(npc.Kind);
			Write(npc.Id);
		}
	}

	public sealed class TerminateChat : D2GSPacket
	{
		public TerminateChat(NPC npc) : base(D2GSOutgoingPacketId.TerminateEntityChat)
		{
			Write(npc.Kind);
			Write(npc.Id);
		}
	}

	public sealed class BuyItem : D2GSPacket
	{
		public BuyItem(NPC npc, Item item, BuyFlags flags, uint cost) : base(D2GSOutgoingPacketId.BuyItem)
		{
			Write(npc.Id);
			Write(item.Id);
			Write<ushort>(0);
			Write((ushort)flags);
			Write(cost);
		}
	}

	public sealed class SellItem : D2GSPacket
	{
		public SellItem(NPC npc, Item item, uint cost) : base(D2GSOutgoingPacketId.SellItem)
		{
			Write(npc.Id);
			Write(item.Id);
			Write<uint>(4);
			Write(cost);
		}
	}

	public sealed class NPCItemIdentify : D2GSPacket
	{
		public NPCItemIdentify(NPC npc) : base(D2GSOutgoingPacketId.IdentifyItems) { Write(npc.Id); }
	}

	public sealed class RepairItem : D2GSPacket
	{
		public RepairItem(NPC npc) : base(D2GSOutgoingPacketId.Repair)
		{
			Write(npc.Id);
			Write<uint>(0);
			Write<uint>(0);
			Write<uint>(0x80);
		}

		public RepairItem(NPC npc, Item item, uint cost) : base(D2GSOutgoingPacketId.Repair)
		{
			Write(npc.Id);
			Write(item.Id);
			Write<uint>(1);
			Write(cost);
		}
	}

	public sealed class GambleIdentify : D2GSPacket
	{
		public GambleIdentify(Item item) : base(D2GSOutgoingPacketId.IdentifyFromGamble) { Write(item.Id); }
	}

	public sealed class NPCMenuAction : D2GSPacket
	{
		public NPCMenuAction(NPC npc, uint entry) : base(D2GSOutgoingPacketId.EntityAction)
		{
			Write(npc.Id);
			Write(entry);
			Write<uint>(0);
			throw new UnknownPacketException();
		}
	}

	public sealed class PurchaseLife : D2GSPacket
	{
		public PurchaseLife(NPC npc) : base(D2GSOutgoingPacketId.PurchaseLife) { Write(npc.Id); }
	}

	// TODO: destination enum
	public sealed class TakeWaypoint : D2GSPacket
	{
		public TakeWaypoint(Entity waypoint, byte dest) : base(D2GSOutgoingPacketId.TakeWaypoint)
		{
			Write(waypoint.Id);
			Write(dest);
			Write<byte>(0);
			Write<ushort>(0);
		}
	}

	public sealed class Transmute : D2GSPacket
	{
		public Transmute(Item cube) : base(D2GSOutgoingPacketId.Transmute) { Write(cube.Id); }
	}

	public sealed class ClickButton : D2GSPacket
	{
		public ClickButton(uint id, uint unknown) : base(D2GSOutgoingPacketId.ClickButton)
		{
			Write(id);
			Write(unknown);
			throw new UnknownPacketException();
		}
	}
}
