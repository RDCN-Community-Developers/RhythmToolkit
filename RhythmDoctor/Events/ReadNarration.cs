namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event for reading narration.
	/// </summary>
	public class ReadNarration : BaseEvent
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.ReadNarration;
		/// <summary>
		/// Gets or sets the text of the narration.
		/// </summary>
		public string Text { get; set; } = "";
		/// <summary>
		/// Gets or sets the category of the narration.
		/// </summary>
		public NarrationCategorys Category { get; set; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Sounds;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
}
