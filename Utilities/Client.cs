using System.Reflection;

namespace Utilities
{
	using System;
	using System.Net.Sockets;
	using System.Threading;
	using System.IO;
	using System.Net;

	public abstract class Client<T> : Loggable, IDisposable where T : Attribute, IPacketHandler
	{
		protected PacketBuilder<T> builder;
		private bool disposed;
		protected Thread recv;
		protected ProxySocket socket;
		protected IPEndPoint Host { get; set; }

		public bool Connected { get; private set; }

		public WebProxy Proxy { get; set; }

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		protected virtual void Dispose(bool disposing)
		{
			if(disposing && !disposed)
			{
				if(socket != null)
				{
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
				}

				disposed = true;
			}
		}

		public virtual void Connect()
		{
			if(Connected)
				throw new InvalidOperationException("Can't connect while already connected!");

			try
			{
				socket = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				lock(socket)
				{
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

					Connected = true;

					recv = new Thread(Receive) {IsBackground = true, Name = GetType().Name + " Receive Thread"};
					recv.Start();
				}
			}
			catch(Exception e)
			{
				OnException("BaseClient", e);
			}
		}

		public virtual void Disconnect()
		{
			lock(socket)
			{
				if(Connected)
				{
					Connected = false;
					recv.Join(1500);

					socket.Shutdown(SocketShutdown.Both);
					socket.Disconnect(true);
					socket.Close();
				}
			}
		}

		public void Send(Packet packet)
		{
			if(Connected)
			{
				lock(socket)
				{
					try
					{
						if(socket.IsActive())
						{
							using(var stream = new NetworkStream(socket, false))
							{
								byte[] p = packet.ToByteArray();
								stream.Write(p, 0, p.Length);
								stream.Flush();
								OnSentPacket("BaseClient", packet);
							}
						}
					}
					catch(IOException e)
					{
						OnException("BaseClient", e);
					}
				}
			}
		}

		protected void Receive()
		{
			if(builder == null)
				throw new NotSupportedException("There must be a packet builder in order to receive packets");

			try
			{
				using(var stream = new NetworkStream(socket, false))
				{
					var bs = new BufferedStream(stream);
					while(Connected)
					{
						var p = builder.Parse(bs);
						if(p == null) continue;
						ThreadPool.QueueUserWorkItem(state => {
							try { FirePacket(p); }
							catch(TargetInvocationException e) { OnException("BaseClient", e.InnerException); }
						});
						OnReceivedPacket("BaseClient", p);
						Thread.Sleep(20);
					}
				}
			}
			catch(ThreadInterruptedException) {}
			catch(Exception e) { OnException("BaseClient", e); }
		}

		protected virtual void FirePacket(Packet p) { }
	}
}
