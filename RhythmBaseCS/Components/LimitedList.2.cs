using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.CompilerServices;

namespace RhythmBase.Components
{
	/// <summary>
	/// A list that limits the capacity of the list and uses the default value to fill the free capacity.
	/// </summary>
	/// <param name="count">Capacity limit.</param>
	/// <param name="defaultValue">Default value.</param>
	public class LimitedList<TEvent>(uint count, TEvent? defaultValue) : LimitedList(count, defaultValue), ICollection<TEvent>
	{
		/// <summary>
		/// Default value
		/// </summary>
		[JsonIgnore]
		public new TEvent? DefaultValue
		{
			get => (TEvent?)base.DefaultValue;
			set => base.DefaultValue = value;
		}
		public TEvent? this[int index]
		{
			get
			{
				if (index >= list.Count)
					throw new IndexOutOfRangeException();
				TEvent? Item;
				if (list[index].isDefault)
				{
					TEvent? ValueCloned = DefaultValue;
					list[index] = (ValueCloned, false);
					Item = ValueCloned;
				}
				else
					Item = Conversions.ToGenericParameter<TEvent?>(list[index].value);
				return Item;
			}
			set
			{
				if (index >= list.Count)
					throw new IndexOutOfRangeException();
				list[index] = (value, false);
			}
		}
		public bool IsReadOnly { get; } = false;

		/// <param name="count">Capacity limit.</param>
		public LimitedList(uint count) : this(count, default)
		{
		}
		public new IEnumerator<TEvent> GetEnumerator()
		{
			foreach (ValueTuple<object, bool> i in list)
			{
				yield return Conversions.ToGenericParameter<TEvent?>(i.Equals(RuntimeHelpers.GetObjectValue(GetDefaultValue())) ? DefaultValue : i.Item1);
			}
			yield break;
		}
		IEnumerator IEnumerable.GetEnumerator() => base.GetEnumerator();
		public void Add(TEvent item) => base.Add(item);
		public new void Clear() => base.Clear();
		public bool Contains(TEvent item) => list.Contains(new ValueTuple<object, bool>(item, true));
		public void CopyTo(TEvent[] array, int arrayIndex) => base.CopyTo(array, arrayIndex);
		public bool Remove(TEvent item) => throw new NotImplementedException();
		public override string ToString() => string.Format("Count = {0}", Count);
	}
}
