using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
	public sealed class DataManager
	{
		private static Dictionary<string, ITableRow[]> cache = new Dictionary<string, ITableRow[]>();

		public static T[] LoadAs<T>(string file)
		{
			var rows = LoadFrom(file);
			return rows.Cast<T>().ToArray();
		}
		public static ITableRow[] LoadFrom(string file)
		{
			if(cache.ContainsKey(file))
				return cache[file] as ITableRow[];

			using(var f = File.OpenRead(file))
			{
				var bf = new BinaryFormatter();
				ITableRow[] rows = (ITableRow[])bf.Deserialize(f);
				cache[file] = rows;
				return rows;
			}
		}
		public static void SaveTo<T>(string file, T[] values)
		{
			using(var f = File.OpenWrite(file))
			{
				var bf = new BinaryFormatter();
				bf.Serialize(f, values);
				cache[file] = values as ITableRow[];
			}
		}

		public static T[] Parse<T>(StreamReader file, params char[] separators) where T : ITableRow, new()
		{
			var cols = file.ReadLine().Split(separators);
			var line = file.ReadLine();
			var rows = new List<T>();
			while(line != null)
			{
				var values = line.Split(separators);
				if(values.Length != cols.Length) continue;

				T row = new T();
				rows.Add(row);
				foreach(var prop in row.GetType().GetProperties())
					ParseProperty(row, prop, "", cols, values);

				line = file.ReadLine();
			}
			return rows.ToArray();
		}

		private static void ParseProperty<T>(T obj, PropertyInfo prop, object preparse, string[] cols, string[] values)
		{
			bool hasCol = prop.HasAttribute(typeof(ColumnAttribute)),
			     hasColId = prop.HasAttribute(typeof(ColumnIdAttribute));
			if(!hasCol && !hasColId)
				return;

			var ptype = prop.PropertyType;
			if(hasCol)
			{
				var attr = prop.GetAttribute<ColumnAttribute>();
				var index = -1;
				if(!String.IsNullOrEmpty(attr.Name))
				{
					var name = String.Format(attr.Name, preparse);
					index = Array.FindIndex(cols, (col) => col == name);
				}
				else { index = attr.Index + (preparse.Equals("") ? 0 : (int)preparse); }

				if(index != -1)
				{
					if(String.IsNullOrEmpty(values[index]))
					{
						if(ptype == typeof(string)) prop.SetValue(obj, "", null);
						else prop.SetValue(obj, ptype.IsValueType ? Activator.CreateInstance(ptype) : null, null);
					}
					else prop.SetValue(obj, ptype.CastTo(values[index]), null);
				}
			}
			else if(hasColId)
			{
				var attr = prop.GetAttribute<ColumnIdAttribute>();
				foreach(var p in ptype.GetProperties())
					if(!String.IsNullOrEmpty(attr.Compose))
						ParseProperty(prop.GetValue(obj, null), p, attr.Compose, cols, values);
					else
						ParseProperty(prop.GetValue(obj, null), p, attr.BaseId, cols, values);
			}
		}
	}

	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class ColumnAttribute : Attribute
	{
		public ColumnAttribute(string name) { Name = name; }
		public ColumnAttribute(int index) { Index = index; }
		public int Index { get; private set; }
		public string Name { get; private set; }
	}

	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class ColumnIdAttribute : Attribute
	{
		public ColumnIdAttribute(string compose) { Compose = compose; }
		public ColumnIdAttribute(int baseId) { BaseId = baseId; }
		public int BaseId { get; private set; }
		public string Compose { get; private set; }
	}

	public interface ITableRow {}
}
