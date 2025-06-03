using RhythmBase.RhythmDoctor.Events;
using System.Collections;

namespace RhythmBase.RhythmDoctor.Components
{
	internal class EventEnumerator : IEnumerator<IBaseEvent>
	{
		private IEnumerator<RDBeat> beats;
		private IEnumerator<IBaseEvent>? events;
		private OrderedEventCollection collection;
		public EventEnumerator(OrderedEventCollection collection)
		{
			collection._isEnumerating = true;
			this.collection = collection;
			beats = collection.eventsBeatOrder.Keys.GetEnumerator();
		}
		public IBaseEvent Current => events!.Current;
		object IEnumerator.Current => Current;
		public void Dispose()
		{
			collection._isEnumerating = false;
			while (collection._modifyingEvents.Count > 0)
			{
				(IBaseEvent e, RDBeat b) = collection._modifyingEvents.Dequeue();
				e.Beat = b;
			}
		}
		public bool MoveNext()
		{
			bool result = events?.MoveNext() ?? false;
			if (!result)
			{
				while (result = beats.MoveNext())
				{
					events = collection.eventsBeatOrder[beats.Current].GetEnumerator();
					if (events.MoveNext())
						break;
				}
				return result;
			}
			return result;
		}
		public void Reset()
		{
			throw new NotSupportedException();
		}
	}
}
