using System;

namespace BNet.BNCS.Incoming
{
	[BNCSPacketHandler(BNCSPacketId.ChatEvent)]
	public sealed class ChatEvent : BNCSPacket
	{
		private ChatEvent(byte[] bytes) : base(BNCSPacketId.ChatEvent)
		{
			SeedBytes(bytes);

			EventID = (ChatEventId)Read<uint>();
			UserFlags = Read<uint>();
			UserPing = Read<uint>();
			Username = Read();
			Message = Read();
		}

		public ChatEventId EventID { get; private set; }
		public uint UserFlags { get; private set; }
		public uint UserPing { get; private set; }
		public string Username { get; private set; }
		public string Message { get; private set; }
		public static ChatEvent Parse(byte[] bytes) { return new ChatEvent(bytes); }
	}
}

namespace BNet.BNCS
{
	[Flags]
	public enum ChatEventId : uint
	{
		ShowUser = 1,
		UserJoined = 2,
		UserLeft = 3,
		Whisper = 4,
		Talk = 5,
		Broadcast = 6,
		ChannelInfo = 7,
		FlagsUpdate = 9,
		WhisperSent = 10,
		ChannelFull = 14,
		ChannelDoesNotExist = 15,
		ChannelRestricted = 16,
		Information = 18,
		Error = 19,
		Emote = 23
	}
}
