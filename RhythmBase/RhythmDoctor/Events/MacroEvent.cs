using System.Diagnostics.CodeAnalysis;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;
using System.Text.Json;
using static RhythmBase.RhythmDoctor.Utils.Utils;

namespace RhythmBase.RhythmDoctor.Events;
/// <summary>  
/// Represents a base class for grouping events in Rhythm Doctor.  
/// </summary>  
[RDJsonObjectNotSerializable]
public abstract partial class MacroEvent : BaseEvent, IAudioFileEvent, IImageFileEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.MacroEvent;
	///<inheritdoc/>
	public override Tab Tab => Tab.Unknown;
	/// <summary>
	/// Retrieves the collection of events contained within this group.
	/// </summary>
	/// <returns>An enumerable collection of <see cref="BaseEvent"/> objects.</returns>
	public abstract IEnumerable<BaseEvent> GenerateEvents();
	/// <summary>  
	/// Sets the parent row for the specified event.  
	/// </summary>  
	protected static TEvent SetParent<TEvent>(TEvent ev, Row row) where TEvent : BaseRowAction
	{
		ev._row = row.Index;
		return ev;
	}
	/// <summary>  
	/// Sets the parent decoration for the specified event.  
	/// </summary>  
	protected static TEvent SetParent<TEvent>(TEvent ev, Decoration deco) where TEvent : BaseDecorationAction
	{
		ev._decoId = deco.Id;
		return ev;
	}
	/// <summary>  
	/// Gets the collection of rows associated with the current beat's base level.  
	/// </summary>  
	protected RowCollection? Rows => _beat.BaseLevel?.Rows;
	/// <summary>  
	/// Gets the collection of decorations associated with the current beat's base level.  
	/// </summary>  
	protected DecorationCollection? Decorations => _beat.BaseLevel?.Decorations;
	internal IEnumerable<BaseEvent> GenerateTaggedEvents(string tag)
	{
		BaseEvent[] events = [.. GenerateEvents().OrderBy(i => {
			i._beat._calculator = _beat._calculator; return i.Beat;
		})];
		int yMax = events.Max(i => i.Y);
		if (events.Length == 0)
			yield break;
		RDBeat startBeat = events.Min(i => i.Beat);
		Flush();
		HashSet<string> tags = [];
		foreach (BaseEvent ev in events)
		{
			if (ev is SetCrotchetsPerBar)
				throw new RhythmBaseException($"{nameof(SetCrotchetsPerBar)} events are not allowed within a {nameof(MacroEvent)}.");
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
			Action = ActionTagAction.Run,
			ActionTag = $"{RhythmBaseMacroEventHeader}{EventTypeUtils.MacroTypes.IndexOf(GetType()):X8}{DataId:X8}",
		};
	}
#if NETSTANDARD
	internal static bool TryGetTypeData(Comment comment, out string[]? types, out JsonElement[]? data)
#else
		internal static bool TryGetTypeData(Comment comment, [NotNullWhen(true)] out string[]? types, [NotNullWhen(true)] out JsonElement[]? data)
#endif
	{
		if (string.IsNullOrEmpty(comment.Text))
		{
			types = null;
			data = null;
			return false;
		}
		string[] lines = comment.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
		if (lines.Length == 0 || lines[0] != RhythmBaseMacroEventDataHeader)
		{
			types = null;
			data = null;
			return false;
		}
		List<string> typel = [];
		List<JsonElement> datal = [];
		for (int i = 2; i < lines.Length; i++)
		{
#if NETSTANDARD
			if (lines[i].StartsWith("@"))
				typel.Add(lines[i].Substring(1));
			else if (lines[i].StartsWith("{"))
#else
				if (lines[i].StartsWith('@'))
					typel.Add(lines[i][1..]);
				else if (lines[i].StartsWith('{'))
#endif
				datal.Add(JsonSerializer.Deserialize<JsonElement>(lines[i]));
		}
		types = [.. typel];
		data = [.. datal];
		return true;
	}
#if NETSTANDARD
	internal static bool TryParse(TagAction tagAction, string[] types, out MacroEvent? result)
#else
		internal static bool TryParse(TagAction tagAction, string[] types, [NotNullWhen(true)] out MacroEvent? result)
#endif
	{
		if (!TryMatch(tagAction))
		{
			result = null;
			return false;
		}
		string info = tagAction.ActionTag.Substring(RhythmBaseMacroEventHeader.Length, 16);
#if NETSTANDARD
		int typeIndex = Convert.ToInt32(info.Substring(0, 8), 16);
		int id = Convert.ToInt32(info.Substring(8), 16);
#else
			int typeIndex = Convert.ToInt32(info[..8], 16);
			int id = Convert.ToInt32(info[8..], 16);
#endif
		if (typeIndex < 0 || typeIndex >= types.Length)
		{
			result = null;
			return false;
		}
		Type? type = EventTypeUtils.MacroTypes.FirstOrDefault(i => i.FullName == types[typeIndex])
					 ?? throw new IllegalEventTypeException(types[typeIndex], "This value does not exist in the EventType enumeration.");
		MacroEvent group = (MacroEvent)Activator.CreateInstance(type)!;
		group.DataId = id;
		group.Beat = tagAction.Beat;
		group.Y = tagAction.Y;
		group.Condition = tagAction.Condition;
		group.Active = tagAction.Active;
		result = group;
		return true;
	}
	internal static bool TryMatch(TagAction tagAction) => !string.IsNullOrEmpty(tagAction.ActionTag) &&
															tagAction.ActionTag.StartsWith(RhythmBaseMacroEventHeader) &&
															tagAction.ActionTag.Length >= RhythmBaseMacroEventHeader.Length + 16;
	internal abstract void Flush();
	internal static bool MatchTag(string tag, out int type, out int id, out string tagsTag)
	{
		if (!string.IsNullOrEmpty(tag))
		{
			if (tag.StartsWith(RhythmBaseMacroEventHeader))
			{
#if NETSTANDARD
				tag = tag.Substring(RhythmBaseMacroEventHeader.Length);
				type = Convert.ToInt32(tag.Substring(0, 8), 16);
				id = Convert.ToInt32(tag.Substring(8, 8), 16);
				if (tag.Length > 17 && tag[16] == '_')
					tagsTag = tag.Substring(17);
				else
					tagsTag = "";
#else
					tag = tag[RhythmBaseMacroEventHeader.Length..];
					type = Convert.ToInt32(tag[..8], 16);
					id = Convert.ToInt32(tag[8..16], 16);
					if (tag.Length > 17 && tag[16] == '_')
						tagsTag = tag[17..];
					else
						tagsTag = "";
#endif
				return true;
			}
		}
		tagsTag = "";
		type = 0;
		id = 0;
		return false;
	}
	internal JsonElement _data;
	internal bool _instanceLoaded = false;
	internal object _instance = new();
	internal abstract int DataId { get; set; }
	IEnumerable<FileReference> IImageFileEvent.ImageFiles => GenerateEvents().OfType<IImageFileEvent>().SelectMany(i => i.ImageFiles);
	IEnumerable<FileReference> IFileEvent.Files => GenerateEvents().OfType<IFileEvent>().SelectMany(i => i.Files);
	IEnumerable<FileReference> IAudioFileEvent.AudioFiles => GenerateEvents().OfType<IAudioFileEvent>().SelectMany(i => i.AudioFiles);
}
/// <summary>
/// Represents a group of events in Rhythm Doctor.
/// </summary>
public abstract partial class MacroEvent<T> : MacroEvent where T : new()
{
	/// <summary>
	/// Retrieves the events in the group with additional tagging and comments.
	/// </summary>
	/// <exception cref="InvalidRDBeatException">Thrown if any event in the group has an empty beat.</exception>
	protected T Data
	{
		get
		{
			T ins =
				_instanceLoaded ? (T)_instance :
				_data.ValueKind == JsonValueKind.Undefined ? new() :
				_data.Deserialize<T>(GetJsonSerializerOptions()) ?? new();
			_instance = ins;
			_instanceLoaded = true;
			return ins;
		}
		set
		{
			_instance = value ?? new();
			_instanceLoaded = true;
		}
	}
	internal override void Flush()
	{
		if (!_instanceLoaded)
			_instance = _data.Deserialize<T>(GetJsonSerializerOptions()) ?? new();
		_data = JsonSerializer.SerializeToElement(_instance, GetJsonSerializerOptions());
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="MacroEvent{T}"/> class.  
	/// </summary>  
	/// <remarks>  
	/// This constructor initializes the <see cref="Data"/> property with a new instance of type <typeparamref name="T"/>.  
	/// </remarks>  
	public MacroEvent() { }
	internal override int DataId { get; set; }
}