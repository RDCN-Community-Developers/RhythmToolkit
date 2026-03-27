using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Extensions
{
    partial class Extensions
    {
        extension(IEventEnumerable<IBaseEvent> source)
        {
            /// <summary>
            /// Filters the event enumerable to only include events of the specified type <typeparamref name="TEvent"/>.
            /// </summary>
            /// <typeparam name="TEvent">The type of event to filter for.</typeparam>
            /// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/>.</returns>
            /// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
            public IEventEnumerable<TEvent> OfEvent<TEvent>() where TEvent : IBaseEvent
            {
                if (source is EventEnumerator<IBaseEvent> casted)
                    return casted.OfEvent<TEvent>();
                return source is OrderedEventCollection ordered
                    ? (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).OfEvent<TEvent>()
                    : throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }
            /// <summary>
            /// Filters the event enumerable to only include events of the specified event types.
            /// </summary>
            /// <param name="types">The event types to filter for.</param>
            /// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events of the specified types.</returns>
            /// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
            public IEventEnumerable<IBaseEvent> OfEvents(params EventType[] types)
            {
                if (source is EventEnumerator<IBaseEvent> casted1)
                    return casted1.OfEvents(types);
                return source is OrderedEventCollection ordered
                    ? (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).OfEvents(types)
                    : throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }

            /// <summary>
            /// Filters the event enumerable to only include events within the specified beat range.
            /// </summary>
            /// <param name="start">The start beat of the range (inclusive).</param>
            /// <param name="end">The end beat of the range (inclusive).</param>
            /// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events within the specified range.</returns>
            /// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
            public IEventEnumerable<IBaseEvent> InRange(RDBeat? start, RDBeat? end)
            {
                if (source is EventEnumerator<IBaseEvent> casted)
                    return casted.InRange(new(start, end));
                return source is OrderedEventCollection ordered
                    ? (IEventEnumerable<IBaseEvent>)((ordered.GetEnumerator() as EventEnumerator<IBaseEvent>)?.InRange(new(start, end)) ?? new EventEnumerator<IBaseEvent>(ordered, new(start, end)))
                    : throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }

            /// <summary>
            /// Filters the event enumerable to only include events within the specified <see cref="RDRange"/>.
            /// </summary>
            /// <param name="range">The beat range to filter for.</param>
            /// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events within the specified range.</returns>
            /// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
            public IEventEnumerable<IBaseEvent> InRange(RDRange range)
            {
                if (source is EventEnumerator<IBaseEvent> casted)
                    return casted.InRange(range);
                return source is OrderedEventCollection ordered
                    ? (IEventEnumerable<IBaseEvent>)(ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).InRange(range)
                    : throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }
            /// <summary>
            /// Filters the event enumerable to only include events of type <typeparamref name="TEvent"/> within the specified beat range.
            /// </summary>
            /// <typeparam name="TEvent">The type of event to filter for.</typeparam>
            /// <param name="start">The start beat of the range (inclusive).</param>
            /// <param name="end">The end beat of the range (inclusive).</param>
            /// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/> within the specified range.</returns>
            /// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{TEvent}"/> is not supported.</exception>
            public IEventEnumerable<TEvent> InRange<TEvent>(RDBeat? start, RDBeat? end) where TEvent : IBaseEvent
            {
                if (source is EventEnumerator<TEvent> casted)
                    return casted.InRange(new(start, end));
                return source is OrderedEventCollection ordered
                    ? (IEventEnumerable<TEvent>)(ordered.GetEnumerator() as EventEnumerator<TEvent> ?? new EventEnumerator<TEvent>(ordered)).InRange(new(start, end))
                    : throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }

            /// <summary>
            /// Filters the event enumerable to only include events of type <typeparamref name="TEvent"/> within the specified <see cref="RDRange"/>.
            /// </summary>
            /// <typeparam name="TEvent">The type of event to filter for.</typeparam>
            /// <param name="range">The beat range to filter for.</param>
            /// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/> within the specified range.</returns>
            /// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{TEvent}"/> is not supported.</exception>
            public IEventEnumerable<TEvent> InRange<TEvent>(RDRange range) where TEvent : IBaseEvent
            {
                if (source is EventEnumerator<TEvent> casted)
                    return casted.InRange(range);
                return source is OrderedEventCollection ordered
                    ? (IEventEnumerable<TEvent>)(ordered.GetEnumerator() as EventEnumerator<TEvent> ?? new EventEnumerator<TEvent>(ordered)).InRange(range)
                    : throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }

            /// <summary>
            /// Filters the event enumerable to only include events of type <typeparamref name="TEvent"/> at the specified beat.
            /// </summary>
            /// <typeparam name="TEvent">The type of event to filter for.</typeparam>
            /// <param name="beat">The beat to filter for.</param>
            /// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/> at the specified beat.</returns>
            /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
            public IEnumerable<TEvent> AtBeat<TEvent>(RDBeat beat) where TEvent : IBaseEvent
            {
                if (source is EventEnumerator<TEvent> casted)
                    return casted.AtBeat(beat);
                if (source is OrderedEventCollection ordered)
                {
                    var collection = ordered.eventsBeatOrder.TryGetValue(beat, out TypedEventCollection<IBaseEvent>? b) ? b : [];
                    var types = Utils.EventTypeUtils.ToEnums(typeof(TEvent));
                    return collection.ContainsTypes(types) ?
                        collection.Where(i => types.Contains(i.Type)).OfType<TEvent>() :
                        [];
                }
                throw new NotSupportedException("The provided IEventEnumerable is not supported.");
            }
        }
    }
}
