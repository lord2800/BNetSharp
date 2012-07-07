using System.IO;
using BNet.D2GS.Outgoing;
using BNet.MCP;

namespace BNet.D2GS
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Collections.Generic;
	using Utilities;
	using Incoming;
	using Exceptions;

	public sealed class D2GSClient : Client<D2GSPacketHandlerAttribute>
	{
		#region handlers

		private readonly Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();

		public event EventHandler<PacketEventArgs> AddAttributeByteEvent
		{
			add
			{
				Type t = typeof(AddAttributeByte);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddAttributeByte);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddAttributeWordEvent
		{
			add
			{
				Type t = typeof(AddAttributeWord);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddAttributeWord);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddAttributeDwordEvent
		{
			add
			{
				Type t = typeof(AddAttributeDword);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddAttributeDword);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddExperienceByteEvent
		{
			add
			{
				Type t = typeof(AddExperienceByte);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddExperienceByte);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddExperienceWordEvent
		{
			add
			{
				Type t = typeof(AddExperienceWord);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddExperienceWord);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddExperienceDwordEvent
		{
			add
			{
				Type t = typeof(AddExperienceDword);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddExperienceDword);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddUnitEvent
		{
			add
			{
				Type t = typeof(AddUnit);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddUnit);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AllyPartyInfoEvent
		{
			add
			{
				Type t = typeof(AllyPartyInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AllyPartyInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignHotkeyEvent
		{
			add
			{
				Type t = typeof(AssignHotkey);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignHotkey);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignLevelWarpEvent
		{
			add
			{
				Type t = typeof(AssignLevelWarp);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignLevelWarp);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignMercEvent
		{
			add
			{
				Type t = typeof(AssignMerc);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignMerc);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignNPCEvent
		{
			add
			{
				Type t = typeof(AssignNPC);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignNPC);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignObjectEvent
		{
			add
			{
				Type t = typeof(AssignObject);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignObject);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignPlayerEvent
		{
			add
			{
				Type t = typeof(AssignPlayer);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignPlayer);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AssignPlayerToPartyEvent
		{
			add
			{
				Type t = typeof(AssignPlayerToParty);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AssignPlayerToParty);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AttributeUpdateEvent
		{
			add
			{
				Type t = typeof(AttributeUpdate);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AttributeUpdate);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> BaseSkillLevelsEvent
		{
			add
			{
				Type t = typeof(BaseSkillLevels);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(BaseSkillLevels);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ButtonActionEvent
		{
			add
			{
				Type t = typeof(ButtonAction);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ButtonAction);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ChargeupActiveEvent
		{
			add
			{
				Type t = typeof(ChargeupActive);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ChargeupActive);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ClearCursorEvent
		{
			add
			{
				Type t = typeof(ClearCursor);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ClearCursor);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> CorpseAssignEvent
		{
			add
			{
				Type t = typeof(CorpseAssign);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(CorpseAssign);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> DelayedStateEvent
		{
			add
			{
				Type t = typeof(DelayedState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(DelayedState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> EndStateEvent
		{
			add
			{
				Type t = typeof(EndState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(EndState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> EventMessageEvent
		{
			add
			{
				Type t = typeof(EventMessage);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(EventMessage);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameQuestInfoEvent
		{
			add
			{
				Type t = typeof(GameQuestInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameQuestInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameDroppedEvent
		{
			add
			{
				Type t = typeof(GameDropped);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameDropped);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameExitSuccessEvent
		{
			add
			{
				Type t = typeof(GameExitSuccess);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameExitSuccess);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameFlagsEvent
		{
			add
			{
				Type t = typeof(GameFlags);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameFlags);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameHandshakeEvent
		{
			add
			{
				Type t = typeof(GameHandshake);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameHandshake);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameLoadedEvent
		{
			add
			{
				Type t = typeof(GameLoaded);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameLoaded);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GameLoadingEvent
		{
			add
			{
				Type t = typeof(GameLoading);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GameLoading);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> LoadActEvent
		{
			add
			{
				Type t = typeof(LoadAct);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(LoadAct);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> LoadActCompleteEvent
		{
			add
			{
				Type t = typeof(LoadActComplete);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(LoadActComplete);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UnloadActCompleteEvent
		{
			add
			{
				Type t = typeof(UnloadActComplete);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UnloadActComplete);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GoldInTradeEvent
		{
			add
			{
				Type t = typeof(GoldInTrade);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GoldInTrade);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> GoldToInventoryEvent
		{
			add
			{
				Type t = typeof(GoldToInventory);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(GoldToInventory);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> IPBanEvent
		{
			add
			{
				Type t = typeof(IPBan);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(IPBan);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> WorldItemActionEvent
		{
			add
			{
				Type t = typeof(WorldItemAction);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(WorldItemAction);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> OwnedItemActionEvent
		{
			add
			{
				Type t = typeof(OwnedItemAction);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(OwnedItemAction);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> LifeManaUpdateEvent
		{
			add
			{
				Type t = typeof(LifeManaUpdate);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(LifeManaUpdate);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> MapHideEvent
		{
			add
			{
				Type t = typeof(MapHide);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(MapHide);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> MapRevealEvent
		{
			add
			{
				Type t = typeof(MapReveal);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(MapReveal);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> MercAttributeByteEvent
		{
			add
			{
				Type t = typeof(MercAttributeByte);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(MercAttributeByte);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> MercAttributeWordEvent
		{
			add
			{
				Type t = typeof(MercAttributeWord);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(MercAttributeWord);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> MercAttributeDwordEvent
		{
			add
			{
				Type t = typeof(MercAttributeDword);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(MercAttributeDword);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddMercExperienceByteEvent
		{
			add
			{
				Type t = typeof(AddMercExperienceByte);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddMercExperienceByte);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> AddMercExperienceWordEvent
		{
			add
			{
				Type t = typeof(AddMercExperienceWord);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(AddMercExperienceWord);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> MercForHireEvent
		{
			add
			{
				Type t = typeof(MercForHire);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(MercForHire);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCActionEvent
		{
			add
			{
				Type t = typeof(NPCAction);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCAction);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCAttackEvent
		{
			add
			{
				Type t = typeof(NPCAttack);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCAttack);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCHealEvent
		{
			add
			{
				Type t = typeof(NPCHeal);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCHeal);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCHitEvent
		{
			add
			{
				Type t = typeof(NPCHit);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCHit);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCInfoEvent
		{
			add
			{
				Type t = typeof(NPCInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCMoveEvent
		{
			add
			{
				Type t = typeof(NPCMove);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCMove);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCMoveToTargetEvent
		{
			add
			{
				Type t = typeof(NPCMoveToTarget);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCMoveToTarget);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCStateEvent
		{
			add
			{
				Type t = typeof(NPCState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCStopEvent
		{
			add
			{
				Type t = typeof(NPCStop);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCStop);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCTransactionEvent
		{
			add
			{
				Type t = typeof(NPCTransaction);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCTransaction);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> NPCWantInteractEvent
		{
			add
			{
				Type t = typeof(NPCWantInteract);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(NPCWantInteract);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ObjectStateEvent
		{
			add
			{
				Type t = typeof(ObjectState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ObjectState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PartyAutomapInfoEvent
		{
			add
			{
				Type t = typeof(PartyAutomapInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PartyAutomapInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PetActionEvent
		{
			add
			{
				Type t = typeof(PetAction);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PetAction);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerCorpseAssignEvent
		{
			add
			{
				Type t = typeof(PlayerCorpseAssign);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerCorpseAssign);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerInGameEvent
		{
			add
			{
				Type t = typeof(PlayerInGame);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerInGame);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerInProximityEvent
		{
			add
			{
				Type t = typeof(PlayerInProximity);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerInProximity);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerKillCountEvent
		{
			add
			{
				Type t = typeof(PlayerKillCount);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerKillCount);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerLeftGameEvent
		{
			add
			{
				Type t = typeof(PlayerLeftGame);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerLeftGame);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerMoveEvent
		{
			add
			{
				Type t = typeof(PlayerMove);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerMove);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerPartyInfoEvent
		{
			add
			{
				Type t = typeof(PlayerPartyInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerPartyInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerRelationshipEvent
		{
			add
			{
				Type t = typeof(PlayerRelationship);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerRelationship);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerSlotRefreshEvent
		{
			add
			{
				Type t = typeof(PlayerSlotRefresh);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerSlotRefresh);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerStopEvent
		{
			add
			{
				Type t = typeof(PlayerStop);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerStop);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlayerToTargetEvent
		{
			add
			{
				Type t = typeof(PlayerToTarget);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlayerToTarget);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PlaySoundEvent
		{
			add
			{
				Type t = typeof(PlaySound);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PlaySound);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PongEvent
		{
			add
			{
				Type t = typeof(Pong);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Pong);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> PortalOwnerEvent
		{
			add
			{
				Type t = typeof(PortalOwner);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(PortalOwner);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> QuestInfoEvent
		{
			add
			{
				Type t = typeof(QuestInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(QuestInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> QuestItemStateEvent
		{
			add
			{
				Type t = typeof(QuestItemState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(QuestItemState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> QuestLogInfoEvent
		{
			add
			{
				Type t = typeof(QuestLogInfo);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(QuestLogInfo);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ReassignPlayerEvent
		{
			add
			{
				Type t = typeof(ReassignPlayer);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ReassignPlayer);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ReceiveChatEvent
		{
			add
			{
				Type t = typeof(ReceiveChat);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ReceiveChat);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv12Event
		{
			add
			{
				Type t = typeof(Recv12);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv12);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv13Event
		{
			add
			{
				Type t = typeof(Recv13);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv13);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv14Event
		{
			add
			{
				Type t = typeof(Recv14);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv14);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv18Event
		{
			add
			{
				Type t = typeof(Recv18);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv18);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv24Event
		{
			add
			{
				Type t = typeof(Recv24);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv24);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv25Event
		{
			add
			{
				Type t = typeof(Recv25);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv25);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv40Event
		{
			add
			{
				Type t = typeof(Recv40);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv40);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv45Event
		{
			add
			{
				Type t = typeof(Recv45);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv45);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv50Event
		{
			add
			{
				Type t = typeof(Recv50);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv50);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv54Event
		{
			add
			{
				Type t = typeof(Recv54);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv54);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv55Event
		{
			add
			{
				Type t = typeof(Recv55);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv55);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv58Event
		{
			add
			{
				Type t = typeof(Recv58);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv58);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv61Event
		{
			add
			{
				Type t = typeof(Recv61);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv61);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv62Event
		{
			add
			{
				Type t = typeof(Recv62);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv62);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv66Event
		{
			add
			{
				Type t = typeof(Recv66);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv66);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv6AEvent
		{
			add
			{
				Type t = typeof(Recv6A);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv6A);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv6EEvent
		{
			add
			{
				Type t = typeof(Recv6E);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv6E);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv6FEvent
		{
			add
			{
				Type t = typeof(Recv6F);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv6F);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv70Event
		{
			add
			{
				Type t = typeof(Recv70);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv70);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv71Event
		{
			add
			{
				Type t = typeof(Recv71);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv71);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv72Event
		{
			add
			{
				Type t = typeof(Recv72);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv72);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv73Event
		{
			add
			{
				Type t = typeof(Recv73);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv73);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv91Event
		{
			add
			{
				Type t = typeof(Recv91);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv91);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv92Event
		{
			add
			{
				Type t = typeof(Recv92);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv92);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv93Event
		{
			add
			{
				Type t = typeof(Recv93);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv93);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv98Event
		{
			add
			{
				Type t = typeof(Recv98);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv98);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv9AEvent
		{
			add
			{
				Type t = typeof(Recv9A);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv9A);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv9BEvent
		{
			add
			{
				Type t = typeof(Recv9B);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv9B);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recva3Event
		{
			add
			{
				Type t = typeof(Recva3);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recva3);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recva4Event
		{
			add
			{
				Type t = typeof(Recva4);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recva4);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> RecvadEvent
		{
			add
			{
				Type t = typeof(Recvad);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recvad);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recvb1Event
		{
			add
			{
				Type t = typeof(Recvb1);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recvb1);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv5EEvent
		{
			add
			{
				Type t = typeof(Recv5E);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv5E);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv5FEvent
		{
			add
			{
				Type t = typeof(Recv5F);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv5F);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Recv7EEvent
		{
			add
			{
				Type t = typeof(Recv7E);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Recv7E);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> RelationshipUpdateEvent
		{
			add
			{
				Type t = typeof(RelationshipUpdate);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(RelationshipUpdate);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Relator1Event
		{
			add
			{
				Type t = typeof(Relator1);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Relator1);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> Relator2Event
		{
			add
			{
				Type t = typeof(Relator2);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(Relator2);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> RemoveObjectEvent
		{
			add
			{
				Type t = typeof(RemoveObject);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(RemoveObject);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> ReportKillEvent
		{
			add
			{
				Type t = typeof(ReportKill);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(ReportKill);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> SetItemStateEvent
		{
			add
			{
				Type t = typeof(SetItemState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(SetItemState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> SetSkillEvent
		{
			add
			{
				Type t = typeof(SetSkill);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(SetSkill);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> SetStateEvent
		{
			add
			{
				Type t = typeof(SetState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(SetState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> SkillTriggeredEvent
		{
			add
			{
				Type t = typeof(SkillTriggered);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(SkillTriggered);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> SpecialQuestEvent
		{
			add
			{
				Type t = typeof(SpecialQuestEvent);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(SpecialQuestEvent);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> StartMercListEvent
		{
			add
			{
				Type t = typeof(StartMercList);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(StartMercList);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> TownPortalStateEvent
		{
			add
			{
				Type t = typeof(TownPortalState);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(TownPortalState);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> TradeAcceptedEvent
		{
			add
			{
				Type t = typeof(TradeAccepted);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(TradeAccepted);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UpdateItemOSkillEvent
		{
			add
			{
				Type t = typeof(UpdateItemOSkill);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UpdateItemOSkill);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UpdateItemSkillEvent
		{
			add
			{
				Type t = typeof(UpdateItemSkill);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UpdateItemSkill);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UpdateItemStatsEvent
		{
			add
			{
				Type t = typeof(UpdateItemStats);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UpdateItemStats);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UseScrollEvent
		{
			add
			{
				Type t = typeof(UseScroll);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UseScroll);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UseSkillOnPointEvent
		{
			add
			{
				Type t = typeof(UseSkillOnPoint);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UseSkillOnPoint);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UseSkillOnTargetEvent
		{
			add
			{
				Type t = typeof(UseSkillOnTarget);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UseSkillOnTarget);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> UseStackableItemEvent
		{
			add
			{
				Type t = typeof(UseStackableItem);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(UseStackableItem);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> WalkVerifyEvent
		{
			add
			{
				Type t = typeof(WalkVerify);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(WalkVerify);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> WardenRequestEvent
		{
			add
			{
				Type t = typeof(WardenRequest);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(WardenRequest);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> WaypointMenuEvent
		{
			add
			{
				Type t = typeof(WaypointMenu);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(WaypointMenu);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}
		public event EventHandler<PacketEventArgs> WeaponSwitchEvent
		{
			add
			{
				Type t = typeof(WeaponSwitch);
				if(!events.ContainsKey(t)) events.Add(t, value);
				else events[t] = Delegate.Combine(events[t], value);
			}
			remove
			{
				Type t = typeof(WeaponSwitch);
				if(events.ContainsKey(t))
					events[t] = Delegate.Remove(events[t], value);
			}
		}

		#endregion

		public D2GSClient(IPEndPoint host)
		{
			builder = new D2GSPacketBuilder();
			Host = host;
		}

		public override void Connect() { throw new InvalidOperationException("You must not call Connect without all of the proper parameters"); }

		public void Connect(uint version, uint serverHash, ushort serverToken, CharInfo charInfo)
		{
			base.Connect();

			try
			{
				// wait for permission to log in
				var start = new byte[2];
				using(var ns = new NetworkStream(socket, false))
				{
					var buffer = new BufferedStream(ns);
					try
					{
						buffer.BlockRead(start);
					}
					catch(EndOfStreamException)
					{
						throw new IPBannedException();
					}
				}
				if(!socket.IsActive())
					throw new IPBannedException();

				if(start[0] == 0xAF && start[1] == 0x01)
				{
					using(var packet = new Startup(version, serverHash, serverToken, charInfo))
						Send(packet);
				}
			}
			catch(Exception e)
			{
				OnException("D2GS", e);
			}
		}

		public override void OnLogEvent(string s, string m) { base.OnLogEvent("D2GS", m); }
		public override void OnException(string s, Exception e) { base.OnException("D2GS", e); }
		public override void OnSentPacket(string s, Packet p) { base.OnSentPacket("D2GS", p); }
		public override void OnReceivedPacket(string s, Packet p) { base.OnReceivedPacket("D2GS", p); }

		protected override void FirePacket(Packet packet)
		{
			Type t = packet.GetType();
			if(events.ContainsKey(t))
				events[t].DynamicInvoke(this, new PacketEventArgs(packet));
		}
	}
}
