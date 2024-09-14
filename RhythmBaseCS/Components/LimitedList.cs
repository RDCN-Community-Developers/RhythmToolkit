using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Collections;
using System.Runtime.CompilerServices;

namespace RhythmBase.Components
{
	/// <summary>
	/// A list that limits the capacity of the list and uses the default value to fill the free capacity.
	/// </summary>

	[JsonConverter(typeof(LimitedListConverter))]
	public class LimitedList : ICollection
	{
		/// <summary>
		/// Default value.
		/// </summary>

		[JsonIgnore]
		protected internal object? DefaultValue;
		public int Count => list.Count;
		public bool IsSynchronized { get; }
		public object SyncRoot { get; }
		/// <param name="count">Capacity limit.</param>
		/// <param name="defaultValue">Default value.</param>
		public LimitedList(uint count, object? defaultValue)
		{
			IsSynchronized = false;
			SyncRoot = this;
			list = new List<(object?, bool)>((int)count);
			long num = (long)(unchecked((ulong)count) - 1UL);
			for (int i = 0; i <= num; i++)
				list.Add((GetDefaultValue(), true));
			DefaultValue = defaultValue;
		}
		/// <param name="count">Capacity limit.</param>
		public LimitedList(uint count) : this(count, null) { }
		protected internal void Add(object item)
		{
			int index = list.IndexOf(list.FirstOrDefault(i => i.isDefault));
			if (index >= 0)
				list[index] = new ValueTuple<object, bool>(item, false);
		}
		/// <summary>
		/// Remove all items.
		/// </summary>
		public void Clear()
		{
			int num = list.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				list[i] = default(ValueTuple<object, bool>);
			}
		}
		/// <summary>
		/// Remove item.
		/// </summary>
		/// <param name="index">Item index.</param>
		protected internal void RemoveAt(uint index)
		{
			if (index >= list.Count)
				throw new IndexOutOfRangeException();
			list[(int)index] = default(ValueTuple<object, bool>);
		}
		/// <summary>
		/// Get the default value.
		/// </summary>
		/// <returns>The default value.</returns>
		protected virtual object? GetDefaultValue() => DefaultValue is ValueType ? new() : null;
		public IEnumerator GetEnumerator() => (from i in list
											   select i.Equals(RuntimeHelpers.GetObjectValue(GetDefaultValue())) ? DefaultValue : i.value).GetEnumerator();
		public void CopyTo(Array array, int arrayIndex)
		{
			for (int i = 0; i <= list.Count - 1; i++)
			{
				array.SetValue(list[i].value, arrayIndex + i);
			}
		}
		public override string ToString() => string.Format("Count = {0}", Count);
		protected readonly List<(object? value, bool isDefault)> list;
	}
}
