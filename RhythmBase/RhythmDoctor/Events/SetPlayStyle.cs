namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to set the play style.
/// </summary>
public record class SetPlayStyle : BaseEvent
{
	/// <summary>
	/// Gets or sets the play style.
	/// </summary>
	[RDJsonAlias("PlayStyle")]
	public PlayStyleType PlayStyle { get; set; } = PlayStyleType.Normal;
	/// <summary>
	/// Gets or sets the next bar.
	/// </summary>
	[RDJsonAlias("NextBar")]
	public int NextBar { get; set; } = 1;
	/// <summary>
	/// Gets or sets a value indicating whether the play style is relative.
	/// </summary>
	[RDJsonAlias("Relative")]
	public bool IsRelative { get; set; } = true;
	///<inheritdoc/>
	public override EventType Type => EventType.SetPlayStyle;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}
