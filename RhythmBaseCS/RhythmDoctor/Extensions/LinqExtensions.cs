using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Extensions
{
	partial class Extensions
	{
		/// <summary>
		/// Filters the event enumerable to only include events of the specified type <typeparamref name="TEvent"/>.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to filter for.</typeparam>
		/// <param name="source">The source event enumerable.</param>
		/// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/>.</returns>
		/// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
		public static IEventEnumerable<TEvent> OfEvent<TEvent>(this IEventEnumerable<IBaseEvent> source) where TEvent : IBaseEvent
		{
			if (source is EventEnumerator<IBaseEvent> casted)
				return casted.OfEvent<TEvent>();
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).OfEvent<TEvent>();
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}

		/// <summary>
		/// Filters the event enumerable to only include events of the specified event types.
		/// </summary>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="types">The event types to filter for.</param>
		/// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events of the specified types.</returns>
		/// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
		public static IEventEnumerable<IBaseEvent> OfEvents(this IEventEnumerable<IBaseEvent> source, params EventType[] types)
		{
			if (source is EventEnumerator<IBaseEvent> casted1)
				return casted1.OfEvents(types);
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).OfEvents(types);
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}

		/// <summary>
		/// Filters the event enumerable to only include events within the specified beat range.
		/// </summary>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="start">The start beat of the range (inclusive).</param>
		/// <param name="end">The end beat of the range (inclusive).</param>
		/// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events within the specified range.</returns>
		/// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
		public static IEventEnumerable<IBaseEvent> InRange(this IEventEnumerable<IBaseEvent> source, RDBeat? start, RDBeat? end)
		{
			if (source is EventEnumerator<IBaseEvent> casted)
				return casted.InRange(new(start, end));
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).InRange(new(start, end));
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}

		/// <summary>
		/// Filters the event enumerable to only include events within the specified <see cref="RDRange"/>.
		/// </summary>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="range">The beat range to filter for.</param>
		/// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events within the specified range.</returns>
		/// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{IBaseEvent}"/> is not supported.</exception>
		public static IEventEnumerable<IBaseEvent> InRange(this IEventEnumerable<IBaseEvent> source, RDRange range)
		{
			if (source is EventEnumerator<IBaseEvent> casted)
				return casted.InRange(range);
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).InRange(range);
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}

		/// <summary>
		/// Filters the event enumerable to only include events at the specified beat.
		/// </summary>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="beat">The beat to filter for.</param>
		/// <returns>An <see cref="IEventEnumerable{IBaseEvent}"/> containing only events at the specified beat.</returns>
		/// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
		private static IEventEnumerable<IBaseEvent> AtBeat(this IEventEnumerable<IBaseEvent> source, RDBeat beat)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Filters the event enumerable to only include events of type <typeparamref name="TEvent"/> within the specified beat range.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to filter for.</typeparam>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="start">The start beat of the range (inclusive).</param>
		/// <param name="end">The end beat of the range (inclusive).</param>
		/// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/> within the specified range.</returns>
		/// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{TEvent}"/> is not supported.</exception>
		public static IEventEnumerable<TEvent> InRange<TEvent>(this IEventEnumerable<TEvent> source, RDBeat? start, RDBeat? end) where TEvent : IBaseEvent
		{
			if (source is EventEnumerator<TEvent> casted)
				return casted.InRange(new(start, end));
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<TEvent> ?? new EventEnumerator<TEvent>(ordered)).InRange(new(start, end));
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}

		/// <summary>
		/// Filters the event enumerable to only include events of type <typeparamref name="TEvent"/> within the specified <see cref="RDRange"/>.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to filter for.</typeparam>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="range">The beat range to filter for.</param>
		/// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/> within the specified range.</returns>
		/// <exception cref="NotSupportedException">Thrown if the provided <see cref="IEventEnumerable{TEvent}"/> is not supported.</exception>
		public static IEventEnumerable<TEvent> InRange<TEvent>(this IEventEnumerable<TEvent> source, RDRange range) where TEvent : IBaseEvent
		{
			if (source is EventEnumerator<TEvent> casted)
				return casted.InRange(range);
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<TEvent> ?? new EventEnumerator<TEvent>(ordered)).InRange(range);
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}

		/// <summary>
		/// Filters the event enumerable to only include events of type <typeparamref name="TEvent"/> at the specified beat.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to filter for.</typeparam>
		/// <param name="source">The source event enumerable.</param>
		/// <param name="beat">The beat to filter for.</param>
		/// <returns>An <see cref="IEventEnumerable{TEvent}"/> containing only events of type <typeparamref name="TEvent"/> at the specified beat.</returns>
		/// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
		private static IEventEnumerable<TEvent> AtBeat<TEvent>(this IEventEnumerable<TEvent> source, RDBeat beat) where TEvent : IBaseEvent
		{
			throw new NotImplementedException();
		}
	}
}
