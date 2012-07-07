using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Utilities
{
	public sealed class Rotator<T> where T : class
	{
		private readonly object syncObj = new object();

		private readonly List<T> activeItems = new List<T>();
		private readonly List<T> inactiveItems = new List<T>();
		private readonly Random rng = new Random();
		private readonly Dictionary<T, DateTime> touchedItems = new Dictionary<T, DateTime>();
		private readonly AutoResetEvent wait = new AutoResetEvent(false);

		public Rotator(IEnumerable<T> source) : this(source, TimeSpan.FromHours(1)) { }

		public Rotator(IEnumerable<T> source, TimeSpan timeoutDuration)
		{
			inactiveItems.AddRange(source);
			TimeoutDuration = timeoutDuration;
		}

		public int Count
		{
			get { return activeItems.Count + inactiveItems.Count + touchedItems.Count; }
		}

		public bool Available
		{
			get { return inactiveItems.Count > 0; }
		}

		private TimeSpan TimeoutDuration { get; set; }

		public void AddItem(T item)
		{
			if(item == null)
				throw new ArgumentNullException("item");

			lock(syncObj)
				inactiveItems.Add(item);
		}

		public void RemoveItem(T item)
		{
			if(item == null)
				throw new ArgumentNullException("item");

			lock(syncObj)
			{
				if(!inactiveItems.Contains(item))
					throw new ArgumentException("The specified item must be inactive before you can remove it");

				inactiveItems.Remove(item);
			}
		}

		public void Cycle()
		{
			var cycled = (from entry in touchedItems
			              where (DateTime.Now - entry.Value) >= TimeoutDuration
			              select entry.Key).ToList();

			lock(syncObj)
			{
				foreach(var item in cycled)
				{
					touchedItems.Remove(item);
					inactiveItems.Add(item);
				}

				if(cycled.Count > 0)
					wait.Set();
			}
		}

		public T GetNext()
		{
			Cycle();

			if(inactiveItems.Count == 0)
				wait.WaitOne();

			lock(syncObj)
			{
				var which = rng.Next(inactiveItems.Count);
				var item = inactiveItems[which];
				inactiveItems.Remove(item);
				activeItems.Add(item);
				return item;
			}
		}

		public void Release(T item)
		{
			if(!activeItems.Contains(item))
				throw new ArgumentException("The specified item must be active in order to release it");

			lock(syncObj)
			{
				activeItems.Remove(item);
				touchedItems.Add(item, DateTime.Now);
			}
		}
	}
}
