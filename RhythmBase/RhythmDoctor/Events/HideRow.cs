namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to hide a row with specific transitions and visibility options.
/// </summary>
public class HideRow : BaseRowAnimation
{
	/// <summary>
	/// Gets or sets the transition type for hiding the row.
	/// </summary>
	public ObjectTransitionTypes Transition { get; set; } = ObjectTransitionTypes.Smooth;
	/// <summary>
	/// Gets or sets the visibility state of the row.
	/// </summary>
	public ShowTargetTypes Show { get; set; } = ShowTargetTypes.Hidden;
	/// <summary>
	/// Gets the type of the event.
	/// </summary>
	public override EventType Type => EventType.HideRow;

	/// <summary>
	/// Gets the tab category of the event.
	/// </summary>
	public override Tab Tab => Tab.Actions;
}
/// <summary>
/// Defines the possible transition types for hiding the row.
/// </summary>
[RDJsonEnumSerializable]
public enum ObjectTransitionTypes
{
	/// <summary>
	/// Smooth transition.
	/// </summary>
	Smooth,
	/// <summary>
	/// Instant transition.
	/// </summary>
	Instant,
	/// <summary>
	/// Full transition.
	/// </summary>
	Full
}
/// <summary>
/// Defines the possible visibility states of the row.
/// </summary>
[RDJsonEnumSerializable]
public enum ShowTargetTypes
{
	/// <summary>
	/// Row is visible.
	/// </summary>
	Visible,
	/// <summary>
	/// Row is hidden.
	/// </summary>
	Hidden,
	/// <summary>
	/// Only the character is visible.
	/// </summary>
	OnlyCharacter,
	/// <summary>
	/// Only the row is visible.
	/// </summary>
	OnlyRow
}
