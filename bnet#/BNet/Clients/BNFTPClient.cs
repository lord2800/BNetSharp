using BNet.BNFTP.Incoming;
using BNet.BNFTP.Outgoing;

namespace BNet.BNFTP
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.IO;
	using Utilities;


	public sealed class BNFTPClient : Loggable
	{
		public BNFTPClient(IPEndPoint host) { Host = host; }
		public IPEndPoint Host { get; private set; }
		public WebProxy Proxy { get; set; }

		private void Send(ProxySocket socket, BNFTPv1Packet packet)
		{
			OnSentPacket("BNFTP", packet);
			lock(socket)
			{
				using(var stream = new NetworkStream(socket, false))
				{
					byte[] p = packet;
					stream.Write(p, 0, p.Length);
					stream.Flush();
				}
			}
		}

		public byte[] GetFile(string filename, DateTime filetime)
		{
			try
			{
				OnLogEvent("BNFTP", "Requesting file {0}".Compose(filename));
				using(var socket = Connect())
				{
					byte[] file;

					using(var stream = new NetworkStream(socket, false))
					{
						var bs = new BufferedStream(stream);
						bs.WriteByte(2);
						bs.Flush();

						using(var packet = new RequestFile(filename, filetime))
							Send(socket, packet);

						using(var fi = ReceivePacket(socket))
						{
							file = new byte[fi.FileSize];
							bs.BlockRead(file);
						}
					}

					OnLogEvent("BNFTP", "File {0} received!".Compose(filename));

					Disconnect(socket);
					return file;
				}
			}
			catch(Exception e)
			{
				OnException("BNFTP", e);
			}

			return null;
		}

		private ProxySocket Connect()
		{
			var socket = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			if(Proxy != null)
			{
				socket.ProxyEndPoint = new IPEndPoint(Dns.GetHostAddresses(Proxy.Address.Host)[0], Proxy.Address.Port);
				socket.ProxyType = (ProxyTypes)Enum.Parse(typeof(ProxyTypes), Proxy.Address.Scheme, true);
				if(!String.IsNullOrEmpty(((NetworkCredential)Proxy.Credentials).UserName))
				{
					socket.ProxyUser = ((NetworkCredential)Proxy.Credentials).UserName;
					socket.ProxyPass = ((NetworkCredential)Proxy.Credentials).Password;
				}
			}
			socket.NoDelay = true;
			socket.SendTimeout = 30000;
			socket.ReceiveTimeout = -1;
			socket.SendBufferSize = 255;
			socket.ReceiveBufferSize = 255;
			socket.Connect(Host);
			return socket;
		}

		private void Disconnect(ProxySocket socket)
		{
			lock(socket)
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Disconnect(true);
				socket.Close();
			}
		}

		private ReceiveFile ReceivePacket(ProxySocket socket)
		{
			lock(socket)
			{
				using(var stream = new NetworkStream(socket, false))
				{
					var bs = new BufferedStream(stream);
					var sz = new byte[2];
					bs.BlockRead(sz);
					var size = BitConverter.ToUInt16(sz, 0);
					var buffer = new byte[size - 2];
					bs.BlockRead(buffer);
					var result = ReceiveFile.Parse(buffer);
					OnReceivedPacket("BNFTP", result);
					return result;
				}
			}
		}
	}
}
