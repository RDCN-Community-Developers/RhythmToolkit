using Newtonsoft.Json;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to show a status sign.
	/// </summary>
	public class ShowStatusSign : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShowStatusSign"/> class.
		/// </summary>
		public ShowStatusSign()
		{
			UseBeats = true;
			Narrate = true;
			Type = EventType.ShowStatusSign;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets a value indicating whether to use beats.
		/// </summary>
		public bool UseBeats { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to narrate.
		/// </summary>
		public bool Narrate { get; set; }

		/// <summary>
		/// Gets or sets the text to display.
		/// </summary>
		public string Text { get; set; } = "";

		/// <summary>
		/// Gets or sets the duration of the status sign in seconds.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets or sets the duration of the status sign as a <see cref="TimeSpan"/>.
		/// </summary>
		[JsonIgnore]
		public TimeSpan TimeDuration
		{
			get
			{
				bool useBeats = UseBeats;
				TimeSpan TimeDuration;
				if (useBeats)
				{
					TimeDuration = TimeSpan.Zero;
				}
				else
				{
					TimeDuration = TimeSpan.FromSeconds((double)Duration);
				}
				return TimeDuration;
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
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab of the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
}
