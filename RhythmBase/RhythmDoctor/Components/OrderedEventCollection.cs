using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// A collection of events that maintains the sequence of events.
	/// </summary>
	public abstract class OrderedEventCollection : ICollection<IBaseEvent>, IEventEnumerable<IBaseEvent>
	{
		/// <summary>
		/// Gets the total count of events in the collection.
		/// </summary>
		public virtual int Count => eventsBeatOrder.Sum(i => i.Value.Count);
		/// <summary>
		/// Gets a value indicating whether the collection is read-only.
		/// </summary>
		public bool IsReadOnly { get; }
		/// <summary>
		/// Returns the beat of the last event.
		/// </summary>
		/// <returns>The beat of the last event.</returns>
		public RDBeat Length => eventsBeatOrder.LastOrDefault().Key;
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
		/// Adds an event to the collection.
		/// </summary>
		/// <param name="item">The event to add.</param>
		public bool Add(IBaseEvent item)
		{
			if (eventsBeatOrder.TryGetValue(item.Beat, out TypedEventCollection<IBaseEvent>? value))
				return value.Add(item);
			eventsBeatOrder.Insert(item.Beat, [item]);
			return true;
		}
		void ICollection<IBaseEvent>.Add(IBaseEvent item) => Add(item);
		/// <summary>
		/// Clears all events from the collection.
		/// </summary>
		public void Clear() => eventsBeatOrder.Clear();
		/// <summary>
		/// Determines whether the collection contains a specific event.
		/// </summary>
		/// <param name="item">The event to locate in the collection.</param>
		/// <returns>true if the event is found in the collection; otherwise, false.</returns>
		public virtual bool Contains(IBaseEvent item) => eventsBeatOrder.FindNode(item.Beat)?.Value.Contains(item) ?? false;
		/// <summary>
		/// Copies the elements of the collection to an array, starting at a particular array index.
		/// </summary>
		/// <param name="array">The array to copy the elements to.</param>
		/// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
		public void CopyTo(IBaseEvent[] array, int arrayIndex)
		{
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
		public virtual IEnumerator<IBaseEvent> GetEnumerator() => new EventEnumerator<IBaseEvent>(this);
		internal IEnumerator<TEvent> GetEnumerator<TEvent>(float? start, float? end) where TEvent : IBaseEvent => new EventEnumerator<TEvent>(this, new RDRange(
			start is null ? null : new(start.Value),
			end is null ? null : new(end.Value)));
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An enumerator for the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
		//internal SortedDictionary<RDBeat, TypedEventCollection<IBaseEvent>> eventsBeatOrder;
		internal RedBlackTree<RDBeat, TypedEventCollection<IBaseEvent>> eventsBeatOrder = new();
		internal Dictionary<EventEnumerator, Queue<(IBaseEvent e, RDBeat b)>> _modifierInstances = [];
		//internal EventEnumerator? _currentModifier;
		internal BeatCalculator? calculator;
	}
}
