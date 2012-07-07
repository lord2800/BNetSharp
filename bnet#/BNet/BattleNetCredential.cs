using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using BNSharp.MBNCSUtil;

namespace BNet
{
	[TypeConverter(typeof(BattleNetCredentialExpandableObjectConverter))]
	public sealed class BattleNetCredential : ICredentials
	{
		private readonly List<CDKey> keys = new List<CDKey>();
		public BattleNetCredential() { }

		public BattleNetCredential(string user, string pass, string owner, params CDKey[] keys)
		{
			UserName = user;
			Password = pass;
			OwnerName = owner;
			this.keys.AddRange(keys);
		}

		[DisplayName("Login Name"), Description("The username with which to log into Battle.net")]
		public string UserName { get; set; }

		[DisplayName("Password"), Description("The password to the specified Login Name")]
		public string Password { get; set; }

		[Browsable(false)]
		public string OwnerName { get; set; }

		[Browsable(false)]
		public ReadOnlyCollection<CDKey> CdKeys
		{
			get { return keys.AsReadOnly(); }
		}

		public NetworkCredential GetCredential(Uri uri, string authType) { return null; }

		public void SetKeys(params CDKey[] keysToSet)
		{
			keys.Clear();
			keys.AddRange(keysToSet);
		}

		public byte[] GetPasswordHash(uint clientToken, uint serverToken) { return GetHash(Password, clientToken, serverToken); }

		public static byte[] GetHash(string value, uint clientToken, uint serverToken)
		{
			using(var ms = new MemoryStream(28))
			{
				var bw = new BinaryWriter(ms);
				var firstHash = XSha1.Hash(Encoding.ASCII.GetBytes(value));
				bw.Write(clientToken);
				bw.Write(serverToken);
				bw.Write(firstHash);
				var buffer = new byte[bw.BaseStream.Length];
				bw.Seek(0, SeekOrigin.Begin);
				bw.BaseStream.Read(buffer, 0, (int)bw.BaseStream.Length);
				return XSha1.Hash(buffer);
			}
		}

		public bool Validate() { return !String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password); }
	}

	public sealed class BattleNetCredentialExpandableObjectConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if(destinationType == typeof(BattleNetCredential))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if(destinationType == typeof(String) && value is BattleNetCredential)
			{
				var cred = value as BattleNetCredential;
				return "{UserName = \"" + cred.UserName + "\", Password = \"" + cred.Password + "\"}";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
