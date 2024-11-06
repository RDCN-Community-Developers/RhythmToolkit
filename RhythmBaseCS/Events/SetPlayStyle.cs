namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to set the play style.
	/// </summary>
	public class SetPlayStyle : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetPlayStyle"/> class.
		/// </summary>
		public SetPlayStyle()
		{
			Type = EventType.SetPlayStyle;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the play style.
		/// </summary>
		public PlayStyles PlayStyle { get; set; }

		/// <summary>
		/// Gets or sets the next bar.
		/// </summary>
		public int NextBar { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the play style is relative.
		/// </summary>
		public bool Relative { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Defines the play styles.
		/// </summary>
		public enum PlayStyles
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
	}
}
