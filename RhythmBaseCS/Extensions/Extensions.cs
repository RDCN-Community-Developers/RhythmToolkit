using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Assets;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Utils;
using SkiaSharp;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;
using static RhythmBase.Utils.Utils;
namespace RhythmBase.Extensions
{
	[StandardModule]
	public static class Extensions
	{
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
				GetRange = ((range.Start.IsFromEnd
							? lastEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(((ulong)lastEvent.Beat.BarBeat.bar) - (ulong)((long)range.Start.Value)), 1f)
							: firstEvent.Beat._calculator!.BarBeatToBeatOnly((uint)Math.Max(range.Start.Value, 1), 1f),
						range.End.IsFromEnd
							? lastEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(((ulong)lastEvent.Beat.BarBeat.bar) - (ulong)((long)range.End.Value) + 1UL), 1f)
							: firstEvent.Beat._calculator!.BarBeatToBeatOnly((uint)(range.End.Value + 1), 1f)));
			}
			catch
			{
				throw new ArgumentOutOfRangeException(nameof(range));
			}
			return GetRange;
		}
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
		public static bool NullableEquals(this float? e, float? obj) => ((e != null & obj != null) && e.Value == obj.Value) || (e == null && obj == null);
		/// <summary>
		///
		/// </summary>
		/// <param name="e"></param>
		public static bool IsNullOrEmpty(this string e) => e == null || e.Length == 0;
		/// <summary>
		/// Make strings follow the Upper Camel Case.
		/// </summary>
		/// <returns>The result.</returns>
		public static string ToUpperCamelCase(this string e)
		{
			char[] S = [.. e];
			S[0] = (S[0].ToString().ToUpper()[0]);
			return string.Join("", [new string(S)]);
		}
		/// <summary>
		/// Make a specific key of a JObject follow the Upper Camel Case.
		/// </summary>
		internal static void ToUpperCamelCase(this JObject e, string key)
		{
			JToken token = e[key];
			e.Remove(key);
			e[key.ToUpperCamelCase()] = token;
		}
		/// <summary>
		/// Make keys of JObject follow the Upper Camel Case.
		/// </summary>
		internal static void ToUpperCamelCase(this JObject e)
		{
			foreach (KeyValuePair<string, JToken?> pair in e)
				e.ToUpperCamelCase(pair.Key);
		}
		/// <summary>
		/// Make strings follow the Lower Camel Case.
		/// </summary>
		/// <returns>The result.</returns>
		public static string ToLowerCamelCase(this string e)
		{
			char[] S = [.. e];
			S[0] = (S[0].ToString().ToLower()[0]);
			return string.Join("", [new string(S)]);
		}
		/// <summary>
		/// Make a specific key of a JObject follow the Lower Camel Case.
		/// </summary>
		internal static void ToLowerCamelCase(this JObject e, string key)
		{
			JToken token = e[key];
			e.Remove(key);
			e[key.ToLowerCamelCase()] = token;
		}
		/// <summary>
		/// Make keys of JObject follow the Lower Camel Case.
		/// </summary>
		internal static void ToLowerCamelCase(this JObject e)
		{
			foreach (KeyValuePair<string, JToken?> pair in e)
				e.ToLowerCamelCase(pair.Key);
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
		/// Convert <see cref="T:SkiaSharp.SKPoint" /> to <see cref="T:RhythmBase.Components.RDPointN" />.
		/// </summary>
		/// <returns></returns>
		public static RDPointN ToRDPoint(this SKPoint e) => new(e.X, e.Y);
		/// <summary>
		/// Convert <see cref="T:SkiaSharp.SKPointI" /> to <see cref="T:RhythmBase.Components.RDPointNI" />.
		/// </summary>
		/// <returns></returns>
		public static RDPointNI ToRDPointI(this SKPointI e) => new(e.X, e.Y);
		/// <summary>
		/// Convert <see cref="T:SkiaSharp.SKSize" /> to <see cref="T:RhythmBase.Components.RDSize" />.
		/// </summary>
		/// <returns></returns>
		public static RDSizeN ToRDSize(this SKSize e) => new(e.Width, e.Height);
		/// <summary>
		/// Convert <see cref="T:SkiaSharp.SKSizeI" /> to <see cref="T:RhythmBase.Components.RDSizeI" />.
		/// </summary>
		/// <returns></returns>
		public static RDSizeNI ToRDSizeI(this SKSizeI e) => new(e.Width, e.Height);
		/// <summary>
		/// Convert <see cref="T:RhythmBase.Components.RDPoint" /> to <see cref="T:SkiaSharp.SKSizeI" />.
		/// </summary>
		/// <returns></returns>
		public static SKPoint ToSKPoint(this RDPointN e) => new(e.X, e.Y);
		/// <summary>
		/// Convert <see cref="T:RhythmBase.Components.RDPointI" /> to <see cref="T:SkiaSharp.SKPointI" />.
		/// </summary>
		/// <returns></returns>
		public static SKPointI ToSKPointI(this RDPointNI e) => new(e.X, e.Y);
		/// <summary>
		/// Convert <see cref="T:RhythmBase.Components.RDSize" /> to <see cref="T:SkiaSharp.SKSize" />.
		/// </summary>
		/// <returns></returns>
		public static SKSize ToSKSize(this RDSizeN e) => new(e.Width, e.Height);
		/// <summary>
		/// Convert <see cref="T:RhythmBase.Components.RDSizeI" /> to <see cref="T:SkiaSharp.SKSizeI" />.
		/// </summary>
		/// <returns></returns>
		public static SKSizeI ToSKSizeI(this RDSizeNI e) => new(e.Width, e.Height);
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
		public static Beat FixFraction(this Beat beat, uint splitBase) => new(beat.BeatOnly.FixFraction(splitBase));
		/// <summary>
		/// Converting enumeration constants to in-game colors。
		/// </summary>
		/// <returns>The in-game color.</returns>
		public static SKColor ToColor(this Bookmark.BookmarkColors e) => e switch
		{
			Bookmark.BookmarkColors.Blue => new(11, 125, 206),
			Bookmark.BookmarkColors.Red => new(219, 41, 41),
			Bookmark.BookmarkColors.Yellow => new(212, 212, 51),
			Bookmark.BookmarkColors.Green => new(54, 215, 54),
			_=>throw new NotSupportedException(),
		};
		/// <summary>
		/// Add a range of events.
		/// </summary>
		/// <param name="items"></param>
		public static void AddRange<TEvent>(this OrderedEventCollection<TEvent> e, IEnumerable<TEvent> items) where TEvent : IBaseEvent
		{
			foreach (TEvent item in items)
				e.Add(item);
		}
		/// <summary>
		/// Filters a sequence of events based on a predicate.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent =>
			((IEnumerable<TEvent>)e.eventsBeatOrder
			.SelectMany(i => i.Value)).Where(predicate);
		/// <summary>
		/// Filters a sequence of events located at a time.
		/// </summary>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Beat beat) where TEvent : IBaseEvent
		{
			IEnumerable<TEvent> Where = [];
			if (e.eventsBeatOrder.TryGetValue(beat, out TypedEventCollection<IBaseEvent> value))
				Where = (IEnumerable<TEvent>)value;
			return Where;
		}
		/// <summary>
		/// Filters a sequence of events located at a range of time.
		/// </summary>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent =>
			e.eventsBeatOrder
			.TakeWhile(i => i.Key < endBeat)
			.SkipWhile(i => i.Key < startBeat)
			.SelectMany(i => i.Value.OfType<TEvent>());
		/// <summary>
		/// Filters a sequence of events located at a bar.
		/// </summary>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Index bar) where TEvent : IBaseEvent
		{
			var (start, end) = GetRange(e, bar);
			return e.eventsBeatOrder
			.TakeWhile(i => i.Key.BeatOnly < end)
			.SkipWhile(i => i.Key.BeatOnly < start)
			.SelectMany(i => i.Value.OfType<TEvent>());
		}
		/// <summary>
		/// Filters a sequence of events located at a range of beat.
		/// </summary>
		/// <param name="range">Specified beat range.</param>
		/// <returns></returns>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, RDRange range) where TEvent : IBaseEvent =>
			(IEnumerable<TEvent>)e.eventsBeatOrder
			.TakeWhile(i => range.End == null || i.Key < range.End)
			.SkipWhile(i => range.Start != null && i.Key < range.Start)
			.SelectMany(i => i.Value);
		/// <summary>
		/// Filters a sequence of events located at a range of bar.
		/// </summary>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Range bars) where TEvent : IBaseEvent
		{
			var (start, end) = GetRange(e, bars);
			return e.eventsBeatOrder
			.TakeWhile(i => i.Key.BeatOnly < end)
			.SkipWhile(i => i.Key.BeatOnly < start)
			.SelectMany(i => i.Value.OfType<TEvent>());
		}
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified beat.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Beat beat) where TEvent : IBaseEvent => e.Where(beat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.Where(startBeat, endBeat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.Where(range).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified bar.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.Where(bar).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate in specified range of bar.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.Where(bars).Where(predicate);
		/// <summary>
		/// Filters a sequence of events in specified event type.
		/// </summary>
		/// <typeparam name="TEvent"></typeparam>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent
		{
			EventType[] enums = EventTypeUtils. ToEnums<TEvent>();
			return e.eventsBeatOrder
							.Where(i => i.Value._types
								.Any(enums.Contains))
							.SelectMany(i => i.Value).OfType<TEvent>();
		}
		/// <summary>
		/// Filters a sequence of events located at a beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Beat beat) where TEvent : IBaseEvent
		{
			TypedEventCollection<IBaseEvent> value;
			return (e.eventsBeatOrder.TryGetValue(beat, out value!) ? value.OfType<TEvent>() : []) ?? [];
		}
		/// <summary>
		/// Filters a sequence of events located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.eventsBeatOrder
					.TakeWhile(i => i.Key < endBeat)
					.SkipWhile(i => i.Key < startBeat)
					.SelectMany(i => i.Value.OfType<TEvent>());
		/// <summary>
		/// Filters a sequence of events located at a bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Index bar) where TEvent : IBaseEvent
		{
			(float, float) rg = GetRange(e, bar);
			return e.eventsBeatOrder
				.TakeWhile((KeyValuePair<Beat, TypedEventCollection<IBaseEvent>> i) => i.Key.BeatOnly < rg.Item2)
				.SkipWhile((KeyValuePair<Beat, TypedEventCollection<IBaseEvent>> i) => i.Key.BeatOnly < rg.Item1)
				.SelectMany(i => i.Value.OfType<TEvent>());
		}
		/// <summary>
		/// Filters a sequence of events located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="range">Specified beat range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, RDRange range) where TEvent : IBaseEvent => e.eventsBeatOrder
			.TakeWhile(i => range.End == null || i.Key < range.End)
			.SkipWhile(i => range.Start != null && i.Key < range.Start)
			.SelectMany(i => i.Value.OfType<TEvent>());
		/// <summary>
		/// Filters a sequence of events located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Range bars) where TEvent : IBaseEvent
		{
			(float start, float end) = GetRange(e, bars);
			return e.eventsBeatOrder
							.TakeWhile(i => i.Key.BeatOnly < end)
							.SkipWhile(i => i.Key.BeatOnly < start)
							.SelectMany(i => i.Value.OfType<TEvent>());
		}
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Beat beat) where TEvent : IBaseEvent => e.Where<TEvent>(beat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.Where<TEvent>(startBeat, endBeat).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.Where<TEvent>(range).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.Where<TEvent>(bar).Where(predicate);
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static IEnumerable<TEvent> Where<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.Where<TEvent>(bars).Where(predicate);
		/// <summary>
		/// Remove a sequence of events based on a predicate.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate)));
		/// <summary>
		/// Remove a sequence of events located at a time.
		/// </summary>
		/// <param name="beat">Specified beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Beat beat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(beat)));
		/// <summary>
		/// Remove a sequence of events located at a range of time.
		/// </summary>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(startBeat, endBeat)));
		/// <summary>
		/// Remove a sequence of events located at a bar.
		/// </summary>
		/// <param name="bar">Specified bar.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Index bar) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(bar)));
		/// <summary>
		/// Remove a sequence of events located at a range of beat.
		/// </summary>
		/// <param name="range">Specified beat range.</param>
		/// <returns></returns>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, RDRange range) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(range)));
		/// <summary>
		/// Remove a sequence of events located at a range of bar.
		/// </summary>
		/// <param name="bars">Specified bar range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Range bars) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(bars)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified beat.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Beat beat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, beat)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, startBeat, endBeat)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified range of beat.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, range)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified bar.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, bar)));
		/// <summary>
		/// Remove a sequence of events based on a predicate in specified range of bar.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, bars)));
		/// <summary>
		/// Remove a sequence of events in specified event type.
		/// </summary>
		/// <typeparam name="TEvent"></typeparam>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where<TEvent>()));
		/// <summary>
		/// Remove a sequence of events located at a beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="beat">Specified beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Beat beat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where<TEvent>(beat)));
		/// <summary>
		/// Remove a sequence of events located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where<TEvent>(startBeat, endBeat)));
		/// <summary>
		/// Filters a sequence of events located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="range">Specified beat range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, RDRange range) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where<TEvent>(range)));
		/// <summary>
		/// Remove a sequence of events located at a bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="bar">Specified bar.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Index bar) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where<TEvent>(bar)));
		/// <summary>
		/// Remove a sequence of events located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="bars">Specified bar range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Range bars) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where<TEvent>(bars)));
		/// <summary>
		/// Remove a sequence of events based on a predicate located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate)));
		/// <summary>
		/// Remove a sequence of events based on a predicate located at a beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Beat beat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, beat)));
		/// <summary>
		/// Remove a sequence of events based on a predicate located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="startBeat">Specified start beat.</param>
		/// <param name="endBeat">Specified end beat.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Beat startBeat, Beat endBeat) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, startBeat, endBeat)));
		/// <summary>
		/// Remove a sequence of events based on a predicate located at a range of beat in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="range">Specified beat range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, RDRange range) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, range)));
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, bar)));
		/// <summary>
		/// Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bars">Specified bar range.</param>
		public static int RemoveAll<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Range bars) where TEvent : IBaseEvent => e.RemoveRange(new List<TEvent>(e.Where(predicate, bars)));
		/// <summary>
		/// Returns the first element of the collection.
		/// </summary>
		public static TEvent First<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent => (TEvent)(object)e.eventsBeatOrder.First().Value.First();
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent First<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().First((Func<IBaseEvent, bool>)(object)predicate);
		/// <summary>
		/// Returns the first element of the collection in specified event type.
		/// </summary>
		public static TEvent First<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().First();
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition in specified event type.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent First<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().First(predicate);
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent
		{
			TypedEventCollection<IBaseEvent> value = e.eventsBeatOrder.FirstOrDefault().Value;
			return (TEvent?)(value?.FirstOrDefault());
		}
		/// <summary>
		/// Returns the first element of the collection, or <paramref name="defaultValue" /> if collection contains no elements.
		/// </summary>
		/// <param name="defaultValue">The default value to return if contains no elements.</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, TEvent defaultValue) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().FirstOrDefault((IBaseEvent)(object)defaultValue);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().FirstOrDefault((Func<IBaseEvent, bool>)(object)predicate);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().FirstOrDefault((Func<IBaseEvent, bool>)(object)predicate, (IBaseEvent)(object)defaultValue);
		/// <summary>
		/// Returns the first element of the collection in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault();
		/// <summary>
		/// Returns the first element of the collection in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault(defaultValue);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault(predicate);
		/// <summary>
		/// Returns the first element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent? FirstOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().FirstOrDefault(predicate, defaultValue);
		/// <summary>
		/// Returns the last element of the collection.
		/// </summary>
		public static TEvent Last<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent => (TEvent)e.eventsBeatOrder.Last().Value.Last();
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent Last<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().Last((Func<IBaseEvent, bool>)(object)predicate);
		/// <summary>
		/// Returns the last element of the collection in specified event type.
		/// </summary>
		public static TEvent Last<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().Last();
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition in specified event type.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent Last<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().Last(predicate);
		/// <summary>
		/// Returns the last element of the collection, or <see langword="null" /> if collection contains no elements.
		/// </summary>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e) where TEvent : IBaseEvent
		{
			IEnumerable<IBaseEvent> value = e.eventsBeatOrder.LastOrDefault().Value.AsEnumerable();
			return (TEvent?)(value?.LastOrDefault<IBaseEvent>());
		}
		/// <summary>
		/// Returns the last element of the collection, or <paramref name="defaultValue" /> if collection contains no elements.
		/// </summary>
		/// <param name="defaultValue">The default value to return if contains no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, TEvent defaultValue) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().LastOrDefault((IBaseEvent)(object)defaultValue);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => (TEvent?)(object?)e.ConcatAll().LastOrDefault((Func<IBaseEvent, bool>)(object)predicate);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => (TEvent)(object)e.ConcatAll().LastOrDefault((Func<IBaseEvent, bool>)(object)predicate, (IBaseEvent)(object)defaultValue);
		/// <summary>
		/// Returns the last element of the collection in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection e) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault();
		/// <summary>
		/// Returns the last element of the collection in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection e, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault(defaultValue);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition in specified event type, or <see langword="null" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static TEvent? LastOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault(predicate);
		/// <summary>
		/// Returns the last element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue" /> if matches no elements.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="defaultValue">The default value to return if matches no elements.</param>
		public static TEvent LastOrDefault<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, TEvent defaultValue) where TEvent : IBaseEvent => e.Where<TEvent>().LastOrDefault(predicate, defaultValue);
		/// <summary>
		/// Returns events from a collection as long as it less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Beat beat) where TEvent : IBaseEvent
		{
			foreach (TEvent item in e.Where<TEvent>())
				if (item.Beat <= beat) yield return item;
				else break;
		}
		/// <summary>
		/// Returns events from a collection as long as it less than or equal to <paramref name="bar" />.
		/// </summary>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Index bar) where TEvent : IBaseEvent
		{
			TEvent firstEvent = e.First();
			TEvent lastEvent = e.Last();
			return e.TakeWhile(checked(bar.IsFromEnd
				? lastEvent.Beat._calculator!.BeatOf((uint)(lastEvent.Beat.BarBeat.bar - (uint)bar.Value + 1U), 1f)
				: firstEvent.Beat._calculator!.BeatOf((uint)(bar.Value + 1), 1f)));
		}
		/// <summary>
		/// Returns events from a collection as long as a specified condition is true.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent =>
			(IEnumerable<TEvent>)e.eventsBeatOrder
			.SelectMany(i => i.Value)
			.TakeWhile((Func<IBaseEvent, bool>)(object)predicate);
		/// <summary>
		/// Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Beat beat) where TEvent : IBaseEvent => e.TakeWhile(beat).TakeWhile(predicate);
		/// <summary>
		/// Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="bar" />.
		/// </summary>
		/// <param name="predicate">A function to test each event for a condition.</param>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection<TEvent> e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.TakeWhile(bar).TakeWhile(predicate);
		/// <summary>
		/// Returns events from a collection in specified event type as long as it less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Beat beat) where TEvent : IBaseEvent
		{
			foreach (TEvent item in e.Where<TEvent>())
				if (item.Beat <= beat) yield return item;
				else break;
		}
		/// <summary>
		/// Returns events from a collection in specified event type as long as it less than or in <paramref name="bar" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="bar">Specified bar.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Index bar) where TEvent : IBaseEvent
		{
			IBaseEvent firstEvent = e.First<IBaseEvent>();
			IBaseEvent lastEvent = e.Last<IBaseEvent>();
			return e.TakeWhile<TEvent>(checked(bar.IsFromEnd
				? lastEvent.Beat._calculator!.BeatOf((uint)(lastEvent.Beat.BarBeat.bar - (ulong)bar.Value + 1U), 1f)
				: firstEvent.Beat._calculator!.BeatOf((uint)(bar.Value + 1), 1f)));
		}
		/// <summary>
		/// Returns events from a collection in specified event type as long as a specified condition is true.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">Specified condition.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate) where TEvent : IBaseEvent => e.Where<TEvent>().TakeWhile(predicate);
		/// <summary>
		/// Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="beat" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">Specified condition.</param>
		/// <param name="beat">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Beat beat) where TEvent : IBaseEvent => e.TakeWhile<TEvent>(beat).TakeWhile(predicate);
		/// <summary>
		/// Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="bar" />.
		/// </summary>
		/// <typeparam name="TEvent">Specified event type.</typeparam>
		/// <param name="predicate">Specified condition.</param>
		/// <param name="bar">Specified beat.</param>
		public static IEnumerable<TEvent> TakeWhile<TEvent>(this OrderedEventCollection e, Func<TEvent, bool> predicate, Index bar) where TEvent : IBaseEvent => e.TakeWhile<TEvent>(bar).TakeWhile(predicate);
		/// <summary>
		/// Remove a range of events.
		/// </summary>
		/// <param name="items">A range of events.</param>
		/// <returns>The number of events successfully removed.</returns>
		public static int RemoveRange<TEvent>(this OrderedEventCollection e, IEnumerable<TEvent> items) where TEvent : IBaseEvent
		{
			int count = 0;
			foreach (var item in items)
				count += e.Remove(item) ? 1 : 0;
			return count;
		}
		/// <summary>
		/// Remove a range of events.
		/// </summary>
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
		/// Determine if <paramref name="item1" /> is in front of <paramref name="item2" />
		/// </summary>
		/// <returns><list type="table">
		/// <item>If <paramref name="item1" /> is in front of <paramref name="item2" />, <see langword="true" /></item>
		/// <item>Else, <see langword="false" /></item>
		/// </list></returns>
		public static bool IsInFrontOf(this OrderedEventCollection e, IBaseEvent item1, IBaseEvent item2) => (item1.Beat < item2.Beat || (item1.Beat.BeatOnly == item2.Beat.BeatOnly && (e.eventsBeatOrder[item1.Beat].BeforeThan(item1, item2))));
		/// <summary>
		/// Determine if <paramref name="item1" /> is after <paramref name="item2" />
		/// </summary>
		/// <returns><list type="table">
		/// <item>If <paramref name="item1" /> is after <paramref name="item2" />, <see langword="true" /></item>
		/// <item>Else, <see langword="false" /></item>
		/// </list></returns>
		public static bool IsBehind(this OrderedEventCollection e, IBaseEvent item1, IBaseEvent item2)
		{
			return (item1.Beat > item2.Beat || (item1.Beat.BeatOnly == item2.Beat.BeatOnly && (e.eventsBeatOrder[item1.Beat].BeforeThan(item2, item1))));
		}
		/// <summary>
		/// Get all the hit of the level.
		/// </summary>
		public static IEnumerable<Hit> GetHitBeat(this RDLevel e)
		{
			List<Hit> L = [];
			foreach (RowEventCollection item in e.Rows)
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
		/// <param name="name">Tag name.</param>
		/// <param name="strict">Indicates whether the label is strictly matched.
		/// If <see langword="true" />, determine If it contains the specified tag.
		/// If <see langword="false" />, determine If it Is equal to the specified tag.</param>
		/// <returns>An <see cref="T:System.Linq.IGrouping`2" />, categorized by tag name.</returns>
		public static IEnumerable<IGrouping<string, TEvent>> GetTaggedEvents<TEvent>(this OrderedEventCollection<TEvent> e, string name, bool strict) where TEvent : IBaseEvent
		{
			if (name.IsNullOrEmpty())
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
		private static IEnumerable<BaseBeat> ClassicBeats(this RowEventCollection e) => e.Where((BaseBeat i) => i.Type == EventType.AddClassicBeat | i.Type == EventType.AddFreeTimeBeat | i.Type == EventType.PulseFreeTimeBeat);
		/// <summary>
		/// Get all oneshot beat events.
		/// </summary>
		private static IEnumerable<BaseBeat> OneshotBeats(this RowEventCollection e) => e.Where((BaseBeat i) => i.Type == EventType.AddOneshotBeat);
		/// <summary>
		/// Get all hits of all beats.
		/// </summary>
		public static IEnumerable<Hit> HitBeats(this RowEventCollection e)
		{
			RowType rowType = e.RowType;
			IEnumerable<Hit> HitBeats;
			if (rowType != RowType.Classic)
			{
				if (rowType != RowType.Oneshot)
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
		/// <param name="beatOnly">Total number of 1-based beats.</param>
		public static Beat BeatOf(this RDLevel e, float beatOnly) => e.Calculator.BeatOf(beatOnly);
		/// <summary>
		/// Get an instance of the beat associated with the level.
		/// </summary>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		public static Beat BeatOf(this RDLevel e, uint bar, float beat) => e.Calculator.BeatOf(bar, beat);
		/// <summary>
		/// Get an instance of the beat associated with the level.
		/// </summary>
		/// <param name="timeSpan">Total time span of the beat.</param>
		public static Beat BeatOf(this RDLevel e, TimeSpan timeSpan) => e.Calculator.BeatOf(timeSpan);
		public static SortedDictionary<float, int[]> GetRowBeatStatus(this RowEventCollection e)
		{
			SortedDictionary<float, int[]> L = [];
			RowType rowType = e.RowType;
			switch (rowType)
			{
				case RowType.Classic:
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
							//case EventType.AddFreeTimeBeat:
							//	break;
							//case EventType.PulseFreeTimeBeat:
							//	break;
							//case EventType.SetRowXs:
							//	break;
							default:
								throw new NotImplementedException();
						}
					break;
				case RowType.Oneshot:
					throw new NotImplementedException();
				default:
					throw new RhythmBaseException("How");
			}
			return L;
		}
		/// <summary>
		/// Get all beats of the row.
		/// </summary>
		public static IEnumerable<BaseBeat> Beats(this RowEventCollection e)
		{
			RowType rowType = e.RowType;
			IEnumerable<BaseBeat> Beats;
			if (rowType != RowType.Classic)
			{
				if (rowType != RowType.Oneshot)
					throw new RhythmBaseException("How?");
				Beats = e.OneshotBeats();
			}
			else
				Beats = e.ClassicBeats();
			return Beats;
		}
		/// <summary>
		/// Add a range of events.
		/// </summary>
		public static void AddRange(this ADLevel e, IEnumerable<ADTile> items)
		{
			foreach (ADTile item in items)
				e.Add(item);
		}
		/// <summary>
		/// Add a range of events.
		/// </summary>
		public static void AddRange(this ADTile e, IEnumerable<ADBaseTileEvent> items)
		{
			foreach (ADBaseTileEvent item in items)
			{
				e.Add(item);
			}
		}
		public static IEnumerable<ADTile> Where(this ADTileCollection e, Func<ADTile, bool> predicate) => e.AsEnumerable().Where(predicate);
		public static IEnumerable<ADBaseEvent> EventsWhere(this ADTileCollection e, Func<ADBaseEvent, bool> predicate) => e.Events.Where(predicate);
		public static IEnumerable<TEvent> EventsWhere<TEvent>(this ADTileCollection e) where TEvent : ADBaseEvent => e.Events.OfType<TEvent>();
		/// <summary>
		/// Check if another event is in front of itself, including events of the same beat but executed before itself.
		/// </summary>
		public static bool IsInFrontOf(this IBaseEvent e, IBaseEvent item) => e.Beat.BaseLevel.IsInFrontOf(e, item);
		/// <summary>
		/// Check if another event is after itself, including events of the same beat but executed after itself.
		/// </summary>
		public static bool IsBehind(this IBaseEvent e, IBaseEvent item) => e.Beat.BaseLevel.IsBehind(e, item);
		/// <summary>
		/// Returns all previous events of the same type, including events of the same beat but executed before itself.
		/// </summary>
		public static IEnumerable<TEvent> Before<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel.Where<TEvent>(e.Beat.BaseLevel.DefaultBeat, e.Beat);
		/// <summary>
		/// Returns all previous events of the specified type, including events of the same beat but executed before itself.
		/// </summary>
		public static IEnumerable<TEvent> Before<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel.Where<TEvent>(e.Beat.BaseLevel.DefaultBeat, e.Beat);
		/// <summary>
		/// Returns all events of the same type that follow, including events of the same beat but executed after itself.
		/// </summary>
		public static IEnumerable<TEvent> After<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel.Where<TEvent>(i => i.Beat > e.Beat);
		/// <summary>
		/// Returns all events of the specified type that follow, including events of the same beat but executed after itself.
		/// </summary>
		public static IEnumerable<TEvent> After<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Beat.BaseLevel.Where<TEvent>(i => i.Beat > e.Beat);
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
		public static TEvent FrontOrDefault<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.Before().LastOrDefault();
		/// <summary>
		/// Returns the previous event of the specified type, including events of the same beat but executed before itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent FrontOrDefault<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.Before<TEvent>().LastOrDefault();
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
		public static TEvent NextOrDefault<TEvent>(this TEvent e) where TEvent : IBaseEvent => e.After().FirstOrDefault();
		/// <summary>
		/// Returns the next event of the specified type, including events of the same beat but executed after itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent NextOrDefault<TEvent>(this IBaseEvent e) where TEvent : IBaseEvent => e.After<TEvent>().FirstOrDefault();		//?
		public static Beat DurationOffset(this Beat beat, float duration)
		{
			SetBeatsPerMinute setBPM = beat.BaseLevel.First((SetBeatsPerMinute i) => i.Beat > beat);
			Beat DurationOffset =
				beat.BarBeat.bar == setBPM.Beat.BarBeat.bar
				? beat + duration
				: beat + TimeSpan.FromMinutes((double)(duration / beat.BPM));
			return DurationOffset;
		}
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
		/// <summary>
		/// Get current player of the beat event.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static PlayerType Player(this BaseBeat e)
		{
			ChangePlayersRows? changePlayersRows = e.Beat.BaseLevel.LastOrDefault((ChangePlayersRows i) => i.Active && i.Players[e.Index] != PlayerType.NoChange);
			return (PlayerType)((changePlayersRows != null) ? changePlayersRows.Players[e.Index] : (PlayerType)e.Parent.Player);
		}
		/// <summary>
		/// Get the pulse sound effect of row beat event.
		/// </summary>
		/// <returns>The sound effect of row beat event.</returns>
		public static Audio BeatSound(this BaseBeat e)
		{
			SetBeatSound? setBeatSound = e.Parent.LastOrDefault((SetBeatSound i) => i.Beat < e.Beat && i.Active);
			return (setBeatSound?.Sound) ?? e.Parent.Sound;
		}
		/// <summary>
		/// Get the hit sound effect of row beat event.
		/// </summary>
		/// <returns>The sound effect of row beat event.</returns>
		public static Audio HitSound(this BaseBeat e)
		{
			Audio DefaultAudio = new()
			{
				AudioFile = e.Beat.BaseLevel.Manager.Create<IAudioFile>("sndClapHit"),
				Offset = TimeSpan.Zero,
				Pan = 100,
				Pitch = 100,
				Volume = 100
			};
			Audio HitSound;
			switch (e.Player())
			{
				case PlayerType.P1:
					{
						SetClapSounds? setClapSounds = e.Beat.BaseLevel.LastOrDefault((SetClapSounds i) => i.Active && i.P1Sound != null);
						HitSound = (setClapSounds?.P1Sound) ?? DefaultAudio;
						break;
					}
				case PlayerType.P2:
					{
						SetClapSounds? setClapSounds2 = e.Beat.BaseLevel.LastOrDefault((SetClapSounds i) => i.Active && i.P2Sound != null);
						HitSound = (setClapSounds2?.P2Sound) ?? DefaultAudio;
						break;
					}
				case PlayerType.CPU:
					{
						SetClapSounds? setClapSounds3 = e.Beat.BaseLevel.LastOrDefault((SetClapSounds i) => i.Active && i.CpuSound != null);
						HitSound = (setClapSounds3?.CpuSound) ?? DefaultAudio;
						break;
					}
				default:
					HitSound = DefaultAudio;
					break;
			}
			return HitSound;
		}
		/// <summary>
		/// Get the special tag of the tag event.
		/// </summary>
		/// <returns>special tags.</returns>
		public static TagAction.SpecialTag[] SpetialTags(this TagAction e) => (TagAction.SpecialTag[])(from i in Enum.GetValues<TagAction.SpecialTag>()
																									   where e.ActionTag.Contains(string.Format("[{0}]", i))
																									   select i);
		/// <summary>
		/// Convert beat pattern to string.
		/// </summary>
		/// <returns>The pattern string.</returns>
		public static string Pattern(this AddClassicBeat e) => Utils.Utils.GetPatternString(e.RowXs());
		/// <summary>
		/// Get the actual beat pattern.
		/// </summary>
		/// <returns>The actual beat pattern.</returns>
		public static LimitedList<Patterns> RowXs(this AddClassicBeat e)
		{
			bool flag = e.SetXs == null;
			LimitedList<Patterns> RowXs;
			if (flag)
			{
				SetRowXs X = e.Parent.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i), new SetRowXs());
				RowXs = X.Pattern;
			}
			else
			{
				LimitedList<Patterns> T = new(6U, Patterns.None);
				AddClassicBeat.ClassicBeatPatterns? setXs = e.SetXs;
				int? num = (setXs != null) ? new int?((int)setXs.GetValueOrDefault()) : null;
				if (((num != null) ? new bool?(num.GetValueOrDefault() == 0) : null).GetValueOrDefault())
				{
					T[1] = Patterns.X;
					T[2] = Patterns.X;
					T[4] = Patterns.X;
					T[5] = Patterns.X;
				}
				else
				{
					num = (setXs != null) ? new int?((int)setXs.GetValueOrDefault()) : null;
					if (!(((num != null) ? new bool?(num.GetValueOrDefault() == 1) : null).GetValueOrDefault()))
					{
						throw new RhythmBaseException("How?");
					}
					T[1] = Patterns.X;
					T[3] = Patterns.X;
					T[5] = Patterns.X;
				}
				RowXs = T;
			}
			return RowXs;
		}
		/// <summary>
		/// Get the total length of the oneshot.
		/// </summary>
		/// <returns></returns>
		public static float Length(this AddOneshotBeat e) => e.Tick * e.Loops + e.Interval * e.Loops - 1f;
		/// <summary>
		/// Get the total length of the classic beat.
		/// </summary>
		/// <returns></returns>
		public static float Length(this AddClassicBeat e)
		{
			float SyncoSwing = e.Parent.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i), new SetRowXs()).SyncoSwing;
			return (float)((double)(e.Tick * 6f) - ((SyncoSwing == 0f) ? 0.5 : ((double)SyncoSwing)) * (double)e.Tick);
		}
		/// <summary>
		/// Check if it can be hit by player.
		/// </summary>
		public static bool IsHitable(this PulseFreeTimeBeat e)
		{
			int PulseIndexMin = 6;
			int PulseIndexMax = 6;
			foreach (BaseBeat item in ((IEnumerable<BaseBeat>)e.Parent
			.Where(e.IsBehind, new RDRange(e.Beat, null)))
			.Reverse())
			{
				EventType type = item.Type;
				switch (type)
				{
					case EventType.AddFreeTimeBeat:
						{
							AddFreeTimeBeat Temp2 = (AddFreeTimeBeat)item;
							if (PulseIndexMin <= (int)Temp2.Pulse & (int)Temp2.Pulse <= PulseIndexMax)
								return true;
							break;
						}
					case EventType.PulseFreeTimeBeat:
						{
							PulseFreeTimeBeat Temp = (PulseFreeTimeBeat)item;
							switch (Temp.Action)
							{
								case PulseFreeTimeBeat.ActionType.Increment:
									if (PulseIndexMin > 0)
										PulseIndexMin--;
									if (!(PulseIndexMax > 0))
										return false;
									PulseIndexMax--;
									break;
								case PulseFreeTimeBeat.ActionType.Decrement:
									if (PulseIndexMin > 0)
										PulseIndexMin++;
									if (!(PulseIndexMax < 6))
										return false;
									PulseIndexMax++;
									break;
								case PulseFreeTimeBeat.ActionType.Custom:
									if (!(PulseIndexMin <= Temp.CustomPulse & Temp.CustomPulse <= PulseIndexMax))
										return false;
									PulseIndexMin = 0;
									PulseIndexMax = 5;
									break;
								case PulseFreeTimeBeat.ActionType.Remove:
									return false;
							}
							if (PulseIndexMin > PulseIndexMax)
								return false;
							break;
						}
				}
			}
			return false;
		}
		/// <summary>
		/// Check if it can be hit by player.
		/// </summary>
		public static bool IsHitable(this AddFreeTimeBeat e) => e.Pulse == 6;
		/// <summary>
		/// Check if it can be hit by player.
		/// </summary>
		public static bool IsHitable(this BaseBeat e) => e.Type switch
		{
			EventType.AddClassicBeat or EventType.AddOneshotBeat => true,
			EventType.AddFreeTimeBeat => ((AddFreeTimeBeat)e).IsHitable(),
			_ => e.Type == EventType.PulseFreeTimeBeat && ((PulseFreeTimeBeat)e).IsHitable(),
		};
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<Hit> HitTimes(this AddClassicBeat e) =>
			new List<Hit> { new(e, e.GetBeat(6), e.Hold) }
			.AsEnumerable();
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<Hit> HitTimes(this AddOneshotBeat e)
		{
			e._beat.IfNullThrowException();
			List<Hit> L = [];
			uint loops = e.Loops;
			for (uint i = 0U; i <= loops; i += 1U)
			{
				sbyte b = (sbyte)(e.Subdivisions - 1);
				for (sbyte j = 0; j <= b; j += 1)
					L.Add(new Hit(e, new Beat(e._beat._calculator, e._beat.BeatOnly + i * e.Interval + e.Tick + e.Delay + (float)j * (e.Tick / (float)e.Subdivisions)), 0f));
			}
			return L.AsEnumerable();
		}
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<Hit> HitTimes(this AddFreeTimeBeat e) =>
			e.Pulse == 6
				? [new(e, e.Beat, e.Hold)]
				: ([]);
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<Hit> HitTimes(this PulseFreeTimeBeat e) =>
			e.IsHitable()
				? [new(e, e.Beat, e.Hold)]
				: ([]);
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<Hit> HitTimes(this BaseBeat e)
		{
			return e.Type switch
			{
				EventType.AddClassicBeat => ((AddClassicBeat)e).HitTimes(),
				EventType.AddFreeTimeBeat => ((AddFreeTimeBeat)e).HitTimes(),
				EventType.AddOneshotBeat => ((AddOneshotBeat)e).HitTimes(),
				_ => e.Type != EventType.PulseFreeTimeBeat
					? Array.Empty<Hit>().AsEnumerable()
					: ((PulseFreeTimeBeat)e).HitTimes(),
			};
		}
		/// <summary>
		/// Returns the pulse beat of the specified 0-based index.
		/// </summary>
		/// <exception cref="T:RhythmBase.Exceptions.RhythmBaseException">THIS IS 7TH BEAT GAMES!</exception>
		public static Beat GetBeat(this AddClassicBeat e, byte index)
		{
			SetRowXs x = e.Parent.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i), new SetRowXs());
			float Synco = 0 <= x.SyncoBeat && x.SyncoBeat < (sbyte)index ? (float)((x.SyncoSwing == 0f) ? 0.5 : ((double)x.SyncoSwing)) : 0f;
			if (index >= 7)
				throw new RhythmBaseException("THIS IS 7TH BEAT GAMES!");
			return e.Beat.DurationOffset(e.Tick * ((float)index - Synco));
		}
		/// <summary>
		/// Converts Xs patterns to string form.
		/// </summary>
		public static string GetPatternString(this SetRowXs e) => Utils.Utils.GetPatternString(e.Pattern);
		/// <summary>
		/// Creates a new <see cref="T:RhythmBase.Events.AdvanceText" /> subordinate to <see cref="T:RhythmBase.Events.FloatingText" /> at the specified beat. The new event created will be attempted to be added to the <see cref="T:RhythmBase.Events.FloatingText" />'s source level.
		/// </summary>
		/// <param name="beat">Specified beat.</param>
		public static AdvanceText CreateAdvanceText(this FloatingText e, Beat beat)
		{
			AdvanceText A = new()
			{
				Parent = e,
				Beat = beat.WithoutBinding()
			};
			e.Children.Add(A);
			return A;
		}
		/// <summary>
		/// Get the sequence of <see cref="T:RhythmBase.Events.PulseFreeTimeBeat" /> belonging to this <see cref="T:RhythmBase.Events.AddFreeTimeBeat" />, return all of the <see cref="T:RhythmBase.Events.PulseFreeTimeBeat" /> from the time the pulse was created to the time it was removed or hit.
		/// </summary>
		public static IEnumerable<PulseFreeTimeBeat> GetPulses(this AddFreeTimeBeat e)
		{
			List<PulseFreeTimeBeat> Result = [];
			byte pulse = e.Pulse;
			checked
			{
				foreach (PulseFreeTimeBeat item in e.Parent.Where<PulseFreeTimeBeat>(i => i.Active && e.IsInFrontOf(i)))
				{
					switch (item.Action)
					{
						case PulseFreeTimeBeat.ActionType.Increment:
							pulse += 1;
							Result.Add(item);
							break;
						case PulseFreeTimeBeat.ActionType.Decrement:
							pulse = (byte)((pulse > 0b1) ? (pulse - 0b1) : 0b1);
							Result.Add(item);
							break;
						case PulseFreeTimeBeat.ActionType.Custom:
							pulse = (byte)item.CustomPulse;
							Result.Add(item);
							break;
						case PulseFreeTimeBeat.ActionType.Remove:
							Result.Add(item);
							break;
					}
					if (pulse == 6)
						break;
				}
			}
			return Result;
		}
		private static SayReadyGetSetGo SplitCopy(this SayReadyGetSetGo e, float extraBeat, SayReadyGetSetGo.Words word)
		{
			SayReadyGetSetGo Temp = e.Clone<SayReadyGetSetGo>();
			Temp.Beat += extraBeat;
			Temp.PhraseToSay = word;
			Temp.Volume = e.Volume;
			return Temp;
		}
		/// <summary>
		/// Generate split event instances.
		/// </summary>
		public static IEnumerable<SayReadyGetSetGo> Split(this SayReadyGetSetGo e) => e.PhraseToSay switch
		{
			SayReadyGetSetGo.Words.SayReaDyGetSetGoNew => [
						e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayRea),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSayDy),
						e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3f, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4f, SayReadyGetSetGo.Words.JustSayGo)
								],
			SayReadyGetSetGo.Words.SayGetSetGo => [
						e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGo)
								],
			SayReadyGetSetGo.Words.SayReaDyGetSetOne => [
						e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayRea),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSayDy),
						e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3f, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4f, SayReadyGetSetGo.Words.Count1)
								],
			SayReadyGetSetGo.Words.SayGetSetOne => [
						e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.Count1)
								],
			SayReadyGetSetGo.Words.SayReadyGetSetGo => [
						e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayReady),
						e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3f, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4f, SayReadyGetSetGo.Words.JustSayGo)
								],
			_ => [e],
		};
		/// <summary>
		/// Generate split event instances.
		/// </summary>
		public static IEnumerable<AddOneshotBeat> Split(this AddOneshotBeat e)
		{
			e._beat.IfNullThrowException();
			List<AddOneshotBeat> L = [];
			uint loops = e.Loops;
			for (uint i = 0U; i <= loops; i += 1U)
			{
				AddOneshotBeat T = e.MemberwiseClone();
				T.Loops = 0U;
				T.Interval = 0f;
				T.Beat = new Beat(e._beat._calculator, unchecked(e.Beat.BeatOnly + i * e.Interval));
				L.Add(T);
			}
			return L.AsEnumerable();
		}
		/// <summary>
		/// Generate split event instances. Follow the most recently activated Xs.
		/// </summary>
		public static IEnumerable<BaseBeat> Split(this AddClassicBeat e)
		{
			SetRowXs x = e.Parent.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i), new SetRowXs());
			return e.Split(x);
		}
		/// <summary>
		/// Generate split event instances.
		/// </summary>
		public static IEnumerable<BaseBeat> Split(this AddClassicBeat e, SetRowXs Xs)
		{
			List<BaseBeat> L = [];
			AddFreeTimeBeat Head = e.Clone<AddFreeTimeBeat>();
			Head.Pulse = 0;
			Head.Hold = e.Hold;
			L.Add(Head);
			int i = 1;
			do
			{
				if (!(i < 6 && Xs.Pattern[i] == Patterns.X))
				{
					PulseFreeTimeBeat Pulse = e.Clone<PulseFreeTimeBeat>();
					PulseFreeTimeBeat pulseFreeTimeBeat;
					(pulseFreeTimeBeat = Pulse).Beat = pulseFreeTimeBeat.Beat + e.Tick * (float)i;
					if (i >= (int)Xs.SyncoBeat)
						(pulseFreeTimeBeat = Pulse).Beat = pulseFreeTimeBeat.Beat - Xs.SyncoSwing;
					if (i % 2 == 1)
						(pulseFreeTimeBeat = Pulse).Beat = pulseFreeTimeBeat.Beat + (e.Tick - ((e.Swing == 0f) ? e.Tick : e.Swing));
					Pulse.Hold = e.Hold;
					Pulse.Action = PulseFreeTimeBeat.ActionType.Increment;
					L.Add(Pulse);
				}
				i++;
			}
			while (i <= 6);
			return L.AsEnumerable();
		}
		/// <summary>
		/// Getting controlled events.
		/// </summary>
		public static IEnumerable<IGrouping<string, IBaseEvent>> ControllingEvents(this TagAction e) => e.Beat.BaseLevel.GetTaggedEvents(e.ActionTag, e.Action.HasFlag(TagAction.Actions.All));
		/// <summary>
		/// Remove auxiliary symbols.
		/// </summary>
		public static string TextOnly(this ShowDialogue e)
		{
			string result = e.Text;
			foreach (string item in new string[]
			{
				"shake",
				"shakeRadius=\\d+",
				"wave",
				"waveHeight=\\d+",
				"waveSpeed=\\d+",
				"swirl",
				"swirlRadius=\\d+",
				"swirlSpeed=\\d+",
				"static"
			})
				result = Regex.Replace(result, string.Format("\\[{0}\\]", item), "");
			return result;
		}
		/// <summary>
		/// Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		/// </summary>
		/// <param name="target">Specified position. </param>
		public static void MovePositionMaintainVisual(this Move e, RDPointE target)
		{
			if (e.Position != null && e.Pivot != null && e.Angle != null && e.Angle.Value.IsNumeric)
			{
				e.Position = new RDPointE?(target);
				e.Pivot = new RDPointE?((e.VisualPosition() - new RDSizeE(target)).Rotate(e.Angle.Value.NumericValue));
			}
		}
		/// <summary>
		/// Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		/// </summary>
		/// <param name="target">Specified position. </param>
		public static void MovePositionMaintainVisual(this MoveRoom e, RDSizeE target)
		{
			if (e.RoomPosition != null && e.Pivot != null && e.Angle != null && e.Angle.Value.IsNumeric)
			{
				e.RoomPosition = new RDPointE?((RDPointE)target);
				e.Pivot = new RDPointE?((e.VisualPosition() - new RDSizeE((RDPointE)target)).Rotate(e.Angle.Value.NumericValue));
			}
		}
		/// <summary>
		/// The visual position of the lower left corner of the image.
		/// </summary>
		public static RDPointE VisualPosition(this Move e)
		{
			RDPointE VisualPosition = default;
			if (e.Position != null && e.Pivot != null && e.Angle != null && e.Angle.Value.IsNumeric && e.Scale != null)
			{
				RDPointE previousPosition = e.Position.Value;
				RDPointE? pointE = e.Pivot;
				RDExpression? expression = pointE?.X;
				pointE = e.Scale;
				RDExpression? x = expression * (pointE?.X) * (float)e.Parent.Size.Width / 100f;
				pointE = e.Pivot;
				RDExpression? expression2 = pointE?.Y;
				pointE = e.Scale;
				RDPointE previousPivot = new(x, expression2 * (pointE?.Y) * (float)e.Parent.Size.Height / 100f);
				VisualPosition = previousPosition + new RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue));
			}
			return VisualPosition;
		}
		/// <summary>
		/// The visual position of the lower left corner of the image.
		/// </summary>
		public static RDPointE VisualPosition(this MoveRoom e)
		{
			RDPointE VisualPosition = default;
			if (e.RoomPosition != null && e.Pivot != null && e.Angle != null)
			{
				RDPointE previousPosition = e.RoomPosition.Value;
				RDPointE? pivot = e.Pivot;
				RDExpression? expression = pivot?.X;
				RDSizeE? scale = e.Scale;
				RDExpression? x = expression * (scale?.Width);
				pivot = e.Pivot;
				RDExpression? expression2 = pivot?.Y;
				scale = e.Scale;
				RDPointE previousPivot = new(x, expression2 * (scale?.Height));
				VisualPosition = previousPosition + new RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue));
			}
			return VisualPosition;
		}
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
	}
}
