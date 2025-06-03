using Newtonsoft.Json.Linq;
using RhythmBase.Global.Components;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Reflection;
namespace RhythmBase.RhythmDoctor.Extensions
{
	/// <summary>
	/// Extensions
	/// </summary>
	public static partial class Extensions
	{
#if NETSTANDARD
		internal static T FirstOrDefault<T>(this IEnumerable<T> e, T defaultValue) => e.FirstOrDefault(defaultValue);
		internal static T FirstOrDefault<T>(this IEnumerable<T> e, Func<T, bool> predicate, T defaultValue) => e.FirstOrDefault(predicate, defaultValue);
		internal static T LastOrDefault<T>(this IEnumerable<T> e, T defaultValue) => e.LastOrDefault(defaultValue);
		internal static T LastOrDefault<T>(this IEnumerable<T> e, Func<T, bool> predicate, T defaultValue) => e.LastOrDefault(predicate, defaultValue);
#endif
#if !NETSTANDARD
		private static (float start, float end) GetRange(OrderedEventCollection e, Index index)
		{
			(float, float) GetRange;
			try
			{
				IBaseEvent firstEvent = e.First<IBaseEvent>();
				IBaseEvent lastEvent = e.Last<IBaseEvent>();
				GetRange = index.IsFromEnd
							? (lastEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(lastEvent.Beat.BarBeat.bar - index.Value), 1f),
						lastEvent.Beat._calculator.BarBeatToBeatOnly((uint)(lastEvent.Beat.BarBeat.bar - index.Value + 1), 1f))
							: (firstEvent.Beat._calculator!.BarBeatToBeatOnly((uint)index.Value, 1f),
						firstEvent.Beat._calculator.BarBeatToBeatOnly((uint)(index.Value + 1), 1f));
			}
			catch
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}
			return GetRange;
		}
		private static (float start, float end) GetRange(OrderedEventCollection e, Range range)
		{
			(float start, float end) GetRange;
			try
			{
				IBaseEvent firstEvent = e.First<IBaseEvent>();
				IBaseEvent lastEvent = e.Last<IBaseEvent>();
				GetRange = (range.Start.IsFromEnd
							? lastEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(lastEvent.Beat.BarBeat.bar - (ulong)(long)range.Start.Value), 1f)
							: firstEvent.Beat._calculator!.BarBeatToBeatOnly((uint)Math.Max(range.Start.Value, 1), 1f),
						range.End.IsFromEnd
							? lastEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(lastEvent.Beat.BarBeat.bar - (ulong)(long)range.End.Value + 1UL), 1f)
							: firstEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(range.End.Value + 1), 1f));
			}
			catch
			{
				throw new ArgumentOutOfRangeException(nameof(range));
			}
			return GetRange;
		}
#endif
		/// <summary>
		/// Null or equal.
		/// </summary>
		/// <param name="e">one item.</param>
		/// <param name="obj">another item.</param>
		/// <returns>
		/// <list type="table">
		/// <item>When neither item is empty,<br />Returns true only if both are equal</item>
		/// <item>when one of the two is empty,<br />Returns true.</item>
		/// <item>when both are empty,<br />Returns false.</item>
		/// </list>
		/// </returns>
		public static bool NullableEquals(this float? e, float? obj) => (e != null && obj != null && e.Value == obj.Value) || (e == null && obj == null);
		/// <summary>
		/// Make strings follow the Upper Camel Case.
		/// </summary>
		/// <returns>The result.</returns>
		public static string ToUpperCamelCase(this string e)
		{
			char[] S = [.. e];
			S[0] = S[0].ToString().ToUpper()[0];
			return string.Join("", [new string(S)]);
		}
		/// <summary>
		/// Make a specific key of a JObject follow the Upper Camel Case.
		/// </summary>
		internal static void ToUpperCamelCase(this JObject e, string key)
		{
			JToken token = e[key] ?? throw new NullReferenceException();
			e.Remove(key);
			e[key.ToUpperCamelCase()] = token;
		}
		/// <summary>
		/// Make strings follow the Lower Camel Case.
		/// </summary>
		/// <returns>The result.</returns>
		public static string ToLowerCamelCase(this string e)
		{
			char[] S = [.. e];
			S[0] = S[0].ToString().ToLower()[0];
			return string.Join("", [new string(S)]);
		}
		/// <summary>
		/// Make a specific key of a JObject follow the Lower Camel Case.
		/// </summary>
		internal static void ToLowerCamelCase(this JObject e, string key)
		{
			JToken token = e[key] ?? throw new NullReferenceException();
			e.Remove(key);
			e[key.ToLowerCamelCase()] = token;
		}
		/// <summary>
		/// Convert color format from RGBA to ARGB
		/// </summary>
		public static int RgbaToArgb(this int Rgba) => (Rgba >> 8 & 16777215) | (Rgba << 24 & -16777216);
		/// <summary>
		/// Convert color format from ARGB to RGBA
		/// </summary>
		public static int ArgbToRgba(this int Argb) => (Argb >> 24 & 255) | (Argb << 8 & -256);
		/// <summary>
		/// Calculate the fraction of <paramref name="splitBase" /> equal to the nearest floating point number.
		/// <example>
		/// <code>
		/// 2.236f.FixFraction(4) == 2.25f
		/// float.Pi.FixFraction(5) == 3.2f
		/// float.E.Fixfraction(2) == 2.5f
		/// </code>
		/// </example>
		/// </summary>
		/// <param name="beat">The float number.</param>
		/// <param name="splitBase">Indicate what fraction this is.</param>
		/// <returns></returns>
		public static float FixFraction(this float beat, uint splitBase) => (float)(Math.Round((double)(beat * splitBase)) / splitBase);
		/// <summary>
		/// Calculate the fraction of <paramref name="splitBase" /> equal to the nearest floating point number.
		/// </summary>
		public static RDBeat FixFraction(this RDBeat beat, uint splitBase) => new(beat.BeatOnly.FixFraction(splitBase));
		/// <summary>
		/// Converting enumeration constants to in-game colors。
		/// </summary>
		/// <param name="e">Collection</param>
		/// <returns>The in-game color.</returns>
		public static RDColor ToColor(this Bookmark.BookmarkColors e) => e switch
		{
			Bookmark.BookmarkColors.Blue => RDColor.FromRgba(11, 125, 206),
			Bookmark.BookmarkColors.Red => RDColor.FromRgba(219, 41, 41),
			Bookmark.BookmarkColors.Yellow => RDColor.FromRgba(212, 212, 51),
			Bookmark.BookmarkColors.Green => RDColor.FromRgba(54, 215, 54),
			_ => throw new NotSupportedException(),
		};
		/// <summary>
		/// Add a range of events.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="items"></param>
		public static void AddRange<TEvent>(this OrderedEventCollection<TEvent> e, IEnumerable<TEvent> items) where TEvent : IBaseEvent
		{
			foreach (TEvent item in items)
				e.Add(item);
		}
		/// <summary>  
		/// Adds a range of items to the specified <see cref="LevelElementCollection{TCollection}"/>.  
		/// </summary>  
		/// <typeparam name="TCollection">The type of elements in the collection, constrained to <see cref="OrderedEventCollection"/>.</typeparam>  
		/// <param name="e">The <see cref="LevelElementCollection{TCollection}"/> to which the items will be added.</param>  
		/// <param name="items">The range of items to add to the collection.</param>  
		public static void AddRange<TCollection>(this LevelElementCollection<TCollection> e, IEnumerable<TCollection> items) where TCollection : OrderedEventCollection
		{
			foreach (TCollection item in items)
				e.Add(item);
		}
		/// <summary>
		/// Filters a sequence of events based on a predicate.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent =>
			((IEnumerable<TEvent>)e.eventsBeatOrder
			.SelectMany(i => i.Value)).Where(predicate);
		/// <summary>
		/// Filters a sequence of events located at a time.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, RDBeat beat) where TEvent : IBaseEvent
		{
			IEnumerable<TEvent> Where = [];
			if (e.eventsBeatOrder.TryGetValue(beat, out TypedEventCollection<IBaseEvent>? value))
				Where = value.Cast<TEvent>().AsEnumerable();
			return Where;
		}
		/// <summary>
		/// Filters a sequence of events located at a range of time.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, RDBeat startBeat, RDBeat endBeat) where TEvent : IBaseEvent =>
			e.eventsBeatOrder
			.TakeWhile(i => i.Key < endBeat)
			.SkipWhile(i => i.Key < startBeat)
			.SelectMany(i => i.Value.OfType<TEvent>());
#if !NETSTANDARD
		/// <summary>
		/// Filters a sequence of events located at a bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Index bar) where TEvent : IBaseEvent
		{
			var (start, end) = GetRange(e, bar);
			return e.eventsBeatOrder
			.TakeWhile(i => i.Key.BeatOnly < end)
			.SkipWhile(i => i.Key.BeatOnly < start)
			.SelectMany(i => i.Value.OfType<TEvent>());
		}
#endif
		/// <summary>
		/// Filters a sequence of events located at a range of beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="range">Specified beat range.</param>
		/// <returns></returns>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, RDRange range) where TEvent : IBaseEvent =>
			(IEnumerable<TEvent>)e.eventsBeatOrder
			.TakeWhile(i => range.End == null || i.Key < range.End)
			.SkipWhile(i => range.Start != null && i.Key < range.Start)
			.SelectMany(i => i.Value);
#if !NETSTANDARD
		/// <summary>
		/// Filters a sequence of events located at a range of bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Range bars) where TEvent : IBaseEvent
		{
			var (start, end) = GetRange(e, bars);
			return e.eventsBeatOrder
			.TakeWhile(i => i.Key.BeatOnly < end)
			.SkipWhile(i => i.Key.BeatOnly < start)
			.SelectMany(i => i.Value.OfType<TEvent>());
		}
#endif
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDBeat beat) where TEvent : IBaseEvent => e.Where(beat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDBeat startBeat, RDBeat endBeat) where TEvent : IBaseEvent => e.Where(startBeat, endBeat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.Where(range).Where(predicate);
#if !NETSTANDARD
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.Where(bar).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified range of bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.Where(bars).Where(predicate);
#endif
		/// <summary>
		/// Filters a sequence of events in specified event type.
		/// </summary>
		/// <typeparam name="TEvent"></typeparam>
		/// <param name="e">Collection</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent
		{
			EventType[] enums = EventTypeUtils.ToEnums<TEvent>();
			return e.eventsBeatOrder
							.Where(i => i.Value._types
								.Any(enums.Contains))
							.SelectMany(i => i.Value).OfType<TEvent>();
		}
		/// <summary>
		/// Filters a sequence of events located at a beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, RDBeat beat) where TEvent : IBaseEvent
		{
			TypedEventCollection<IBaseEvent> value;
			return (e.eventsBeatOrder.TryGetValue(beat, out value!) ? value.OfType<TEvent>() : []) ?? [];
		}
		/// <summary>
		/// Filters a sequence of events located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, RDBeat startBeat, RDBeat endBeat) where TEvent : IBaseEvent => e.eventsBeatOrder
					.TakeWhile(i => i.Key < endBeat)
					.SkipWhile(i => i.Key < startBeat)
					.SelectMany(i => i.Value.OfType<TEvent>());
#if !NETSTANDARD
		/// <summary>
		/// Filters a sequence of events located at a bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Index bar) where TEvent : IBaseEvent
		{
			(float, float) rg = GetRange(e, bar);
			return e.eventsBeatOrder
				.TakeWhile((KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> i) => i.Key.BeatOnly < rg.Item2)
				.SkipWhile((KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> i) => i.Key.BeatOnly < rg.Item1)
				.SelectMany(i => i.Value.OfType<TEvent>());
		}
#endif
		/// <summary>
		/// Filters a sequence of events located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="range">Specified beat range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, RDRange range) where TEvent : IBaseEvent => e.eventsBeatOrder
			.TakeWhile(i => range.End == null || i.Key < range.End)
			.SkipWhile(i => range.Start != null && i.Key < range.Start)
			.SelectMany(i => i.Value.OfType<TEvent>());
#if !NETSTANDARD
		/// <summary>
		/// Filters a sequence of events located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Range bars) where TEvent : IBaseEvent
		{
			(float start, float end) = GetRange(e, bars);
			return e.eventsBeatOrder
							.TakeWhile(i => i.Key.BeatOnly < end)
							.SkipWhile(i => i.Key.BeatOnly < start)
							.SelectMany(i => i.Value.OfType<TEvent>());
		}
#endif
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, RDBeat beat) where TEvent : IBaseEvent => e.Where<TEvent>(beat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, RDBeat startBeat, RDBeat endBeat) where TEvent : IBaseEvent => e.Where<TEvent>(startBeat, endBeat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.Where<TEvent>(range).Where(predicate);
#if !NETSTANDARD
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.Where<TEvent>(bar).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.Where<TEvent>(bars).Where(predicate);
#endif
		/// <summary>
		/// Remove a sequence of events based on a predicate.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate)));
		/// <summary>
		/// Remove a sequence of events located at a time.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="beat">Specified beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, RDBeat beat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(beat)));
		/// <summary>
		/// Remove a sequence of events located at a range of time.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, RDBeat startBeat, RDBeat endBeat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(startBeat, endBeat)));
#if !NETSTANDARD
		/// <summary>
		/// Remove a sequence of events located at a bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="bar">Specified bar.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Index bar) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(bar)));
#endif
		/// <summary>
		/// Remove a sequence of events located at a range of beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="range">Specified beat range.</param>
		/// <returns></returns>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, RDRange range) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(range)));
#if !NETSTANDARD
		/// <summary>
		/// Remove a sequence of events located at a range of bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="bars">Specified bar range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Range bars) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(bars)));
#endif
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDBeat beat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, beat)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDBeat startBeat, RDBeat endBeat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, startBeat, endBeat)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, range)));
#if !NETSTANDARD
		/// <summary>                 
		/// Remove a sequence of events based on a predicate in specified bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, bar)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified range of bar.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, bars)));
#endif
		/// <summary>
		/// Returns the first element of the collection.
		/// </summary>
		/// <param name="e">Collection</param>
		public static TEvent First<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent => (TEvent)e.eventsBeatOrder.First().Value.First();
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		public static TEvent First<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.ConcatAll().First(predicate);
		/// <summary>
		/// Returns the first element of the collection in specified event type.
		/// </summary>
		/// <param name="e">Collection</param>
		public static TEvent First<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().First();
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition in specified event type.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		public static TEvent First<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().First(predicate);
		/// <summary>
		/// Returns the first event in the collection or the default value if the collection is empty.
		/// </summary>
		/// <typeparam name="TEvent">The type of event in the collection.</typeparam>
		/// <param name="e">The ordered event collection.</param>
		/// <returns>The first event in the collection or the default value if the collection is empty.</returns>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent
		{
			TypedEventCollection<IBaseEvent> value = e.eventsBeatOrder.FirstOrDefault().Value;
			return (TEvent?)(value?.FirstOrDefault());
		}
		/// <summary>
		/// Returns the first element of the collection, or <paramref name="defaultValue" /> if collection contains no elements.
		/// </summary>
		/// <param name="defaultValue">The default value to return if contains no elements.</param>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, TEvent defaultValue) where TEvent : IBaseEvent => e.ConcatAll().FirstOrDefault(defaultValue);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.ConcatAll().OfType<TEvent>().FirstOrDefault(predicate);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => e.ConcatAll().FirstOrDefault(predicate, defaultValue);
		/// <summary>
		/// Returns the first element of the collection in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault();
		/// <summary>
		/// Returns the first element of the collection in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault(defaultValue);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault(predicate);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		/// <param name="e">Collection</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault(predicate, defaultValue);
		/// <summary>
		/// Returns the last element of the collection.
		/// </summary>
		public static TEvent Last<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent => (TEvent)e.eventsBeatOrder.Last().Value.Last();
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		public static TEvent Last<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.ConcatAll().Last(predicate);
		/// <summary>
		/// Returns the last element of the collection in specified event type.
		/// </summary>
		/// <param name="e">Collection</param>
		public static TEvent Last<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().Last();
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition in specified event type.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		public static TEvent Last<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().Last(predicate);
		/// <summary>
		/// Returns the last element of the collection, or <see langword="null" /> if collection contains no elements.
		/// </summary>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent
		{
			IEnumerable<IBaseEvent> value = e.eventsBeatOrder.LastOrDefault().Value.AsEnumerable();
			return (TEvent?)(value?.LastOrDefault());
		}
		/// <summary>
		/// Returns the last element of the collection, or <paramref name="defaultValue" /> if collection contains no elements.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="defaultValue">The default value to return if contains no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, TEvent defaultValue) where TEvent : IBaseEvent => e.ConcatAll().LastOrDefault(defaultValue);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.ConcatAll().LastOrDefault(predicate);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => e.ConcatAll().LastOrDefault(predicate, defaultValue);
		/// <summary>
		/// Returns the last element of the collection in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault();
		/// <summary>
		/// Returns the last element of the collection in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection e, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault(defaultValue);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault(predicate);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault(predicate, defaultValue);
		/// <summary>
		/// Returns events from a collection as long as it less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, RDBeat beat) where TEvent : IBaseEvent
		{
			foreach (TEvent item in e.Where<TEvent>())
				if (item.Beat <= beat) yield return item;
				else break;
		}
#if !NETSTANDARD
		/// <summary>
		/// Returns events from a collection as long as it less than or equal to <paramref name="bar" />.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Index bar) where TEvent : IBaseEvent
		{
			TEvent firstEvent = e.First();
			TEvent lastEvent = e.Last();
			return e.TakeWhile(checked(bar.IsFromEnd
				? lastEvent.Beat._calculator!.BeatOf(lastEvent.Beat.BarBeat.bar - (uint)bar.Value + 1U, 1f)
				: firstEvent.Beat._calculator!.BeatOf((uint)(bar.Value + 1), 1f)));
		}
#endif
		/// <summary>
		/// Returns events from a collection as long as a specified condition is true.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent =>
			(IEnumerable<TEvent>)e.eventsBeatOrder
			.SelectMany(i => i.Value)
			.TakeWhile((Func<IBaseEvent, bool>)(object)predicate);
		/// <summary>
		/// Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDBeat beat) where TEvent : IBaseEvent => e.TakeWhile(beat).TakeWhile(predicate);
#if !NETSTANDARD
		/// <summary>                 
		/// Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="bar" />.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="e">Collection</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.TakeWhile(bar).TakeWhile(predicate);
#endif
		/// <summary>
		/// Returns events from a collection in specified event type as long as it less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, RDBeat beat) where TEvent : IBaseEvent
		{
			foreach (TEvent item in e.Where<TEvent>())
				if (item.Beat <= beat) yield return item;
				else break;
		}
#if !NETSTANDARD
		/// <summary>
		/// Returns events from a collection in specified event type as long as it less than or in <paramref name="bar" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Index bar) where TEvent : IBaseEvent
		{
			IBaseEvent firstEvent = e.First<IBaseEvent>();
			IBaseEvent lastEvent = e.Last<IBaseEvent>();
			return e.TakeWhile<TEvent>(checked(bar.IsFromEnd
				? lastEvent.Beat._calculator!.BeatOf((uint)(lastEvent.Beat.BarBeat.bar - (ulong)bar.Value + 1U), 1f)
				: firstEvent.Beat._calculator!.BeatOf((uint)(bar.Value + 1), 1f)));
		}
#endif
		/// <summary>
		/// Returns events from a collection in specified event type as long as a specified condition is true.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">Specified condition.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().TakeWhile(predicate);
		/// <summary>
		/// Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">Specified condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, RDBeat beat) where TEvent : IBaseEvent => e.TakeWhile<TEvent>(beat).TakeWhile(predicate);
#if !NETSTANDARD
		/// <summary>
		/// Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="bar" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="e">Collection</param>
		/// <param name="predicate">Specified condition.</param>
		/// <param name="bar">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.TakeWhile<TEvent>(bar).TakeWhile(predicate);
#endif
		/// <summary>
		/// Remove a range of events.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="items">A range of events.</param>
		/// <returns>The number of events successfully removed.</returns>
		public static int RemoveRange<TEvent>(this OrderedEventCollection<TEvent> e, IEnumerable<TEvent> items) where TEvent : IBaseEvent
		{
			int count = 0;
			foreach (var item in items)
				count += e.Remove(item) ? 1 : 0;
			return count;
		}
		/// <summary>
		/// Get all the hit of the level.
		/// </summary>
		public static IEnumerable<RDHit> GetHitBeat(this RDLevel e)
		{
			List<RDHit> L = [];
			foreach (Row item in e.Rows)
			{
				L.AddRange(item.HitBeats());
			}
			return L;
		}
		/// <summary>
		/// Get all the hit event of the level.
		/// </summary>
		public static IEnumerable<BaseBeat> GetHitEvents(this RDLevel e) => e.Where<BaseBeat>(IsHitable);
		/// <summary>
		/// Get all events with the specified tag.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="name">Tag name.</param>
		/// <param name="strict">Indicates whether the label is strictly matched.
		/// If <see langword="true" />, determine If it contains the specified tag.
		/// If <see langword="false" />, determine If it Is equal to the specified tag.</param>
		/// <returns>An <see cref="T:System.Linq.IGrouping`2" />, categorized by tag name.</returns>
		public static IEnumerable<IGrouping<string, TEvent>> GetTaggedEvents<TEvent>(this OrderedEventCollection<TEvent> e, string name, bool strict) where TEvent : IBaseEvent
		{
			if (string.IsNullOrEmpty(name))
				return [];
			if (strict)
				return e
					.Where(i => i.Tag == name).GroupBy(i => i.Tag);
			else
				return e
					.Where(i => i.Tag?.Contains(name) ?? false).GroupBy(i => i.Tag);
		}
		/// <summary>
		/// Get all classic beat events and their variants.
		/// </summary>
		private static IEnumerable<BaseBeat> ClassicBeats(this Row e) => e.Where((BaseBeat i) => i.Type == EventType.AddClassicBeat | i.Type == EventType.AddFreeTimeBeat | i.Type == EventType.PulseFreeTimeBeat);
		/// <summary>
		/// Get all oneshot beat events.
		/// </summary>
		private static IEnumerable<BaseBeat> OneshotBeats(this Row e) => e.Where((BaseBeat i) => i.Type == EventType.AddOneshotBeat);
		/// <summary>
		/// Get all hits of all beats.
		/// </summary>
		public static IEnumerable<RDHit> HitBeats(this Row e)
		{
			RowTypes rowType = e.RowType;
			IEnumerable<RDHit> HitBeats;
			if (rowType != RowTypes.Classic)
			{
				if (rowType != RowTypes.Oneshot)
					throw new RhythmBaseException("How?");
				HitBeats = e.OneshotBeats().SelectMany((BaseBeat i) => i.HitTimes());
			}
			else
				HitBeats = e.ClassicBeats().SelectMany((BaseBeat i) => i.HitTimes());
			return HitBeats;
		}
		/// <summary>
		/// Get an instance of the beat associated with the level.
		/// </summary>
		/// <param name="e">RDLevel</param>
		/// <param name="beatOnly">Total number of 1-based beats.</param>
		public static RDBeat BeatOf(this RDLevel e, float beatOnly) => e.Calculator.BeatOf(beatOnly);
		/// <summary>
		/// Get an instance of the beat associated with the level.
		/// </summary>
		/// <param name="e">RDLevel</param>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		public static RDBeat BeatOf(this RDLevel e, uint bar, float beat) => e.Calculator.BeatOf(bar, beat);
		/// <summary>
		/// Get an instance of the beat associated with the level.
		/// </summary>
		/// <param name="e">RDLevel</param>
		/// <param name="timeSpan">Total time span of the beat.</param>
		public static RDBeat BeatOf(this RDLevel e, TimeSpan timeSpan) => e.Calculator.BeatOf(timeSpan);
		/// <summary>
		/// Get the row beat status
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		/// <exception cref="RhythmBaseException"></exception>
		public static SortedDictionary<float, int[]> GetRowBeatStatus(this Row e)
		{
			SortedDictionary<float, int[]> L = [];
			RowTypes rowType = e.RowType;
			switch (rowType)
			{
				case RowTypes.Classic:
					int[] value = new int[7];
					L.Add(0f, value);
					foreach (IBaseEvent beat in e)
						switch (beat.Type)
						{
							case EventType.AddClassicBeat:
								AddClassicBeat trueBeat = (AddClassicBeat)beat;
								int i = 0;
								do
								{
									int[] statusArray = L[beat.Beat.BeatOnly] ?? new int[7];
									int[] array = statusArray;
									int num = i;
									ref int ptr = ref array[num];
									array[num] = ptr + 1;
									L[beat.Beat.BeatOnly] = statusArray;
									i++;
								}
								while (i <= 6);
								break;
							default:
								throw new NotImplementedException();
						}
					break;
				case RowTypes.Oneshot:
					throw new NotImplementedException();
				default:
					throw new RhythmBaseException("How");
			}
			return L;
		}
		/// <summary>
		/// Get all beats of the row.
		/// </summary>
		public static IEnumerable<BaseBeat> Beats(this Row e)
		{
			RowTypes rowType = e.RowType;
			IEnumerable<BaseBeat> Beats;
			if (rowType != RowTypes.Classic)
			{
				if (rowType != RowTypes.Oneshot)
					throw new RhythmBaseException("How?");
				Beats = e.OneshotBeats();
			}
			else
				Beats = e.ClassicBeats();
			return Beats;
		}
		/// <summary>
		/// Returns all previous events of the same type, including events of the same beat but executed before itself.
		/// </summary>
		public static IEnumerable<TEvent> Before<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel?.Where<TEvent>(e.Beat.BaseLevel.DefaultBeat, e.Beat) ?? [];
		/// <summary>
		/// Returns all previous events of the specified type, including events of the same beat but executed before itself.
		/// </summary>
		public static IEnumerable<TEvent> Before<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel?.Where<TEvent>(e.Beat.BaseLevel.DefaultBeat, e.Beat) ?? [];
		/// <summary>
		/// Returns all events of the same type that follow, including events of the same beat but executed after itself.
		/// </summary>
		public static IEnumerable<TEvent> After<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel?.Where<TEvent>(i => i.Beat > e.Beat) ?? [];
		/// <summary>
		/// Returns all events of the specified type that follow, including events of the same beat but executed after itself.
		/// </summary>
		public static IEnumerable<TEvent> After<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel?.Where<TEvent>(i => i.Beat > e.Beat) ?? [];
		/// <summary>
		/// Returns the previous event of the same type, including events of the same beat but executed before itself.
		/// </summary>
		public static TEvent Front<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Before().Last();
		/// <summary>
		/// Returns the previous event of the specified type, including events of the same beat but executed before itself.
		/// </summary>
		public static TEvent Front<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Before<TEvent>().Last();
		/// <summary>
		/// Returns the previous event of the same type, including events of the same beat but executed before itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? FrontOrDefault<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Before().LastOrDefault();
		/// <summary>
		/// Returns the previous event of the specified type, including events of the same beat but executed before itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? FrontOrDefault<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Before<TEvent>().LastOrDefault();
		/// <summary>
		/// Returns the next event of the same type, including events of the same beat but executed after itself.
		/// </summary>
		public static TEvent Next<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.After().First();
		/// <summary>
		/// Returns the next event of the specified type, including events of the same beat but executed after itself.
		/// </summary>
		public static TEvent Next<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.After<TEvent>().First();
		/// <summary>
		/// Returns the next event of the same type, including events of the same beat but executed after itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? NextOrDefault<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.After().FirstOrDefault();
		/// <summary>
		/// Returns the next event of the specified type, including events of the same beat but executed after itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? NextOrDefault<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.After<TEvent>().FirstOrDefault();      //?
		/// <summary>
		/// Shallow copy.
		/// </summary>
		public static TEvent MemberwiseClone<TEvent>(this TEvent e) where TEvent : IBaseEvent, new() => (TEvent)e.MClone();
		internal static object MClone(this object e)
		{
			if (e != null)
			{
				Type type = e.GetType();
				object copy = Activator.CreateInstance(type)!;
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (PropertyInfo p in properties)
				{
					if (p.CanWrite)
					{
						p.SetValue(copy, p.GetValue(e));
					}
				}
				return copy;
			}
			throw new NullReferenceException();
		}
		/// <inheritdoc/>
		public static string GetCloseTag(string name) => $"</{name}>";
		/// <inheritdoc/>
		public static string GetOpenTag(string name, string? arg = null) => arg is null ? $"<{name}>" : $"<{name}={arg}>";
		/// <summary>
		/// Tries to add a tag to the specified string based on the provided name and boolean values.
		/// </summary>
		/// <param name="tag">The string to which the tag will be added.</param>
		/// <param name="name">The name of the tag.</param>
		/// <param name="before">A boolean value indicating whether the tag is before.</param>
		/// <param name="after">A boolean value indicating whether the tag is after.</param>
		public static void TryAddTag(ref string tag, string name, bool before, bool after)
		{
			if (before != after)
				tag += after
				? GetOpenTag(name)
				: GetCloseTag(name);
		}
		/// <summary>
		/// Tries to add a tag to the specified string based on the provided name and optional string values.
		/// </summary>
		/// <param name="tag">The string to which the tag will be added.</param>
		/// <param name="name">The name of the tag.</param>
		/// <param name="before">An optional string value indicating the tag before.</param>
		/// <param name="after">An optional string value indicating the tag after.</param>
		public static void TryAddTag(ref string tag, string name, string? before, string? after)
		{
			if (before != after)
				tag += after is null
				? GetCloseTag(name)
				: before is null
				? GetOpenTag(name, after)
				: GetCloseTag(name) + GetOpenTag(name, after);
		}
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
		public enum Wavetype
		{
			BoomAndRush,
			Spring,
			Spike,
			SpikeHuge,
			Ball,
			Single
		}
		public enum ShockWaveType
		{
			size,
			distortion,
			duration
		}
		public enum Particle
		{
			HitExplosion,
			leveleventexplosion
		}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
	}
}
