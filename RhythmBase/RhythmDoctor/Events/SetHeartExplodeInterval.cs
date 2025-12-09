namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to set the heart explode interval.
/// </summary>
public class SetHeartExplodeInterval : BaseEvent
{
	/// <summary>
	/// Gets or sets the type of interval.
	/// </summary>
	public HeartExplodeIntervalType IntervalType { get; set; } = HeartExplodeIntervalType.GatherAndCeil;
	/// <summary>
	/// Gets or sets the interval value.
	/// </summary>
	public float Interval { get; set; } = 1f;
	///<inheritdoc/>
	public override EventType Type => EventType.SetHeartExplodeInterval;
	///<inheritdoc/>
	public override Tab Tab => Tab.Sounds;
}
/// <summary>
/// Defines the types of intervals.
/// </summary>
[RDJsonEnumSerializable]
public enum HeartExplodeIntervalType
{
	/// <summary>
	/// Interval of one beat after.
	/// </summary>
	OneBeatAfter,
	/// <summary>
	/// Instant interval.
	/// </summary>
	Instant,
	/// <summary>
	/// Gather without ceiling.
	/// </summary>
	GatherNoCeil,
	/// <summary>
	/// Gather and ceiling.
	/// </summary>
	GatherAndCeil
}
