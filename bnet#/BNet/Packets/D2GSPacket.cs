namespace BNet.D2GS
{
	using System;
	using System.IO;
	using System.Collections.Generic;
	using System.Reflection;
	using Utilities;

	public delegate bool CompletePacket(byte command, List<byte> bytes);

	internal sealed class D2GSPacketBuilder : PacketBuilder<D2GSPacketHandlerAttribute>
	{
		private const BindingFlags PublicStaticFlat = BindingFlags.Static|BindingFlags.Public|BindingFlags.FlattenHierarchy;

		private readonly List<D2GSPacket> completePackets = new List<D2GSPacket>();
		private readonly Dictionary<Type, CompletePacket> lenProcessor = new Dictionary<Type, CompletePacket>();

		public override void ProcessTypes(params Assembly[] assemblies)
		{
			base.ProcessTypes(assemblies);

			// find the length processors
			foreach(var processor in processors)
			{
				var type = processor.Value.Key;

				// all packet definitions must have a PacketLength attribute
				if(!type.HasAttribute(typeof(PacketLengthAttribute)))
					throw new InvalidPacketDefinitionException(type);

				var length = type.GetAttribute<PacketLengthAttribute>();

				// if the length is a fixed size, then the handler is the D2GSPacket one
				if(length.FixedSize) lenProcessor.Add(type, D2GSPacket.IsCompletePacket);
				else
				{
					// otherwise we search for one, and if we don't find it then we complain
					var method = type.GetMethod("IsCompletePacket", PublicStaticFlat);
					if(method == null)
						throw new InvalidPacketDefinitionException(type);

					var complete = (CompletePacket)Delegate.CreateDelegate(typeof(CompletePacket), method);
					lenProcessor.Add(type, complete);
				}
			}
		}

		public override Packet Parse(BufferedStream buff)
		{
			if(completePackets.Count > 0)
			{
				// just return the next completed packet
				Packet packet = completePackets[0];
				completePackets.RemoveAt(0);
				return packet;
			}

			var size = buff.ReadByte();
			if(size == -1) return null;

			if(size >= 0xF0) size = (((size & 0xF) << 8) + buff.ReadByte() - 2);
			else size -= 1;

			var buf = new byte[size];
			buff.BlockRead(buf);

			byte[] output;
			Huffman.Decompress(buf, out output);

			var opcount = 0;
			do
			{
				var command = (D2GSIncomingPacketId)(output[opcount]);

				var type = ProcessType((byte)command);
				CompletePacket complete = lenProcessor[type];
				var len = type.GetAttribute<PacketLengthAttribute>().Length;

				var packet = new List<byte>();
				if(len > 1)
				{
					var temp = new byte[len - 1];
					Array.Copy(output, opcount + 1, temp, 0, len - 1);
					packet.AddRange(temp);
				}

				opcount += len;

				for(; !complete((byte)command, packet) && output.Length > opcount; opcount++)
					packet.Add(output[opcount]);

				completePackets.Add((D2GSPacket)processors[(byte)command].Value(packet.ToArray()));
			} while(opcount < output.Length);

			Packet first = completePackets[0];
			completePackets.RemoveAt(0);
			return first;
		}
	}

	public enum D2GSOutgoingPacketId : byte
	{
		Walk = 0x01,
		WalkToUnit = 0x02,
		Run = 0x03,
		RunToUnit = 0x04,
		ShiftLeftClick = 0x05,
		LeftClickUnit = 0x06,
		ShiftLeftClickUnit = 0x07,
		ShiftLeftClickHold = 0x08,
		LeftClickHoldUnit = 0x09,
		ShiftLeftClickHoldUnit = 0x0A,
		ShiftRightClick = 0x0C,
		RightClickUnit = 0x0D,
		ShiftRightClickUnit = 0x0E,
		ShiftRightClickHold = 0x0F,
		RightClickHoldUnit = 0x10,
		ShiftRightClickHoldUnit = 0x11,
		EntityInteract = 0x13,
		OverheadChat = 0x14,
		Chat = 0x15,
		PickItem = 0x16,
		DropItem = 0x17,
		InsertItemToBuffer = 0x18,
		RemoveItemFromBuffer = 0x19,
		EquipItem = 0x1A,
		Swap2HItem = 0x1B,
		RemoveBodyItem = 0x1C,
		SwapCursorWithBody = 0x1D,
		Swap1HItemWith2HItem = 0x1E,
		SwapCursorAndBufferItem = 0x1F,
		ActivateBufferItem = 0x20,
		StackItems = 0x21,
		UnstackItems = 0x22,
		ItemToBelt = 0x23,
		ItemFromBelt = 0x24,
		SwapBeltItem = 0x25,
		UseBeltItem = 0x26,
		IdentifyItem = 0x27,
		SocketItem = 0x28,
		ScrollToBook = 0x29,
		ItemToCube = 0x2A,
		InitiateEntityChat = 0x2F,
		TerminateEntityChat = 0x30,
		QuestMessage = 0x31,
		BuyItem = 0x32,
		SellItem = 0x33,
		IdentifyItems = 0x34,
		Repair = 0x35,
		HireMerc = 0x36,
		IdentifyFromGamble = 0x37,
		EntityAction = 0x38,
		PurchaseLife = 0x39,
		AddStatPoint = 0x3A,
		AddSkillPoint = 0x3B,
		SelectSkill = 0x3C,
		HighlightDoor = 0x3D,
		ActivateInifussScroll = 0x3E,
		PlayAudio = 0x3F,
		RequestQuestData = 0x40,
		Resurrect = 0x41,
		InsertHoradricStaff = 0x44,
		HaveMercInteract = 0x46,
		MoveMerc = 0x47,
		TurnOffBusy = 0x48,
		TakeWaypoint = 0x49,
		RequestEntityUpdate = 0x4B,
		Transmute = 0x4C,
		PlayNPCMessage = 0x4D,
		ClickButton = 0x4F,
		DropGold = 0x50,
		BindHotkey = 0x51,
		QuestComplete = 0x58,
		MakeEntityMove = 0x59,
		SetPlayerRelation = 0x5D,
		InvitePlayer = 0x5E,
		UpdatePlayerLocation = 0x5F,
		SwapWeapons = 0x60,
		DropOrEquipMercItem = 0x61,
		ResurrectMerc = 0x62,
		SendItemToBelt = 0x63,
		WardenResponse = 0x66,
		Startup = 0x68,
		LeaveGame = 0x69,
		JoinGame = 0x6B,
		Ping = 0x6D
	}

	public enum D2GSIncomingPacketId : byte
	{
		GameLoading = 0x00,
		GameFlags = 0x01,
		LoadSuccessful = 0x02,
		LoadAct = 0x03,
		LoadActComplete = 0x04,
		UnloadActComplete = 0x05,
		GameExitSuccess = 0x06,
		MapReveal = 0x07,
		MapHide = 0x08,
		AssignLevelWarp = 0x09,
		RemoveObject = 0x0A,
		GameHandshake = 0x0B,
		NPCHit = 0x0C,
		PlayerStop = 0x0D,
		ObjectState = 0x0E,
		PlayerMove = 0x0F,
		PlayerToTarget = 0x10,
		ReportKill = 0x11,
		Recv12 = 0x12,
		Recv13 = 0x13,
		Recv14 = 0x14,
		ReassignPlayer = 0x15,
		Recv18 = 0x18,
		GoldToInventory = 0x19,
		AddExperienceByte = 0x1A,
		AddExperienceWord = 0x1B,
		AddExperienceDword = 0x1C,
		AddAttributeByte = 0x1D,
		AddAttributeWord = 0x1E,
		AddAttributeDword = 0x1F,
		AttributeUpdate = 0x20,
		UpdateItemOSkill = 0x21,
		UpdateItemSkill = 0x22,
		SetSkill = 0x23,
		Recv24 = 0x24,
		Recv25 = 0x25,
		ReceiveChat = 0x26,
		NPCInfo = 0x27,
		QuestInfo = 0x28,
		GameQuestInfo = 0x29,
		NPCTransaction = 0x2A,
		PlaySound = 0x2C,
		UpdateItemStats = 0x3E,
		UseStackableItem = 0x3F,
		Recv40 = 0x40,
		ClearCursor = 0x42,
		Recv45 = 0x45,
		Relator1 = 0x47,
		Relator2 = 0x48,
		UseSkillOnTarget = 0x4C,
		UseSkillOnPoint = 0x4D,
		MercForHire = 0x4E,
		StartMercList = 0x4F,
		Recv50 = 0x50,
		AssignObject = 0x51,
		QuestLogInfo = 0x52,
		PlayerSlotRefresh = 0x53,
		Recv54 = 0x54,
		Recv55 = 0x55,
		Recv58 = 0x58,
		AssignPlayer = 0x59,
		EventMessage = 0x5A,
		PlayerInGame = 0x5B,
		PlayerLeftGame = 0x5C,
		QuestItemState = 0x5D,
		Recv5E = 0x5E,
		Recv5F = 0x5F,
		TownPortalState = 0x60,
		Recv61 = 0x61,
		Recv62 = 0x62,
		WaypointMenu = 0x63,
		PlayerKillCount = 0x65,
		Recv66 = 0x66,
		NPCMove = 0x67,
		NPCMoveToTarget = 0x68,
		NPCState = 0x69,
		Recv6A = 0x6A,
		NPCAction = 0x6B,
		NPCAttack = 0x6C,
		NPCStop = 0x6D,
		Recv6E = 0x6E,
		Recv6F = 0x6F,
		Recv70 = 0x70,
		Recv71 = 0x71,
		Recv72 = 0x72,
		Recv73 = 0x73,
		PlayerCorpseAssign = 0x74,
		PlayerPartyInfo = 0x75,
		PlayerInProximity = 0x76,
		ButtonAction = 0x77,
		TradeAccepted = 0x78,
		GoldInTrade = 0x79,
		PetAction = 0x7A,
		AssignHotkey = 0x7B,
		UseScroll = 0x7C,
		SetItemState = 0x7D,
		Recv7E = 0x7E,
		AllyPartyInfo = 0x7F,
		AssignMerc = 0x81,
		PortalOwner = 0x82,
		Recv86 = 0x86,
		SpecialQuestEvent = 0x89,
		NPCWantInteract = 0x8A,
		PlayerRelationship = 0x8B,
		RelationshipUpdate = 0x8C,
		AssignPlayerToParty = 0x8D,
		CorpseAssign = 0x8E,
		Pong = 0x8F,
		PartyAutomapInfo = 0x90,
		Recv91 = 0x91,
		Recv92 = 0x92,
		Recv93 = 0x93,
		BaseSkillLevels = 0x94,
		LifeManaUpdate = 0x95,
		WalkVerify = 0x96,
		WeaponSwitch = 0x97,
		Recv98 = 0x98,
		SkillTriggered = 0x99,
		Recv9A = 0x9A,
		Recv9B = 0x9B,
		WorldItemAction = 0x9C,
		OwnedItemAction = 0x9D,
		MercAttributeByte = 0x9E,
		MercAttributeWord = 0x9F,
		MercAttributeDword = 0xA0,
		AddMercExperienceByte = 0xA1,
		AddMercExperienceWord = 0xA2,
		Recva3 = 0xA3,
		Recva4 = 0xA4,
		ChargeupActive = 0xA5,
		DelayedState = 0xA7,
		SetState = 0xA8,
		EndState = 0xA9,
		AddUnit = 0xAA,
		NPCHeal = 0xAB,
		AssignNPC = 0xAC,
		Recvad = 0xAD,
		WardenRequest = 0xAE,
		GameDropped = 0xB0,
		Recvb1 = 0xB1,
		IPBan = 0xB3,
	}

	public class D2GSPacket : D2Packet
	{
		private readonly byte id;

		public D2GSPacket(D2GSOutgoingPacketId id) { this.id = (byte)id; }
		public D2GSPacket(D2GSIncomingPacketId id) { this.id = (byte)id; }
		public override byte[] GetHeader() { return new[] {id}; }

		public static bool IsCompletePacket(byte command, List<byte> input) { return true; }
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class D2GSPacketHandlerAttribute : Attribute, IPacketHandler
	{
		public D2GSPacketHandlerAttribute(D2GSIncomingPacketId id) { Header = id; }
		public D2GSIncomingPacketId Header { get; set; }

		#region IPacketHandler Members

		public byte HeaderId
		{
			get { return (byte)Header; }
		}

		#endregion
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PacketLengthAttribute : Attribute
	{
		public PacketLengthAttribute()
		{
			Length = 1;
			FixedSize = true;
		}

		public PacketLengthAttribute(int length, bool fixedSize)
		{
			Length = length;
			FixedSize = fixedSize;
		}

		public int Length { get; set; }
		public bool FixedSize { get; set; }
	}
}
