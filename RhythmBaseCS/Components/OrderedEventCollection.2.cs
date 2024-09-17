using RhythmBase.Events;
namespace RhythmBase.Components
{
	public class OrderedEventCollection<TEvent> : OrderedEventCollection where TEvent : IBaseEvent
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
		public virtual bool Remove(TEvent item) => Remove((BaseEvent)(object)item);
		public override string ToString() => string.Format("Count = {0}", Count);
	}
}
