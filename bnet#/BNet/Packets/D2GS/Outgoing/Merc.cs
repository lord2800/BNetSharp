namespace BNet.D2GS.Outgoing
{
	using Objects;

	public sealed class HireMerc : D2GSPacket
	{
		public HireMerc(NPC npc, Monster merc) : base(D2GSOutgoingPacketId.HireMerc)
		{
			Write(npc.Id);
			Write(merc.Id);
		}
	}

	public sealed class HaveMercInteract : D2GSPacket
	{
		public HaveMercInteract(Monster merc, NPC npc) : base(D2GSOutgoingPacketId.HaveMercInteract)
		{
			Write(merc.Id);
			Write(npc.Id);
			Write(npc.Kind);
		}
	}

	public sealed class MoveMerc : D2GSPacket
	{
		public MoveMerc(Monster merc, Point location) : base(D2GSOutgoingPacketId.MoveMerc)
		{
			Write(merc.Id);
			Write(location.X);
			Write<ushort>(0);
			Write(location.Y);
			Write<ushort>(0);
		}
	}

	public sealed class ResurrectMerc : D2GSPacket
	{
		public ResurrectMerc(NPC npc) : base(D2GSOutgoingPacketId.ResurrectMerc) { Write(npc.Id); }
	}
}
