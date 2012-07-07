namespace BNet.D2GS.Outgoing
{
	using Objects;
	using Exceptions;

	public sealed class RequestUnitUpdate : D2GSPacket
	{
		public RequestUnitUpdate(IUnit unit) : base(D2GSOutgoingPacketId.RequestEntityUpdate)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class HighlightDoor : D2GSPacket
	{
		public HighlightDoor(Entity entity) : base(D2GSOutgoingPacketId.HighlightDoor)
		{
			Write(entity.Id);
			throw new UnknownPacketException();
		}
	}

	public sealed class ActivateScroll : D2GSPacket
	{
		public ActivateScroll(Item inifussScroll) : base(D2GSOutgoingPacketId.ActivateInifussScroll) { Write(inifussScroll.Id); }
	}

	public sealed class InsertHoradricStaff : D2GSPacket
	{
		public InsertHoradricStaff(Entity orifice, Item staff, uint state) : base(D2GSOutgoingPacketId.InsertHoradricStaff)
		{
			Write(orifice.Kind);
			Write(orifice.Id);
			Write(staff.Id);
			Write(state);
		}
	}

	public sealed class PlayNPCAudio : D2GSPacket
	{
		public PlayNPCAudio(ushort id) : base(D2GSOutgoingPacketId.PlayNPCMessage) { Write(id); }
	}

	public sealed class ForceMove : D2GSPacket
	{
		public ForceMove(IUnit unit, Point location) : base(D2GSOutgoingPacketId.MakeEntityMove)
		{
			Write(unit.Kind);
			Write(unit.Id);
			Write(location.X);
			Write<ushort>(0);
			Write(location.Y);
			Write<ushort>(0);
		}
	}
}
