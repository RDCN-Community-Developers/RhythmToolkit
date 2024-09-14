using Newtonsoft.Json;
using RhythmBase.Events;
using System.Collections;
using System.ComponentModel.Design;

namespace RhythmBase.Components
{
	/// <summary>
	/// A collection of events that maintains the sequence of events.
	/// </summary>
	public abstract class OrderedEventCollection : ICollection<BaseEvent>
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
		public OrderedEventCollection(IEnumerable<BaseEvent> items)
		{
			eventsBeatOrder = [];
			IsReadOnly = false;
			foreach (BaseEvent item in items)
				Add(item);
		}
		public List<BaseEvent> ConcatAll() => eventsBeatOrder.SelectMany(i => i.Value).ToList();
		public void Add(BaseEvent item)
		{
			TypedEventCollection<BaseEvent> list=[];
			if (eventsBeatOrder.TryGetValue(item.Beat, out TypedEventCollection<BaseEvent>? value))
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
		public virtual bool Contains(BaseEvent item) => eventsBeatOrder.ContainsKey(item.Beat) && eventsBeatOrder[item.Beat].Contains(item);
		public void CopyTo(BaseEvent[] array, int arrayIndex) => ConcatAll().CopyTo(array, arrayIndex);
		internal bool Remove(BaseEvent item)
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
		public IEnumerator<BaseEvent> GetEnumerator()
		{
			foreach (KeyValuePair<Beat, TypedEventCollection<BaseEvent>> pair in eventsBeatOrder)
				foreach (BaseEvent item in pair.Value)
					yield return item;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (KeyValuePair<Beat, TypedEventCollection<BaseEvent>> pair in eventsBeatOrder)
				foreach (BaseEvent item in pair.Value)
					yield return item;
		}
		public override string ToString() => string.Format("Count = {0}", Count);
		bool ICollection<BaseEvent>.Remove(BaseEvent item) => throw new NotImplementedException();
		internal SortedDictionary<Beat, TypedEventCollection<BaseEvent>> eventsBeatOrder;
	}
}
