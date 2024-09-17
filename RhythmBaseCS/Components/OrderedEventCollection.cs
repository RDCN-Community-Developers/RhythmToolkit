using Newtonsoft.Json;
using RhythmBase.Events;
using System.Collections;
using System.ComponentModel.Design;
namespace RhythmBase.Components
{
	/// <summary>
	/// A collection of events that maintains the sequence of events.
	/// </summary>
	public abstract class OrderedEventCollection : ICollection<IBaseEvent>
	{
		[JsonIgnore]
		public virtual int Count => eventsBeatOrder.Sum((i) => i.Value.Count());
		[JsonIgnore]
		public bool IsReadOnly { get; }
		/// <summary>
		/// Returns the beat of the last event.
		/// </summary>
		/// <returns>The beat of the last event.</returns>
		[JsonIgnore]
		public Beat Length => eventsBeatOrder.LastOrDefault().Value.First().Beat;
		public OrderedEventCollection()
		{
			eventsBeatOrder = [];
			IsReadOnly = false;
		}
		public OrderedEventCollection(IEnumerable<IBaseEvent> items)
		{
			eventsBeatOrder = [];
			IsReadOnly = false;
			foreach (IBaseEvent item in items)
				Add(item);
		}
		public List<IBaseEvent> ConcatAll() => eventsBeatOrder.SelectMany(i => i.Value).ToList();
		public void Add(IBaseEvent item)
		{
			TypedEventCollection<IBaseEvent> list = [];
			if (eventsBeatOrder.TryGetValue(item.Beat, out TypedEventCollection<IBaseEvent>? value))
			{
				list = value;
			}
			else
			{
				eventsBeatOrder.Add(item.Beat, list);
			}
			list.Add(item);
		}
		public void Clear() => eventsBeatOrder.Clear();
		public virtual bool Contains(IBaseEvent item) => eventsBeatOrder.ContainsKey(item.Beat) && eventsBeatOrder[item.Beat].Contains(item);
		public void CopyTo(IBaseEvent[] array, int arrayIndex) => ConcatAll().CopyTo(array, arrayIndex);
		internal bool Remove(IBaseEvent item)
		{
			bool Remove;
			if (Contains(item))
			{
				bool result = eventsBeatOrder[item.Beat].Remove(item);
				if (!eventsBeatOrder[item.Beat].Any())
					eventsBeatOrder.Remove(item.Beat);
				Remove = result;
			}
			else
				Remove = false;
			return Remove;
		}
		public IEnumerator<IBaseEvent> GetEnumerator()
		{
			foreach (KeyValuePair<Beat, TypedEventCollection<IBaseEvent>> pair in eventsBeatOrder)
				foreach (IBaseEvent item in pair.Value)
					yield return item;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (KeyValuePair<Beat, TypedEventCollection<IBaseEvent>> pair in eventsBeatOrder)
				foreach (IBaseEvent item in pair.Value)
					yield return item;
		}
		public override string ToString() => string.Format("Count = {0}", Count);
		bool ICollection<IBaseEvent>.Remove(IBaseEvent item) => throw new NotImplementedException();
		internal SortedDictionary<Beat, TypedEventCollection<IBaseEvent>> eventsBeatOrder;
	}
}
