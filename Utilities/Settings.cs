using System;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Utilities
{
	public abstract class Settings
	{
		private const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;

		public void Save()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings.Clear();

			var props = GetType().GetProperties();
			foreach(var prop in props)
			{
				var value = prop.GetValue(this, null).ToString();
				config.AppSettings.Settings.Add(prop.Name, value);
			}

			config.Save();
		}

		public void Load()
		{
			var props = GetType().GetProperties();
			foreach(var prop in props)
			{
				var value = ConfigurationManager.AppSettings[prop.Name];

				if(String.IsNullOrEmpty(value))
				{
					// no available value, does the property have a DefaultValueAttribute?
					if(prop.HasAttribute(typeof(DefaultValueAttribute)))
						prop.SetValue(this, prop.GetAttribute<DefaultValueAttribute>().Value, null);
					else
						prop.SetValue(this, null, null);
				}
				else
				{
					prop.SetValue(this, prop.PropertyType.CastTo(value), null);
				}
			}
		}
	}

	public abstract class XmlSchemaClass
	{
		public static T Load<T>() { return Load<T>(typeof(T).GetAttribute<DefaultFileAttribute>().FileName); }
		public static T Load<T>(string file)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using(XmlReader reader = XmlReader.Create(File.OpenRead(file)))
				return (T)serializer.Deserialize(reader);
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class DefaultFileAttribute : Attribute
	{
		public DefaultFileAttribute() {}
		public string FileName { get; set; }
	}
}
