namespace BNet.D2GS.Outgoing
{
	using Objects;

	public sealed class Walk : D2GSPacket
	{
		public Walk(Point point) : base(D2GSOutgoingPacketId.Walk)
		{
			Write(point.X);
			Write(point.Y);
		}
	}

	public sealed class WalkToUnit : D2GSPacket
	{
		public WalkToUnit(IUnit unit) : base(D2GSOutgoingPacketId.WalkToUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}

	public sealed class Run : D2GSPacket
	{
		public Run(Point point) : base(D2GSOutgoingPacketId.Run)
		{
			Write(point.X);
			Write(point.Y);
		}
	}

	public sealed class RunToUnit : D2GSPacket
	{
		public RunToUnit(IUnit unit) : base(D2GSOutgoingPacketId.RunToUnit)
		{
			Write(unit.Kind);
			Write(unit.Id);
		}
	}
}
