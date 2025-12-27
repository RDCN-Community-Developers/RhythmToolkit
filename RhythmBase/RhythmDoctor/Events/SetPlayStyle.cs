namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to set the play style.
/// </summary>
public class SetPlayStyle : BaseEvent
{
	/// <summary>
	/// Gets or sets the play style.
	/// </summary>
	[RDJsonProperty("PlayStyle")]
	public PlayStyleType PlayStyle { get; set; } = PlayStyleType.Normal;
	/// <summary>
	/// Gets or sets the next bar.
	/// </summary>
	[RDJsonProperty("NextBar")]
	public int NextBar { get; set; } = 1;
	/// <summary>
	/// Gets or sets a value indicating whether the play style is relative.
	/// </summary>
	[RDJsonProperty("Relative")]
	public bool IsRelative { get; set; } = true;
	///<inheritdoc/>
	public override EventType Type => EventType.SetPlayStyle;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}
