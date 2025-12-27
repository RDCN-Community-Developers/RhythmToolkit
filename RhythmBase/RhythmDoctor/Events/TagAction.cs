using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a tag action event.
/// </summary>
public class TagAction : BaseEvent
{
	/// <summary>
	/// Gets or sets the action associated with the tag.
	/// </summary>
	[RDJsonProperty("Action")]
	[RDJsonConverter(typeof(TagActionTypeConverter))]
	public ActionTagAction Action { get; set; } = ActionTagAction.Run;
	/// <summary>
	/// Gets or sets the action tag.
	/// </summary>
	[RDJsonProperty("Tag")]
	public string ActionTag { get; set; } = "";
	/// <summary>
	/// Gets the type of the event.
	/// </summary>
	public override EventType Type => EventType.TagAction;

	/// <summary>
	/// Gets the tab associated with the event.
	/// </summary>
	public override Tab Tab => Tab.Actions;

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString() => base.ToString() + $" {ActionTag}";
}
