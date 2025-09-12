using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;

namespace RhythmBase.RhythmDoctor.Components
{
	internal class EventEnumerator { }
	internal class EventEnumerator<TEvent> : EventEnumerator, IEventEnumerable<TEvent>, IEnumerator<TEvent> where TEvent : IBaseEvent
	{
		protected readonly IEnumerator<RDBeat> beats;
		protected IEnumerator<IBaseEvent>? events;
		protected readonly OrderedEventCollection collection;
		private EventType[] types;
		private RDRange range;
		public EventEnumerator(OrderedEventCollection collection) : this(collection, RDRange.Infinite) { }
		public EventEnumerator(OrderedEventCollection collection, RDRange range) : this(collection, EventTypeUtils.ToEnums(typeof(TEvent)), range) { }
		public EventEnumerator(OrderedEventCollection collection, EventType[] types, RDRange range)
		{
			//collection._modifierInstances[this] = [];
			this.collection = collection;
			beats = collection.eventsBeatOrder.Keys.GetEnumerator();
			this.types = types;
			this.range = range;
			while (beats.MoveNext())
				if ((range.Start is null || beats.Current >= range.Start) && collection.eventsBeatOrder[beats.Current]._types.Any(types.Contains))
				{
					if (range.End is null || beats.Current < range.End)
						events = collection.eventsBeatOrder[beats.Current].GetEnumerator();
					break;
				}
		}
		public TEvent Current => (TEvent)(events?.Current ?? throw new InvalidOperationException());
		object IEnumerator.Current => Current;
		public bool MoveNext()
		{
			//collection._currentModifier = this;
			bool result;
			while (result = (events?.MoveNext() ?? false))
				if (types.Contains(events!.Current.Type))
					return true;
			if (!result)
			{
				while (beats.MoveNext())
				{
					if (!collection.eventsBeatOrder[beats.Current]._types.Any(types.Contains))
					{
						continue;
					}
					if (range.End is not null && range.End <= beats.Current)
						return false;
					events = collection.eventsBeatOrder[beats.Current].GetEnumerator();
					while (events.MoveNext())
					{
						if (types.Contains(events.Current.Type))
							return true;
					}
				}
			}
			return result;
		}
		public void Dispose()
		{
			//if (collection._currentModifier == this)
			//{
			//	collection._currentModifier = null;
			//	var values = collection._modifierInstances[this];
			//	while (values.Count > 0)
			//	{
			//		(IBaseEvent e, RDBeat b) = values.Dequeue();
			//		e.Beat = b;
			//	}
			//	collection._modifierInstances.Remove(this);
			//}
			//else
			//{
			//	if (collection._modifierInstances[this].Count > 0)
			//		throw new InvalidOperationException("Cannot dispose an enumerator while there are modifying events in the queue.");
			//}
		}
		public void Reset() => throw new NotSupportedException();
		public EventEnumerator<TEvent2> OfEvent<TEvent2>() where TEvent2 : IBaseEvent
		{
			EventType[] types = [.. this.types.Intersect(EventTypeUtils.ToEnums(typeof(TEvent2)))];
			this.types = types;
			return this as EventEnumerator<TEvent2> ?? new(collection, types, range);
		}
		public EventEnumerator<TEvent> InRange(RDRange range)
		{
			this.range = this.range.Intersect(range);
			return this;
		}

		public IEnumerator<TEvent> GetEnumerator() => this;
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
