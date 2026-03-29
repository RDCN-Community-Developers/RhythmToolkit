using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;

namespace RhythmBase.RhythmDoctor.Components;

internal class EventEnumerator { }
internal class EventEnumerator<TEvent>(OrderedEventCollection collection, ReadOnlyEnumCollection<EventType> types, RDRange range) : EventEnumerator, IEventEnumerable<TEvent>, IEnumerator<TEvent> where TEvent : IBaseEvent
{
	protected readonly IEnumerator<KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>>> beats = collection.eventsBeatOrder.GetEnumerator();
	protected IEnumerator<IBaseEvent>? events;
	protected readonly OrderedEventCollection collection = collection;
	private ReadOnlyEnumCollection<EventType> types = types;
	private RDRange range = range;
	public EventEnumerator(OrderedEventCollection collection) : this(collection, RDRange.Infinity) { }
	public EventEnumerator(OrderedEventCollection collection, RDRange range) : this(collection, EventTypeUtils.ToEnums(typeof(TEvent)), range) { }

	public TEvent Current => (TEvent)(events?.Current ?? throw new InvalidOperationException());
	object IEnumerator.Current => Current;
    public bool MoveNext()
    {
        if (events != null)
        {
            while (events.MoveNext())
            {
                if (types.Contains(events.Current.Type))
                    return true;
            }
            events = null;
        }
        while (beats.MoveNext())
        {
            var currentKey = beats.Current.Key;

            if (range.Start.HasValue && currentKey < range.Start.Value)
                continue;
            if (range.End.HasValue && currentKey >= range.End.Value)
                return false;
            if (!beats.Current.Value.ContainsTypes(types))
                continue;
            events = beats.Current.Value.GetEnumerator();
            while (events.MoveNext())
            {
                if (types.Contains(events.Current.Type))
                    return true;
            }
            events = null;
        }
        return false;
    }
    public void Dispose()
	{
	}
	public void Reset() => throw new NotSupportedException();
	public IEventEnumerable<TEvent2> OfEvent<TEvent2>() where TEvent2 : IBaseEvent
	{
		ReadOnlyEnumCollection<EventType> types = this.types.Intersect(EventTypeUtils.ToEnums(typeof(TEvent2)));
		this.types = types;
		return this as EventEnumerator<TEvent2> ?? new(collection, types, range);
	}
	public IEventEnumerable<IBaseEvent> OfEvents(ReadOnlyEnumCollection<EventType> types)
	{
		this.types = types;
		return this as EventEnumerator<IBaseEvent> ?? new(collection, types, range);
	}
	public EventEnumerator<TEvent> InRange(RDRange range)
	{
		this.range = this.range.Intersect(range);
		return this;
	}
	public IEnumerable<TEvent> AtBeat(RDBeat beat)
	{
		if (!range.Contains(beat))
			yield break;
		if (!collection.eventsBeatOrder.TryGetValue(beat, out var events))
			yield break;
		if (!events.ContainsTypes(types))
			yield break;
		foreach (var ev in events)
			if (types.Contains(ev.Type))
				yield return (TEvent)ev;
	}
	public IEnumerator<TEvent> GetEnumerator() => this;
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
