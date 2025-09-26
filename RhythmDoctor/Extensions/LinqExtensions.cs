using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Components.Linq;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Extensions
{
	partial class Extensions
	{
		public static IEventEnumerable<TEvent> OfEvent<TEvent>(this IEventEnumerable<IBaseEvent> source) where TEvent : IBaseEvent
		{
			if (source is EventEnumerator<IBaseEvent> casted)
				return casted.OfEvent<TEvent>();
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).OfEvent<TEvent>();
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}
		public static IEventEnumerable<IBaseEvent> WithEvent<TEvent, TExtraEvent>(this IEventEnumerable<TEvent> source)
			where TEvent : IBaseEvent
			where TExtraEvent : IBaseEvent
		{
			if (source is EventEnumerator<TEvent> casted)
				return casted.WithEvent<TExtraEvent>();
			if (source is EventEnumerator<IBaseEvent> casted1)
				return casted1.WithEvent<TExtraEvent>();
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).WithEvent<TExtraEvent>();
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}
		public static IEventEnumerable<IBaseEvent> InRange(this IEventEnumerable<IBaseEvent> source, RDBeat? start, RDBeat? end)
		{
			if (source is EventEnumerator<IBaseEvent> casted)
				return casted.InRange(new(start, end));
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).InRange(new(start, end));
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}
		public static IEventEnumerable<IBaseEvent> InRange(this IEventEnumerable<IBaseEvent> source, RDRange range)
		{
			if (source is EventEnumerator<IBaseEvent> casted)
				return casted.InRange(range);
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<IBaseEvent> ?? new EventEnumerator<IBaseEvent>(ordered)).InRange(range);
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}
		private static IEventEnumerable<IBaseEvent> AtBeat(this IEventEnumerable<IBaseEvent> source, RDBeat beat)
		{
			throw new NotImplementedException();
		}
		public static IEventEnumerable<TEvent> InRange<TEvent>(this IEventEnumerable<TEvent> source, RDBeat? start, RDBeat? end) where TEvent : IBaseEvent
		{
			if (source is EventEnumerator<TEvent> casted)
				return casted.InRange(new(start, end));
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<TEvent> ?? new EventEnumerator<TEvent>(ordered)).InRange(new(start, end));
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}
		public static IEventEnumerable<TEvent> InRange<TEvent>(this IEventEnumerable<TEvent> source, RDRange range) where TEvent : IBaseEvent
		{
			if (source is EventEnumerator<TEvent> casted)
				return casted.InRange(range);
			if (source is OrderedEventCollection ordered)
				return (ordered.GetEnumerator() as EventEnumerator<TEvent> ?? new EventEnumerator<TEvent>(ordered)).InRange(range);
			throw new NotSupportedException("The provided IEventEnumerable is not supported.");
		}
		private static IEventEnumerable<TEvent> AtBeat<TEvent>(this IEventEnumerable<TEvent> source, RDBeat beat) where TEvent : IBaseEvent
		{
			throw new NotImplementedException();
		}
	}
}
