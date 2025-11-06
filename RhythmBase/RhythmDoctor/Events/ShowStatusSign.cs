namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to show a status sign.
	/// </summary>
	public class ShowStatusSign : BaseEvent, IDurationEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShowStatusSign"/> class.
		/// </summary>
		public ShowStatusSign()
		{
		}
		/// <summary>
		/// Gets or sets a value indicating whether to use beats.
		/// </summary>
		public bool UseBeats { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether to narrate.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Narrate)}")]
		public bool Narrate { get; set; } = true;
		/// <summary>
		/// Gets or sets the text to display.
		/// </summary>
		public string Text { get; set; } = "";
		/// <summary>
		/// Gets or sets the duration of the status sign in seconds.
		/// </summary>
		public float Duration { get; set; } = 4f;
		/// <summary>
		/// Gets or sets the duration of the status sign as a <see cref="TimeSpan"/>.
		/// </summary>
		[RDJsonIgnore]
		public TimeSpan TimeDuration
		{
			get
			{
				bool useBeats = UseBeats;
				TimeSpan timeDuration;
				timeDuration = useBeats ? TimeSpan.Zero : TimeSpan.FromSeconds((double)Duration);
				return timeDuration;
			}
			set
			{
				UseBeats = false;
				Duration = (float)value.TotalSeconds;
			}
		}
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.ShowStatusSign;
		/// <summary>
		/// Gets the tab of the event.
		/// </summary>
		public override Tabs Tab { get; } =  Tabs.Actions;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Text}";
	}
}
