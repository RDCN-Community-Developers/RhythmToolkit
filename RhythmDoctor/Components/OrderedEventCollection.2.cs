using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>  
	/// Represents a collection of ordered events.  
	/// </summary>  
	/// <typeparam name="TEvent">The type of event.</typeparam>  
	public class OrderedEventCollection<TEvent> : OrderedEventCollection, ICollection<TEvent>, IEventEnumerable<TEvent> where TEvent : IBaseEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="OrderedEventCollection{TEvent}"/> class.  
		/// </summary>  
		public OrderedEventCollection()
		{
		}
		/// <summary>  
		/// Initializes a new instance of the <see cref="OrderedEventCollection{TEvent}"/> class with the specified items.  
		/// </summary>  
		/// <param name="items">The items to add to the collection.</param>  
		public OrderedEventCollection(IEnumerable<TEvent> items)
		{
			foreach (TEvent item in items)
				Add(item);
		}
		/// <summary>  
		/// Concatenates all events in the collection.  
		/// </summary>  
		/// <returns>An <see cref="IEnumerable{TEvent}"/> that contains all events in the collection.</returns>  
		public new IEnumerable<TEvent> ConcatAll() => eventsBeatOrder.SelectMany(i => i.Value).Cast<TEvent>();
		/// <summary>  
		/// Adds an event to the collection.  
		/// </summary>  
		/// <param name="item">The event to add.</param>  
		public virtual void Add(TEvent item) => Add((IBaseEvent)(object)item);
		/// <inheritdoc/>  
		public virtual bool Contains(TEvent item) => Contains((IBaseEvent)(object)item);
		/// <inheritdoc/>  
		public void CopyTo(TEvent[] array, int arrayIndex) => CopyTo((IBaseEvent[])(object)array, arrayIndex);
		/// <inheritdoc/>  
		public virtual bool Remove(TEvent item) => Remove((BaseEvent)(object)item);
		/// <inheritdoc/>  
		public override string ToString() => string.Format("Count = {0}", Count);
		/// <inheritdoc/>  
		IEnumerator<TEvent> IEnumerable<TEvent>.GetEnumerator()
		{
			foreach (KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> pair in eventsBeatOrder)
				foreach (TEvent item in pair.Value.Select(v => (TEvent)v))
					yield return item;
		}
	}
}
