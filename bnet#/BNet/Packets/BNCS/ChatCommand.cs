using System;

namespace BNet.BNCS.Outgoing
{
	public sealed class ChatCommand : BNCSPacket
	{
		public ChatCommand(string command) : base(BNCSPacketId.ChatCommand)
		{
			if(command == null)
				throw new ArgumentNullException("command");
			if(command.Length > 223)
				throw new ArgumentException("Command is too long, max command length is 223", "command");

			Write(command);
		}
	}
}
