using Newtonsoft.Json;
using RhythmBase.Events;
using System.Collections;
namespace RhythmBase.Components
{
	/// <summary>
	/// A collection of events that maintains the sequence of events.
	/// </summary>
	public abstract class OrderedEventCollection : ICollection<IBaseEvent>
	{
		/// <summary>
		/// Gets the total count of events in the collection.
		/// </summary>
		[JsonIgnore]
		public virtual int Count => eventsBeatOrder.Sum((i) => i.Value.Count());

		/// <summary>
		/// Gets a value indicating whether the collection is read-only.
		/// </summary>
		[JsonIgnore]
		public bool IsReadOnly { get; }

		/// <summary>
		/// Returns the beat of the last event.
		/// </summary>
		/// <returns>The beat of the last event.</returns>
		[JsonIgnore]
		public RDBeat Length => eventsBeatOrder.LastOrDefault().Value.FirstOrDefault()?.Beat??new();

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderedEventCollection"/> class.
		/// </summary>
		public OrderedEventCollection()
		{
			eventsBeatOrder = [];
			IsReadOnly = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderedEventCollection"/> class with the specified items.
		/// </summary>
		/// <param name="items">The items to add to the collection.</param>
		public OrderedEventCollection(IEnumerable<IBaseEvent> items)
		{
			eventsBeatOrder = [];
			IsReadOnly = false;
			foreach (IBaseEvent item in items)
				Add(item);
		}

		/// <summary>
		/// Concatenates all events in the collection.
		/// </summary>
		/// <returns>A list of all events in the collection.</returns>
		public IEnumerable<IBaseEvent> ConcatAll() => eventsBeatOrder.SelectMany(i => i.Value).ToList();

		/// <summary>
		/// Adds an event to the collection.
		/// </summary>
		/// <param name="item">The event to add.</param>
		public void Add(IBaseEvent item)
		{
			TypedEventCollection<IBaseEvent> list = [];
			if (eventsBeatOrder.TryGetValue(item.Beat, out TypedEventCollection<IBaseEvent>? value))
				list = value;
			else
				eventsBeatOrder.Add(item.Beat, list);
			list.Add(item);
		}

		/// <summary>
		/// Clears all events from the collection.
		/// </summary>
		public void Clear() => eventsBeatOrder.Clear();

		/// <summary>
		/// Determines whether the collection contains a specific event.
		/// </summary>
		/// <param name="item">The event to locate in the collection.</param>
		/// <returns>true if the event is found in the collection; otherwise, false.</returns>
		public virtual bool Contains(IBaseEvent item) => eventsBeatOrder.ContainsKey(item.Beat) && eventsBeatOrder[item.Beat].Contains(item);

		/// <summary>
		/// Copies the elements of the collection to an array, starting at a particular array index.
		/// </summary>
		/// <param name="array">The array to copy the elements to.</param>
		/// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
		public void CopyTo(IBaseEvent[] array, int arrayIndex)
		{
			ArgumentNullException.ThrowIfNull(array);
			if (arrayIndex < 0 || arrayIndex > array.Length)
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			if (array.Length - arrayIndex < Count)
				throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");

			foreach (var pair in eventsBeatOrder)
			{
				foreach (var item in pair.Value)
				{
					array[arrayIndex++] = item;
				}
			}
		}
		/// <summary>
		/// Removes the first occurrence of a specific event from the collection.
		/// </summary>
		/// <param name="item">The event to remove from the collection.</param>
		/// <returns>true if the event was successfully removed; otherwise, false.</returns>
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

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An enumerator for the collection.</returns>
		public IEnumerator<IBaseEvent> GetEnumerator()
		{
			foreach (KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> pair in eventsBeatOrder)
				foreach (IBaseEvent item in pair.Value)
					yield return item;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An enumerator for the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> pair in eventsBeatOrder)
				foreach (IBaseEvent item in pair.Value)
					yield return item;
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => string.Format("Count = {0}", Count);

		/// <summary>
		/// Removes the first occurrence of a specific event from the collection.
		/// </summary>
		/// <param name="item">The event to remove from the collection.</param>
		/// <returns>true if the event was successfully removed; otherwise, false.</returns>
		bool ICollection<IBaseEvent>.Remove(IBaseEvent item) => throw new NotImplementedException();

		/// <summary>
		/// The dictionary that maintains the order of events based on their beats.
		/// </summary>
		internal SortedDictionary<RDBeat, TypedEventCollection<IBaseEvent>> eventsBeatOrder;
	}
}
