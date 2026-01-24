namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to hide a row with specific transitions and visibility options.
/// </summary>
public record class HideRow : BaseRowAnimation
{
	/// <summary>
	/// Gets or sets the transition type for hiding the row.
	/// </summary>
	public Transition Transition { get; set; } = Transition.Smooth;
	/// <summary>
	/// Gets or sets the visibility state of the row.
	/// </summary>
	public ShowTargetType Show { get; set; } = ShowTargetType.Hidden;
	/// <summary>
	/// Gets the type of the event.
	/// </summary>
	public override EventType Type => EventType.HideRow;

	/// <summary>
	/// Gets the tab category of the event.
	/// </summary>
	public override Tab Tab => Tab.Actions;
}