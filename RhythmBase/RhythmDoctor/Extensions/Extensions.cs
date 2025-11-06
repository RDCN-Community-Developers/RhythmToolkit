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
			if (e.calculator is BeatCalculator c)
			{
				(int bar, _) = e.Length;
				float start = index.IsFromEnd
					? c.BarBeatToBeatOnly(bar - index.Value, 1f)
					: c.BarBeatToBeatOnly(index.Value, 1f);
				float end = index.IsFromEnd
					? c.BarBeatToBeatOnly(bar - index.Value + 1, 1f)
					: c.BarBeatToBeatOnly(index.Value + 1, 1f);
				return (start, end);
			}
			return (1, 1);

		}
		private static (float start, float end) GetRange(OrderedEventCollection e, Range range)
		{
			if (e.calculator is BeatCalculator c)
			{
				(int bar, _) = e.Length;
				float start = range.Start.IsFromEnd
					? c.BarBeatToBeatOnly(bar - range.Start.Value, 1f)
					: c.BarBeatToBeatOnly(range.Start.Value, 1f);
				float end = range.End.IsFromEnd
					? c.BarBeatToBeatOnly(bar - range.End.Value + 1, 1f)
					: c.BarBeatToBeatOnly(range.End.Value + 1, 1f);
				return (start, end);
			}
			return (1, 1);
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
		public static float FixFraction(this float beat, uint splitBase) => (float)(Math.Round((double)(beat * splitBase)) / splitBase);
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
		/// <param name="beat">The beat.</param>
		/// <param name="splitBase">Indicate what fraction this is.</param>
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
		/// Remove a range of events.
		/// </summary>
		/// <param name="e">Collection</param>
		/// <param name="items">A range of events.</param>
		/// <returns>The number of events successfully removed.</returns>
		public static int RemoveRange<TEvent>(this OrderedEventCollection<TEvent> e, IEnumerable<TEvent> items) where TEvent : IBaseEvent
		{
			int count = 0;
			foreach (TEvent item in items)
				count += e.Remove(item) ? 1 : 0;
			return count;
		}
		/// <summary>
		/// Get all the hit of the level.
		/// </summary>
		public static IEnumerable<RDHit> GetHitBeat(this RDLevel e)
		{
			foreach (Row item in e.Rows)
				foreach (RDHit hit in item.HitBeats())
					yield return hit;
		}
		/// <summary>
		/// Get all the hit event of the level.
		/// </summary>
		public static IEnumerable<BaseBeat> GetHitEvents(this RDLevel e) => e.OfEvent<BaseBeat>().Where(IsHitable);
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
				return ((IEnumerable<TEvent>)e).Where(i => i.Tag == name).GroupBy(i => i.Tag);
			else
				return ((IEnumerable<TEvent>)e).Where(i => i.Tag?.Contains(name) ?? false).GroupBy(i => i.Tag);
		}
		/// <summary>
		/// Get all classic beat events and their variants.
		/// </summary>
		private static IEnumerable<BaseBeat> ClassicBeats(this Row e) => e.OfEvent<BaseBeat>().Where(i => i.Type == EventType.AddClassicBeat | i.Type == EventType.AddFreeTimeBeat | i.Type == EventType.PulseFreeTimeBeat);
		/// <summary>
		/// Get all oneshot beat events.
		/// </summary>
		private static IEnumerable<BaseBeat> OneshotBeats(this Row e) => e.OfEvent<BaseBeat>().Where(i => i.Type == EventType.AddOneshotBeat);
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
				HitBeats = e.OneshotBeats().SelectMany(i => i.HitTimes());
			}
			else
				HitBeats = e.ClassicBeats().SelectMany(i => i.HitTimes());
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
		public static RDBeat BeatOf(this RDLevel e, int bar, float beat) => e.Calculator.BeatOf(bar, beat);
		/// <summary>
		/// Get an instance of the beat associated with the level.
		/// </summary>
		/// <param name="e">RDLevel</param>
		/// <param name="timeSpan">Total time span of the beat.</param>
		public static RDBeat BeatOf(this RDLevel e, TimeSpan timeSpan) => e.Calculator.BeatOf(timeSpan);
		/// <summary>
		/// Gets the depth of the decoration at the specified beat.
		/// </summary>
		/// <param name="e">The decoration instance.</param>
		/// <param name="beat">The beat to query.</param>
		/// <returns>
		/// The depth value at the specified beat, or the decoration's default depth if no <see cref="ReorderSprite"/> event is found.
		/// </returns>
		public static int DepthOf(this Decoration e, RDBeat beat) => e.InRange(new(), beat).OfEvent<ReorderSprite>().LastOrDefault()?.Depth ?? e.Depth;

		/// <summary>
		/// Gets the room index of the decoration at the specified beat.
		/// </summary>
		/// <param name="e">The decoration instance.</param>
		/// <param name="beat">The beat to query.</param>
		/// <returns>
		/// The room index at the specified beat, or the decoration's default room if no <see cref="ReorderSprite"/> event is found.
		/// </returns>
		public static RDRoomIndex RoomOf(this Decoration e, RDBeat beat) => e.InRange(new(), beat).OfEvent<ReorderSprite>().LastOrDefault()?.NewRoom ?? e.Room.Room;

		/// <summary>
		/// Gets the room index of the row at the specified beat.
		/// </summary>
		/// <param name="e">The row instance.</param>
		/// <param name="beat">The beat to query.</param>
		/// <returns>
		/// The room index at the specified beat, or the row's default room if no <see cref="ReorderRow"/> event is found.
		/// </returns>
		public static RDRoomIndex RoomOf(this Row e, RDBeat beat) => e.InRange(new(), beat).OfEvent<ReorderRow>().LastOrDefault()?.NewRoom ?? e.Rooms.Room;
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
		public static IEnumerable<TEvent> Before<TEvent>(this TEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			while (enumerator.MoveNext() && enumerator.Current.Key < e.Beat)
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item as TEvent is TEvent o)
							yield return o;
					}
			}
			if (enumerator.Current.Key != e.Beat)
				yield break;
			if (enumerator.Current.Value.ContainsTypes(types))
				foreach (IBaseEvent item in enumerator.Current.Value)
				{
					if (item == e)
						yield break;
					if (item as TEvent is TEvent o)
						yield return o;
				}
		}
		/// <summary>
		/// Returns all previous events of the specified type, including events of the same beat but executed before itself.
		/// </summary>
		public static IEnumerable<TEvent> Before<TEvent>(this IBaseEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			while (enumerator.MoveNext() && enumerator.Current.Key < e.Beat)
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item as TEvent is TEvent o)
							yield return o;
					}
			}
			if (enumerator.Current.Key != e.Beat || !enumerator.Current.Value.ContainsTypes(types))
				yield break;
			foreach (IBaseEvent item in enumerator.Current.Value)
			{
				if (item == e)
					yield break;
				if (item as TEvent is TEvent o)
					yield return o;
			}
		}

		/// <summary>
		/// Returns all events of the same type that follow, including events of the same beat but executed after itself.
		/// </summary>
		public static IEnumerable<TEvent> After<TEvent>(this TEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			bool moved;
			while ((moved = enumerator.MoveNext()) && enumerator.Current.Key < e.Beat) { }
			if (!moved)
				yield break;
			bool flag = false;
			if (enumerator.Current.Key == e.Beat && enumerator.Current.Value.ContainsTypes(types))
			{
				foreach (IBaseEvent item in enumerator.Current.Value)
				{
					if (flag && item as TEvent is TEvent o)
						yield return o;
					if (item == e)
						flag = true;
				}
			}
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item as TEvent is TEvent o)
							yield return o;
					}
			}
		}

		/// <summary>
		/// Returns all events of the specified type that follow, including events of the same beat but executed after itself.
		/// </summary>
		public static IEnumerable<TEvent> After<TEvent>(this IBaseEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			bool moved;
			while ((moved = enumerator.MoveNext()) && enumerator.Current.Key < e.Beat) { }
			if (!moved)
				yield break;
			bool flag = false;
			if (enumerator.Current.Key == e.Beat && enumerator.Current.Value.ContainsTypes(types))
			{
				foreach (IBaseEvent item in enumerator.Current.Value)
				{
					if (flag && item as TEvent is TEvent o)
						yield return o;
					if (item == e)
						flag = true;
				}
			}
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item as TEvent is TEvent o)
							yield return o;
					}
			}
		}

		/// <summary>
		/// Returns the previous event of the same type, including events of the same beat but executed before itself.
		/// </summary>
		public static TEvent Front<TEvent>(this TEvent e) where TEvent : class, IBaseEvent => e.FrontOrDefault() ?? throw new InvalidOperationException("");
		/// <summary>
		/// Returns the previous event of the specified type, including events of the same beat but executed before itself.
		/// </summary>
		public static TEvent Front<TEvent>(this IBaseEvent e) where TEvent : class, IBaseEvent => e.FrontOrDefault<TEvent>() ?? throw new InvalidOperationException("");
		/// <summary>
		/// Returns the previous event of the same type, including events of the same beat but executed before itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? FrontOrDefault<TEvent>(this TEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			TEvent? front = null;
			while (enumerator.MoveNext() && enumerator.Current.Key < e.Beat)
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item as TEvent is TEvent o)
							front = o;
					}
			}
			if (enumerator.Current.Key != e.Beat || !enumerator.Current.Value.ContainsTypes(types))
				return front;
			foreach (IBaseEvent item in enumerator.Current.Value)
			{
				if (item == e)
					return front;
				if (item as TEvent is TEvent o)
					front = o;
			}
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Returns the previous event of the specified type, including events of the same beat but executed before itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? FrontOrDefault<TEvent>(this IBaseEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			TEvent? front = null;
			while (enumerator.MoveNext() && enumerator.Current.Key < e.Beat)
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item is TEvent o)
							front = o;
					}
			}
			if (enumerator.Current.Key != e.Beat || !enumerator.Current.Value.ContainsTypes(types))
				return front;
			foreach (IBaseEvent item in enumerator.Current.Value)
			{
				if (item == e)
					return front;
				if (item is TEvent o)
					front = o;
			}
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Returns the next event of the same type, including events of the same beat but executed after itself.
		/// </summary>
		public static TEvent Next<TEvent>(this TEvent e) where TEvent : class, IBaseEvent => e.NextOrDefault() ?? throw new InvalidOperationException("");
		/// <summary>
		/// Returns the next event of the specified type, including events of the same beat but executed after itself.
		/// </summary>
		public static TEvent Next<TEvent>(this IBaseEvent e) where TEvent : class, IBaseEvent => e.NextOrDefault<TEvent>() ?? throw new InvalidOperationException("");
		/// <summary>
		/// Returns the next event of the same type, including events of the same beat but executed after itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? NextOrDefault<TEvent>(this TEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			bool moved;
			while ((moved = enumerator.MoveNext()) && enumerator.Current.Key < e.Beat) { }
			if(!moved)
				return null;
			bool flag = false;
			if (enumerator.Current.Key == e.Beat && enumerator.Current.Value.ContainsTypes(types))
			{
				foreach (IBaseEvent item in enumerator.Current.Value)
				{
					if (flag && item is TEvent o)
						return o;
					if (item == e)
						flag = true;
				}
			}
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.Value.ContainsTypes(types))
					continue;
				foreach (IBaseEvent item in enumerator.Current.Value)
				{
					if (item is TEvent o)
						return o;
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the next event of the specified type, including events of the same beat but executed after itself. Returns <see langword="null" /> if it does not exist.
		/// </summary>
		public static TEvent? NextOrDefault<TEvent>(this IBaseEvent e) where TEvent : class, IBaseEvent
		{
			EventType[] types = EventTypeUtils.ToEnums<TEvent>();
			IEnumerator<KeyValuePair<RDBeat,TypedEventCollection<IBaseEvent>>> enumerator = e.Beat.BaseLevel?.eventsBeatOrder.GetEnumerator() ?? throw new InvalidRDBeatException();
			bool moved;
			while ((moved = enumerator.MoveNext()) && enumerator.Current.Key < e.Beat) { }
			if (!moved)
				return null;
			bool flag = false;
			if (enumerator.Current.Key == e.Beat && enumerator.Current.Value.ContainsTypes(types))
			{
				foreach (IBaseEvent item in enumerator.Current.Value)
				{
					if (flag && item is TEvent o)
						return o;
					if (item == e)
						flag = true;
				}
			}
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value.ContainsTypes(types))
					foreach (IBaseEvent item in enumerator.Current.Value)
					{
						if (item is TEvent o)
							return o;
					}
			}
			return null;
		}

		/// <summary>
		/// Compares this <see cref="BaseEvent"/> instance with another <see cref="BaseEvent"/> instance.
		/// </summary>
		/// <param name="e">The current <see cref="BaseEvent"/> instance.</param>
		/// <param name="obj">The <see cref="BaseEvent"/> instance to compare with.</param>
		/// <returns>
		/// <para>0 if both events are the same instance.</para>
		/// <para>-1 if <paramref name="e"/> should be ordered before <paramref name="obj"/>.</para>
		/// <para>1 if <paramref name="e"/> should be ordered after <paramref name="obj"/>.</para>
		/// </returns>
		/// <exception cref="RhythmBaseException">
		/// Thrown if either event has an empty beat, or if the event order cannot be determined.
		/// </exception>
		public static int CompareTo(this BaseEvent e, BaseEvent obj)
		{
			if (e.Beat.IsEmpty || obj.Beat.IsEmpty)
				throw new RhythmBaseException("Cannot compare events with empty beats.");
			if (ReferenceEquals(e, obj))
				return 0;
			if (e.Beat == obj.Beat)
			{
				TypedEventCollection<IBaseEvent> list = (e._beat.BaseLevel?.eventsBeatOrder[e.Beat]) ?? throw new RhythmBaseException("How?");
				return list.CompareTo(e, obj) ? -1 : 1;
			}
			return e.Beat.CompareTo(obj.Beat);
		}
		/// <summary>
		/// Shallow copy.
		/// </summary>
		public static TEvent MemberwiseClone<TEvent>(this TEvent e) where TEvent : IBaseEvent, new() => (TEvent)e.MClone();
		internal static object MClone(this object e)
		{
			if (e == null)
				throw new NullReferenceException();
			Type type = e.GetType();
			object copy = Activator.CreateInstance(type)!;
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (PropertyInfo p in properties)
				if (p.CanWrite)
					p.SetValue(copy, p.GetValue(e));
			return copy;
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
