using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using static RhythmBase.RhythmDoctor.Utils.Utils;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents a base class for grouping events in Rhythm Doctor.  
	/// </summary>  
	/// <remarks>  
	/// This class provides functionality for managing and processing collections of events,  
	/// including tagging, parent association, and event generation.  
	/// </remarks>
	public abstract partial class Group : BaseEvent, IEnumerable<BaseEvent>
	{
		/// <summary>
		/// Gets the type of the event, which is always <see cref="EventType.Group"/> for this class.
		/// </summary>
		public override EventType Type => EventType.Group;
		/// <summary>
		/// Gets the tab associated with this event, which is <see cref="Tabs.Unknown"/> by default.
		/// </summary>
		public override Tabs Tab => Tabs.Unknown;
		/// <summary>  
		/// Initializes a new instance of the <see cref="Group"/> class.  
		/// </summary>  
		internal Group() { }
		/// <summary>
		/// Retrieves the collection of events contained within this group.
		/// </summary>
		/// <returns>An enumerable collection of <see cref="BaseEvent"/> objects.</returns>
		public abstract IEnumerable<BaseEvent> GenerateEvents();
		/// <summary>  
		/// Sets the parent row for the specified event.  
		/// </summary>  
		/// <typeparam name="TEvent">The type of the event, which must inherit from <see cref="BaseRowAction"/>.</typeparam>  
		/// <param name="ev">The event to set the parent for.</param>  
		/// <param name="row">The parent row to associate with the event.</param>  
		/// <returns>The event with its parent row set.</returns>  
		protected static TEvent SetParent<TEvent>(TEvent ev, Row row) where TEvent : BaseRowAction
		{
			ev._row = row.Index;
			return ev;
		}
		/// <summary>  
		/// Sets the parent decoration for the specified event.  
		/// </summary>  
		/// <typeparam name="TEvent">The type of the event, which must inherit from <see cref="BaseDecorationAction"/>.</typeparam>  
		/// <param name="ev">The event to set the parent for.</param>  
		/// <param name="deco">The parent decoration to associate with the event.</param>  
		/// <returns>The event with its parent decoration set.</returns>  
		protected static TEvent SetParent<TEvent>(TEvent ev, Decoration deco) where TEvent : BaseDecorationAction
		{
			ev._decoId = deco.Id;
			return ev;
		}
		/// <summary>  
		/// Gets the collection of rows associated with the current beat's base level.  
		/// </summary>  
		/// <remarks>  
		/// This property retrieves the rows from the base level of the current beat.  
		/// If the base level is null, the property will return null.  
		/// </remarks>  
		protected RowCollection? Rows => _beat.BaseLevel?.Rows;
		/// <summary>  
		/// Gets the collection of decorations associated with the current beat's base level.  
		/// </summary>  
		/// <remarks>  
		/// This property retrieves the decorations from the base level of the current beat.  
		/// If the base level is null, the property will return null.  
		/// </remarks>  
		protected DecorationCollection? Decorations => _beat.BaseLevel?.Decorations;
		private IEnumerable<BaseEvent> GenerateTaggedEvents(string tag)
		{
			BaseEvent[] events = [.. GenerateEvents().OrderBy(i => {
				i._beat._calculator = _beat._calculator; return i.Beat;
			})];
			int yMax = events.Max(i => i.Y);
			if (events.Length == 0)
				yield break;
			var startBeat = events.Min(i => i.Beat);
			Flush();
			HashSet<string> tags = [];
			foreach (BaseEvent ev in events)
			{
				if (string.IsNullOrEmpty(ev.Tag))
					ev.Tag = tag;
				else
				{
					string preTag = ev.Tag;
					string newTag = tag + "_" + ev.Tag;
					if (tags.Add(newTag))
					{
						TagAction action = new()
						{
							Beat = ev.Beat,
							Tag = preTag,
							ActionTag = newTag,
						};
						yield return action;
					}
					ev.Tag = newTag;
				}
				ev.Y -= yMax + 1;
				yield return ev;
			}
		}
		internal TagAction GenerateTagAction()
		{
			return new TagAction()
			{
				Beat = Beat,
				Y = Y,
				Tag = Tag,
				Condition = Condition,
				Active = Active,
				Action = TagAction.Actions.Run,
				ActionTag = $"{RhythmBaseGroupEventHeader}{EventTypeUtils.GroupTypes.IndexOf(GetType()):X8}{DataId:X8}",
			};
		}
		/// <summary>
		/// Returns an enumerator that iterates through the events in the group.
		/// </summary>
		/// <returns>An enumerator for the events in the group.</returns>
		IEnumerator<BaseEvent> IEnumerable<BaseEvent>.GetEnumerator() => GenerateTaggedEvents(
			$"{RhythmBaseGroupEventHeader}{EventTypeUtils.GroupTypes.IndexOf(GetType()):X8}{DataId:X8}"
			).GetEnumerator();
		/// <summary>
		/// Returns an enumerator that iterates through the events in the group.
		/// </summary>
		/// <returns>An enumerator for the events in the group.</returns>
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<BaseEvent>)this).GetEnumerator();
		internal static bool TryParse(Comment comment, [NotNullWhen(true)] out string[]? types, [NotNullWhen(true)] out JObject[]? data)
		{
			if (string.IsNullOrEmpty(comment.Text))
			{
				types = null;
				data = null;
				return false;
			}
			string[] lines = comment.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length < 3 || lines[0] != RhythmBaseGroupDataHeader)
			{
				types = null;
				data = null;
				return false;
			}
			List<string> typel = [];
			List<JObject> datal = [];
			for (int i = 2; i < lines.Length; i++)
			{
				if (lines[i].StartsWith('@'))
					typel.Add(lines[i][1..]);
				else
					datal.Add(JObject.Parse(lines[i]));
			}
			types = [.. typel];
			data = [.. datal];
			return true;
		}
		internal static bool TryParse(TagAction tagAction, string[] types, [NotNullWhen(true)] out Group? result)
		{
			if (!TryMatch(tagAction))
			{
				result = null;
				return false;
			}
			string info = tagAction.ActionTag.Substring(RhythmBaseGroupEventHeader.Length, 16);
			int typeIndex = Convert.ToInt32(info[..8], 16);
			int id = Convert.ToInt32(info[8..], 16);
			if (typeIndex < 0 || typeIndex >= types.Length)
			{
				result = null;
				return false;
			}
			Type? type = EventTypeUtils.GroupTypes.FirstOrDefault(i => i.FullName == types[typeIndex])
				?? throw new IllegalEventTypeException(types[typeIndex], "This value does not exist in the EventType enumeration.");
			Group group = (Group)Activator.CreateInstance(type)!;
			group.DataId = id;
			group.Beat = tagAction.Beat;
			group.Y = tagAction.Y;
			group.Condition = tagAction.Condition;
			group.Active = tagAction.Active;
			result = group;
			return true;
		}
		internal static bool TryMatch(TagAction tagAction) => !string.IsNullOrEmpty(tagAction.ActionTag) &&
				tagAction.ActionTag.StartsWith(RhythmBaseGroupEventHeader) &&
				tagAction.ActionTag.Length >= RhythmBaseGroupEventHeader.Length + 16;
		internal abstract void Flush();
		internal static bool MatchTag(string tag, out int type, out int id, out string tagstag)
		{
			if (!string.IsNullOrEmpty(tag))
			{
				if (tag.StartsWith(RhythmBaseGroupEventHeader))
				{
					tag = tag[RhythmBaseGroupEventHeader.Length..];
					type = Convert.ToInt32(tag[..8], 16);
					id = Convert.ToInt32(tag[8..16], 16);
					if (tag.Length > 17 && tag[16] == '_')
						tagstag = tag[17..];
					else
						tagstag = "";
					return true;
				}
			}
			tagstag = "";
			type = 0;
			id = 0;
			return false;
		}
		internal JObject _data = [];
		internal bool _loaded = false;
		internal object _instance = new();
		internal abstract int DataId { get; set; }
	}
	/// <summary>
	/// Represents a group of events in Rhythm Doctor.
	/// </summary>
	public abstract partial class Group<T> : Group where T : new()
	{
		/// <summary>
		/// Retrieves the events in the group with additional tagging and comments.
		/// </summary>
		/// <exception cref="InvalidRDBeatException">Thrown if any event in the group has an empty beat.</exception>
		protected T Data
		{
			get
			{
				T ins = _loaded ? (T)_instance : _data.ToObject<T>(JsonSerializer.Create(GetSerializer())) ?? new();
				_instance = ins;
				_loaded = true;
				return ins;
			}
			set
			{
				_instance = value ?? new();
				_loaded = true;
			}
		}
		internal override void Flush()
		{
			_instance = _data.ToObject<T>(JsonSerializer.Create(GetSerializer())) ?? new();
			_data = JObject.FromObject(_instance, JsonSerializer.Create(GetSerializer()));
		}
		/// <summary>  
		/// Initializes a new instance of the <see cref="Group{T}"/> class.  
		/// </summary>  
		/// <remarks>  
		/// This constructor initializes the <see cref="Data"/> property with a new instance of type <typeparamref name="T"/>.  
		/// </remarks>  
		public Group() { }
		internal override int DataId { get; set; }
	}
}
