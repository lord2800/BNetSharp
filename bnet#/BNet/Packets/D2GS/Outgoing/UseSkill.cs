namespace BNet.D2GS.Outgoing
{
	using BNet;
	using Objects;

	public sealed class ShiftLeftSkill : D2GSPacket
	{
		public ShiftLeftSkill(Point point) : base(D2GSOutgoingPacketId.ShiftLeftClick)
		{
			Write(point.X);
			Write(point.Y);
		}
	}

	public sealed class LeftSkillOnUnit : D2GSPacket
	{
		public LeftSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.LeftClickUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftLeftSkillOnUnit : D2GSPacket
	{
		public ShiftLeftSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.ShiftLeftClickUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftHoldLeftSkill : D2GSPacket
	{
		public ShiftHoldLeftSkill(Point point) : base(D2GSOutgoingPacketId.ShiftLeftClickHold)
		{
			Write(point.X);
			Write(point.Y);
		}
	}

	public sealed class HoldLeftSkillOnUnit : D2GSPacket
	{
		public HoldLeftSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.LeftClickHoldUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftHoldLeftSkillOnUnit : D2GSPacket
	{
		public ShiftHoldLeftSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.ShiftLeftClickHoldUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftRightSkill : D2GSPacket
	{
		public ShiftRightSkill(Point point) : base(D2GSOutgoingPacketId.ShiftRightClick)
		{
			Write(point.X);
			Write(point.Y);
		}
	}

	public sealed class RightSkillOnUnit : D2GSPacket
	{
		public RightSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.RightClickUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftRightSkillOnUnit : D2GSPacket
	{
		public ShiftRightSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.ShiftRightClickUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftHoldRightSkill : D2GSPacket
	{
		public ShiftHoldRightSkill(Point point) : base(D2GSOutgoingPacketId.ShiftRightClickHold)
		{
			Write(point.X);
			Write(point.Y);
		}
	}

	public sealed class HoldRightSkillOnUnit : D2GSPacket
	{
		public HoldRightSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.RightClickHoldUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class ShiftHoldRightSkillOnUnit : D2GSPacket
	{
		public ShiftHoldRightSkillOnUnit(Monster unit) : base(D2GSOutgoingPacketId.ShiftRightClickHoldUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}
}
