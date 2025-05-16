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
					string newTag = tag + "\n" + ev.Tag;
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
				ActionTag = $"{RhythmBaseGroupEventHeader}\n{DataType}\n{DataId}",
			};
		}
		/// <summary>
		/// Returns an enumerator that iterates through the events in the group.
		/// </summary>
		/// <returns>An enumerator for the events in the group.</returns>
		IEnumerator<BaseEvent> IEnumerable<BaseEvent>.GetEnumerator() => GenerateTaggedEvents(
			$"{RhythmBaseGroupEventHeader}\n{DataType}\n{DataId}"
			).GetEnumerator();
		/// <summary>
		/// Returns an enumerator that iterates through the events in the group.
		/// </summary>
		/// <returns>An enumerator for the events in the group.</returns>
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<BaseEvent>)this).GetEnumerator();
		internal static bool TryParse(Comment comment, [NotNullWhen(true)] out JObject[]? data)
		{
			if (string.IsNullOrEmpty(comment.Text))
			{
				data = null;
				return false;
			}
			string[] lines = comment.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length < 3 || lines[0] != RhythmBaseGroupDataHeader)
			{
				data = null;
				return false;
			}
			List<JObject> datal = [];
			for (int i = 2; i < lines.Length; i++)
			{
				datal.Add(JObject.Parse(lines[i]));
			}
			data = [.. datal];
			return true;
		}
		internal static bool TryParse(TagAction tagAction, [NotNullWhen(true)] out Group? result)
		{
			if (string.IsNullOrEmpty(tagAction.ActionTag))
			{
				result = null;
				return false;
			}
			string[] lines = tagAction.ActionTag.Split('\n', 3, StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length < 3 || lines[0] != RhythmBaseGroupEventHeader)
			{
				result = null;
				return false;
			}
			string type = lines[1];
			string id = lines[2];
			if (!EventTypeUtils.GroupTypes.TryGetValue(type, out Type? groupType))
				throw new IllegalEventTypeException(type, "This value does not exist in the EventType enumeration.");
			Group group = (Group)Activator.CreateInstance(groupType)!;
			group.DataId = id;
			group.Beat = tagAction.Beat;
			group.Condition = tagAction.Condition;
			group.Active = tagAction.Active;
			result = group;
			return true;
		}
		internal abstract void Flush();
		internal static bool MatchTag(string tag, out string typeName, out string id, out string tagstag)
		{
			if (!string.IsNullOrEmpty(tag))
			{
				string[] args = tag.Split('\n', 3, StringSplitOptions.RemoveEmptyEntries);
				if (args.Length >= 3 && args[0] == RhythmBaseGroupEventHeader)
				{
					typeName = args[1];
					id = args[2];
					tagstag = args.Length > 3 ? args[3] : "";
					return true;
				}
			}
			tagstag = "";
			typeName = "";
			id = "";
			return false;
		}
		internal JObject _data = [];
		internal bool _loaded = false;
		internal object _instance = new();
		internal abstract string DataType { get; }
		internal abstract string DataId { get; set; }
	}
	/// <summary>
	/// Represents a group of events in Rhythm Doctor.
	/// </summary>
	public abstract partial class Group<T> : Group where T : struct
	{
		/// <summary>
		/// Retrieves the events in the group with additional tagging and comments.
		/// </summary>
		/// <exception cref="InvalidRDBeatException">Thrown if any event in the group has an empty beat.</exception>
		protected T Data
		{
			get
			{
				T ins = _loaded ? (T)_instance : _data.ToObject<T>(JsonSerializer.Create(GetSerializer()));
				_instance = ins;
				_loaded = true;
				return ins;
			}
			set
			{
				_instance = value;
				_loaded = true;
			}
		}
		internal override void Flush()
		{
			_instance = _data.ToObject<T>(JsonSerializer.Create(GetSerializer()));
			_data = JObject.FromObject(_instance, JsonSerializer.Create(GetSerializer()));
		}
		/// <summary>  
		/// Initializes a new instance of the <see cref="Group{T}"/> class.  
		/// </summary>  
		/// <remarks>  
		/// This constructor initializes the <see cref="Data"/> property with a new instance of type <typeparamref name="T"/>.  
		/// </remarks>  
		public Group() { }
		internal override string DataType => GetType().Name;
		internal override string DataId { get; set; } = "";
	}
}
