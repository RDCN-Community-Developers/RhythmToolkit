namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the heart explode interval.
	/// </summary>
	public class SetHeartExplodeInterval : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetHeartExplodeInterval"/> class.
		/// </summary>
		public SetHeartExplodeInterval()
		{
			Type = EventType.SetHeartExplodeInterval;
			Tab = Tabs.Sounds;
		}
		/// <summary>
		/// Gets or sets the type of interval.
		/// </summary>
		public HeartExplodeIntervalTypes IntervalType { get; set; }
		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		public float Interval { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
	/// <summary>
	/// Defines the types of intervals.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum HeartExplodeIntervalTypes
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
}
