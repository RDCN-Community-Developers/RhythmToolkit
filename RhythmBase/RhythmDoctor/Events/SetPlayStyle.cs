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
	public PlayStyleTypes PlayStyle { get; set; } = PlayStyleTypes.Normal;
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
/// <summary>
/// Defines the play styles.
/// </summary>
[RDJsonEnumSerializable]
public enum PlayStyleTypes
{
	/// <summary>
	/// Normal play style.
	/// </summary>
	Normal,
	/// <summary>
	/// Loop play style.
	/// </summary>
	Loop,
	/// <summary>
	/// Prolong play style.
	/// </summary>
	Prolong,
	/// <summary>
	/// Immediate play style.
	/// </summary>
	Immediately,
	/// <summary>
	/// Extra immediate play style.
	/// </summary>
	ExtraImmediately,
	/// <summary>
	/// Prolong one bar play style.
	/// </summary>
	ProlongOneBar
}
