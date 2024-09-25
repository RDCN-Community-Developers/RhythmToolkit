using RhythmBase.Events;
namespace RhythmBase.Components
{
	public class OrderedEventCollection<TEvent> : OrderedEventCollection, ICollection<TEvent> where TEvent : IBaseEvent
	{
		public OrderedEventCollection()
		{
		}
		public OrderedEventCollection(IEnumerable<TEvent> items)
		{
			foreach (TEvent item in items)
				Add(item);
		}
		public virtual void Add(TEvent item) => Add((IBaseEvent)(object)item);
		/// <inheritdoc/>
		public virtual bool Contains(TEvent item) => Contains((IBaseEvent)(object)item);
		/// <inheritdoc/>
		public void CopyTo(TEvent[] array, int arrayIndex) => CopyTo((IBaseEvent[])(object)array, arrayIndex);
		/// <inheritdoc/>
		public virtual bool Remove(TEvent item) => Remove((BaseEvent)(object)item);
		/// <inheritdoc/>
		public override string ToString() => string.Format("Count = {0}", Count);
		IEnumerator<TEvent> IEnumerable<TEvent>.GetEnumerator()
		{
			foreach (KeyValuePair<Beat, TypedEventCollection<IBaseEvent>> pair in eventsBeatOrder)
				foreach (TEvent item in pair.Value)
					yield return item;
		}
	}
}
