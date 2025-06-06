using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;

namespace RhythmBase.RhythmDoctor.Components
{
	internal class EventEnumerator : IEnumerator<IBaseEvent>
	{
		protected readonly IEnumerator<RDBeat> beats;
		protected IEnumerator<IBaseEvent>? events;
		protected readonly OrderedEventCollection collection;
		public EventEnumerator(OrderedEventCollection collection)
		{
			collection._modifierInstances[this] = [];
			this.collection = collection;
			beats = collection.eventsBeatOrder.Keys.GetEnumerator();
		}
		public IBaseEvent Current => events!.Current;
		object IEnumerator.Current => Current;
		public void Dispose()
		{
			if (collection._currentModifier == this)
			{
				collection._currentModifier = null;
				var values = collection._modifierInstances[this];
				while (values.Count > 0)
				{
					(IBaseEvent e, RDBeat b) = values.Dequeue();
					e.Beat = b;
				}
				collection._modifierInstances.Remove(this);
			}
			else
			{
				if (collection._modifierInstances[this].Count > 0)
					throw new InvalidOperationException("Cannot dispose an enumerator while there are modifying events in the queue.");
			}
		}
		public virtual bool MoveNext()
		{
			collection._currentModifier = this;
			bool result = events?.MoveNext() ?? false;
			if (!result)
			{
				while (beats.MoveNext())
				{
					events = collection.eventsBeatOrder[beats.Current].GetEnumerator();
					if (events?.MoveNext() ?? false)
						return true;
				}
			}
			return result;
		}
		public void Reset()
		{
			throw new NotSupportedException();
		}
	}
	internal class EventEnumerator<TEvent> : EventEnumerator, IEnumerator<TEvent> where TEvent : IBaseEvent
	{
		internal float? enumerateStart;
		internal float? enumerateEnd;
		private readonly EventType[] types;
		public EventEnumerator(OrderedEventCollection collection, float? start, float? end) : base(collection)
		{
			types = EventTypeUtils.ToEnums(typeof(TEvent));
			enumerateStart = start;
			enumerateEnd = end;
			while (beats.MoveNext())
				if (start == null || beats.Current.BeatOnly >= start)
				{
					if (beats.Current.BeatOnly < end)
						events = collection.eventsBeatOrder[beats.Current].GetEnumerator();
					break;
				}
		}
		public new TEvent Current => (TEvent)base.Current;
		public override bool MoveNext()
		{
			collection._currentModifier = this;
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
					if (enumerateEnd is not null && enumerateEnd <= beats.Current.BeatOnly)
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
	}
}
