namespace BNet.D2GS.Outgoing
{
	using Objects;
	using Exceptions;

	public sealed class QuestMessage : D2GSPacket
	{
		public QuestMessage() : base(D2GSOutgoingPacketId.QuestMessage) { throw new UnknownPacketException(); }
	}

	// TODO: stat enum
	public sealed class AddStat : D2GSPacket
	{
		public AddStat(ushort stat) : base(D2GSOutgoingPacketId.AddStatPoint) { Write(stat); }
	}

	// TODO: skill enum
	public sealed class AddSkill : D2GSPacket
	{
		public AddSkill(ushort skill) : base(D2GSOutgoingPacketId.AddSkillPoint) { Write(skill); }
	}

	public sealed class SelectSkill : D2GSPacket
	{
		public SelectSkill(Skill skill, bool leftHand) : this(skill, leftHand, null) { }

		public SelectSkill(Skill skill, bool leftHand, Item item) : base(D2GSOutgoingPacketId.SelectSkill)
		{
			Write(skill.Id);
			Write<byte>(0);
			Write(leftHand ? (byte)80 : (byte)0);
			Write(item == null ? (unchecked((uint)-1)) : item.Id);
		}
	}

	// TODO: sound enum
	public sealed class PlayAudio : D2GSPacket
	{
		public PlayAudio(ushort id) : base(D2GSOutgoingPacketId.PlayAudio) { Write(id); }
	}

	public sealed class RequestQuestData : D2GSPacket
	{
		public RequestQuestData() : base(D2GSOutgoingPacketId.RequestQuestData) { }
	}

	public sealed class Resurrect : D2GSPacket
	{
		public Resurrect() : base(D2GSOutgoingPacketId.Resurrect) { }
	}

	public sealed class DisableBusyState : D2GSPacket
	{
		public DisableBusyState() : base(D2GSOutgoingPacketId.TurnOffBusy) { }
	}

	public sealed class DropGold : D2GSPacket
	{
		public DropGold(Player self, uint amount) : base(D2GSOutgoingPacketId.DropGold)
		{
			Write(self.Id);
			Write(amount);
		}
	}

	public sealed class BindHotkey : D2GSPacket
	{
		public BindHotkey(Skill skill, bool leftHand, ushort hotkey) : base(D2GSOutgoingPacketId.BindHotkey)
		{
			Write((byte)skill.Id);
			Write(leftHand ? (byte)80 : (byte)0);
			Write(hotkey);
			Write(0xFFFFFFFF);
		}
	}

	// TODO: quest enum
	public sealed class QuestComplete : D2GSPacket
	{
		public QuestComplete(ushort id) : base(D2GSOutgoingPacketId.QuestComplete) { Write(id); }
	}

	public class PlayerRelation : D2GSPacket
	{
		public PlayerRelation(Player player, PlayerRelationType type, bool toggle)
			: base(D2GSOutgoingPacketId.SetPlayerRelation)
		{
			Write((byte)type);
			Write(toggle ? (byte)1 : (byte)0);
			Write(player.Id);
		}
	}

	public sealed class Squelch : PlayerRelation
	{
		public Squelch(Player player, bool toggle) : base(player, PlayerRelationType.Squelch, toggle) { }
	}

	public sealed class Hostile : PlayerRelation
	{
		public Hostile(Player player, bool toggle) : base(player, PlayerRelationType.Hostile, toggle) { }
	}

	public sealed class Loot : PlayerRelation
	{
		public Loot(Player player, bool toggle) : base(player, PlayerRelationType.Loot, toggle) { }
	}

	public sealed class Mute : PlayerRelation
	{
		public Mute(Player player, bool toggle) : base(player, PlayerRelationType.Mute, toggle) { }
	}

	public class PlayerPartyAction : D2GSPacket
	{
		public PlayerPartyAction(Player player, PartyAction action) : base(D2GSOutgoingPacketId.InvitePlayer)
		{
			Write((byte)action);
			Write(player.Id);
		}
	}

	public sealed class InvitePlayer : PlayerPartyAction
	{
		public InvitePlayer(Player player) : base(player, PartyAction.Invite) { }
	}

	public sealed class CancelInvite : PlayerPartyAction
	{
		public CancelInvite(Player player) : base(player, PartyAction.CancelInvite) { }
	}

	public sealed class AcceptInvite : PlayerPartyAction
	{
		public AcceptInvite(Player player) : base(player, PartyAction.AcceptInvite) { }
	}

	public sealed class UpdatePosition : D2GSPacket
	{
		public UpdatePosition(Point location) : base(D2GSOutgoingPacketId.UpdatePlayerLocation)
		{
			Write(location.X);
			Write(location.Y);
		}
	}

	public sealed class SwapWeapons : D2GSPacket
	{
		public SwapWeapons() : base(D2GSOutgoingPacketId.SwapWeapons) { }
	}

	// TODO: position enum
	public sealed class HandleMercItem : D2GSPacket
	{
		public HandleMercItem(ushort position) : base(D2GSOutgoingPacketId.DropOrEquipMercItem) { Write(position); }
	}
}
