namespace RhythmBase.RhythmDoctor.Events;

///<inheritdoc/>
public record class ShowSubdivisionsRows : BaseEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.ShowSubdivisionsRows;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;

	/// <summary>
	/// Gets or sets the number of subdivisions to display.
	/// </summary>
	public int Subdivisions { get; set; } = 1;

	/// <summary>
	/// Gets or sets the arc angle for the subdivision rows.
	/// Only used if not null.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(ArcAngle)} is not null")]
	public int? ArcAngle { get; set; }

	/// <summary>
	/// Gets or sets the spin speed per second for the subdivision rows.
	/// </summary>
	public float SpinPerSecond { get; set; } = -100f;

	/// <summary>
	/// Gets or sets the display mode for the subdivision rows.
	/// </summary>
	public ShowSubdivisionsRowsMode Mode { get; set; } = ShowSubdivisionsRowsMode.Mini;
}
