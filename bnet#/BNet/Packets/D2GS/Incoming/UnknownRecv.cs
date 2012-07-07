namespace BNet.D2GS.Incoming
{
	[PacketLength(Length = 26)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv12)]
	public sealed class Recv12 : D2GSPacket
	{
		public Recv12(byte[] bytes) : base(D2GSIncomingPacketId.Recv12) { SeedBytes(bytes); }
		public static Recv12 Parse(byte[] bytes) { return new Recv12(bytes); }
	}

	[PacketLength(Length = 14)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv13)]
	public sealed class Recv13 : D2GSPacket
	{
		public Recv13(byte[] bytes) : base(D2GSIncomingPacketId.Recv13) { SeedBytes(bytes); }
		public static Recv13 Parse(byte[] bytes) { return new Recv13(bytes); }
	}

	[PacketLength(Length = 18)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv14)]
	public sealed class Recv14 : D2GSPacket
	{
		public Recv14(byte[] bytes) : base(D2GSIncomingPacketId.Recv14) { SeedBytes(bytes); }
		public static Recv14 Parse(byte[] bytes) { return new Recv14(bytes); }
	}

	[PacketLength(Length = 15)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv18)]
	public sealed class Recv18 : D2GSPacket
	{
		public Recv18(byte[] bytes) : base(D2GSIncomingPacketId.Recv18) { SeedBytes(bytes); }
		public static Recv18 Parse(byte[] bytes) { return new Recv18(bytes); }
	}

	[PacketLength(Length = 90)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv24)]
	public sealed class Recv24 : D2GSPacket
	{
		public Recv24(byte[] bytes) : base(D2GSIncomingPacketId.Recv24) { SeedBytes(bytes); }
		public static Recv24 Parse(byte[] bytes) { return new Recv24(bytes); }
	}


	[PacketLength(Length = 90)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv25)]
	public sealed class Recv25 : D2GSPacket
	{
		public Recv25(byte[] bytes) : base(D2GSIncomingPacketId.Recv25) { SeedBytes(bytes); }
		public static Recv25 Parse(byte[] bytes) { return new Recv25(bytes); }
	}

	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv40)]
	public sealed class Recv40 : D2GSPacket
	{
		public Recv40(byte[] bytes) : base(D2GSIncomingPacketId.Recv40) { SeedBytes(bytes); }
		public static Recv40 Parse(byte[] bytes) { return new Recv40(bytes); }
	}

	[PacketLength(Length = 13)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv45)]
	public sealed class Recv45 : D2GSPacket
	{
		public Recv45(byte[] bytes) : base(D2GSIncomingPacketId.Recv45) { SeedBytes(bytes); }
		public static Recv45 Parse(byte[] bytes) { return new Recv45(bytes); }
	}

	[PacketLength(Length = 15)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv50)]
	public sealed class Recv50 : D2GSPacket
	{
		public Recv50(byte[] bytes) : base(D2GSIncomingPacketId.Recv50) { SeedBytes(bytes); }
		public static Recv50 Parse(byte[] bytes) { return new Recv50(bytes); }
	}

	[PacketLength(Length = 10)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv54)]
	public sealed class Recv54 : D2GSPacket
	{
		public Recv54(byte[] bytes) : base(D2GSIncomingPacketId.Recv54) { SeedBytes(bytes); }
		public static Recv54 Parse(byte[] bytes) { return new Recv54(bytes); }
	}

	[PacketLength(Length = 3)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv55)]
	public sealed class Recv55 : D2GSPacket
	{
		public Recv55(byte[] bytes) : base(D2GSIncomingPacketId.Recv55) { SeedBytes(bytes); }
		public static Recv55 Parse(byte[] bytes) { return new Recv55(bytes); }
	}

	[PacketLength(Length = 14)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv58)]
	public sealed class Recv58 : D2GSPacket
	{
		public Recv58(byte[] bytes) : base(D2GSIncomingPacketId.Recv58) { SeedBytes(bytes); }
		public static Recv58 Parse(byte[] bytes) { return new Recv58(bytes); }
	}

	[PacketLength(Length = 38)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv5E)]
	public sealed class Recv5E : D2GSPacket
	{
		public Recv5E(byte[] bytes) : base(D2GSIncomingPacketId.Recv5E) { SeedBytes(bytes); }
		public static Recv5E Parse(byte[] bytes) { return new Recv5E(bytes); }
	}

	[PacketLength(Length = 5)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv5F)]
	public sealed class Recv5F : D2GSPacket
	{
		public Recv5F(byte[] bytes) : base(D2GSIncomingPacketId.Recv5F) { SeedBytes(bytes); }
		public static Recv5F Parse(byte[] bytes) { return new Recv5F(bytes); }
	}

	[PacketLength(Length = 2)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv61)]
	public sealed class Recv61 : D2GSPacket
	{
		public Recv61(byte[] bytes) : base(D2GSIncomingPacketId.Recv61) { SeedBytes(bytes); }
		public static Recv61 Parse(byte[] bytes) { return new Recv61(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv62)]
	public sealed class Recv62 : D2GSPacket
	{
		public Recv62(byte[] bytes) : base(D2GSIncomingPacketId.Recv62) { SeedBytes(bytes); }
		public static Recv62 Parse(byte[] bytes) { return new Recv62(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv66)]
	public sealed class Recv66 : D2GSPacket
	{
		public Recv66(byte[] bytes) : base(D2GSIncomingPacketId.Recv66) { SeedBytes(bytes); }
		public static Recv66 Parse(byte[] bytes) { return new Recv66(bytes); }
	}

	[PacketLength(Length = 12)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv6A)]
	public sealed class Recv6A : D2GSPacket
	{
		public Recv6A(byte[] bytes) : base(D2GSIncomingPacketId.Recv6A) { SeedBytes(bytes); }
		public static Recv6A Parse(byte[] bytes) { return new Recv6A(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv6E)]
	public sealed class Recv6E : D2GSPacket
	{
		public Recv6E(byte[] bytes) : base(D2GSIncomingPacketId.Recv6E) { SeedBytes(bytes); }
		public static Recv6E Parse(byte[] bytes) { return new Recv6E(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv6F)]
	public sealed class Recv6F : D2GSPacket
	{
		public Recv6F(byte[] bytes) : base(D2GSIncomingPacketId.Recv6F) { SeedBytes(bytes); }
		public static Recv6F Parse(byte[] bytes) { return new Recv6F(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv70)]
	public sealed class Recv70 : D2GSPacket
	{
		public Recv70(byte[] bytes) : base(D2GSIncomingPacketId.Recv70) { SeedBytes(bytes); }
		public static Recv70 Parse(byte[] bytes) { return new Recv70(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv71)]
	public sealed class Recv71 : D2GSPacket
	{
		public Recv71(byte[] bytes) : base(D2GSIncomingPacketId.Recv71) { SeedBytes(bytes); }
		public static Recv71 Parse(byte[] bytes) { return new Recv71(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv72)]
	public sealed class Recv72 : D2GSPacket
	{
		public Recv72(byte[] bytes) : base(D2GSIncomingPacketId.Recv72) { SeedBytes(bytes); }
		public static Recv72 Parse(byte[] bytes) { return new Recv72(bytes); }
	}

	[PacketLength(Length = 32)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv73)]
	public sealed class Recv73 : D2GSPacket
	{
		public Recv73(byte[] bytes) : base(D2GSIncomingPacketId.Recv73) { SeedBytes(bytes); }
		public static Recv73 Parse(byte[] bytes) { return new Recv73(bytes); }
	}

	[PacketLength(Length = 5)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv7E)]
	public sealed class Recv7E : D2GSPacket
	{
		public Recv7E(byte[] bytes) : base(D2GSIncomingPacketId.Recv7E) { SeedBytes(bytes); }
		public static Recv7E Parse(byte[] bytes) { return new Recv7E(bytes); }
	}

	[PacketLength(Length = 1)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv86)]
	public sealed class Recv86 : D2GSPacket
	{
		public Recv86(byte[] bytes) : base(D2GSIncomingPacketId.Recv86) { SeedBytes(bytes); }
		public static Recv86 Parse(byte[] bytes) { return new Recv86(bytes); }
	}

	[PacketLength(Length = 26)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv91)]
	public sealed class Recv91 : D2GSPacket
	{
		public Recv91(byte[] bytes) : base(D2GSIncomingPacketId.Recv91) { SeedBytes(bytes); }
		public static Recv91 Parse(byte[] bytes) { return new Recv91(bytes); }
	}

	[PacketLength(Length = 6)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv92)]
	public sealed class Recv92 : D2GSPacket
	{
		public Recv92(byte[] bytes) : base(D2GSIncomingPacketId.Recv92) { SeedBytes(bytes); }
		public static Recv92 Parse(byte[] bytes) { return new Recv92(bytes); }
	}

	[PacketLength(Length = 8)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv93)]
	public sealed class Recv93 : D2GSPacket
	{
		public Recv93(byte[] bytes) : base(D2GSIncomingPacketId.Recv93) { SeedBytes(bytes); }
		public static Recv93 Parse(byte[] bytes) { return new Recv93(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv98)]
	public sealed class Recv98 : D2GSPacket
	{
		public Recv98(byte[] bytes) : base(D2GSIncomingPacketId.Recv98) { SeedBytes(bytes); }
		public static Recv98 Parse(byte[] bytes) { return new Recv98(bytes); }
	}

	[PacketLength(Length = 17)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv9A)]
	public sealed class Recv9A : D2GSPacket
	{
		public Recv9A(byte[] bytes) : base(D2GSIncomingPacketId.Recv9A) { SeedBytes(bytes); }
		public static Recv9A Parse(byte[] bytes) { return new Recv9A(bytes); }
	}

	[PacketLength(Length = 7)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recv9B)]
	public sealed class Recv9B : D2GSPacket
	{
		public Recv9B(byte[] bytes) : base(D2GSIncomingPacketId.Recv9B) { SeedBytes(bytes); }
		public static Recv9B Parse(byte[] bytes) { return new Recv9B(bytes); }
	}

	[PacketLength(Length = 24)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recva3)]
	public sealed class Recva3 : D2GSPacket
	{
		public Recva3(byte[] bytes) : base(D2GSIncomingPacketId.Recva3) { SeedBytes(bytes); }
		public static Recva3 Parse(byte[] bytes) { return new Recva3(bytes); }
	}

	[PacketLength(Length = 3)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recva4)]
	public sealed class Recva4 : D2GSPacket
	{
		public Recva4(byte[] bytes) : base(D2GSIncomingPacketId.Recva4) { SeedBytes(bytes); }
		public static Recva4 Parse(byte[] bytes) { return new Recva4(bytes); }
	}

	[PacketLength(Length = 9)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recvad)]
	public sealed class Recvad : D2GSPacket
	{
		public Recvad(byte[] bytes) : base(D2GSIncomingPacketId.Recvad) { SeedBytes(bytes); }
		public static Recvad Parse(byte[] bytes) { return new Recvad(bytes); }
	}

	[PacketLength(Length = 53)]
	[D2GSPacketHandler(D2GSIncomingPacketId.Recvb1)]
	public sealed class Recvb1 : D2GSPacket
	{
		public Recvb1(byte[] bytes) : base(D2GSIncomingPacketId.Recvb1) { SeedBytes(bytes); }
		public static Recvb1 Parse(byte[] bytes) { return new Recvb1(bytes); }
	}
}
