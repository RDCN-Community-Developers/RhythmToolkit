using RhythmBase.Global.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event for reading narration.
	/// </summary>
	public class ReadNarration : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadNarration"/> class.
		/// </summary>
		public ReadNarration()
		{
			Type = EventType.ReadNarration;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets or sets the text of the narration.
		/// </summary>
		public string Text { get; set; } = "";
		/// <summary>
		/// Gets or sets the category of the narration.
		/// </summary>
		public NarrationCategory Category { get; set; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
		/// <summary>
		/// Specifies the category of the narration.
		/// </summary>
		public enum NarrationCategory
		{
			/// <summary>
			/// Fallback category.
			/// </summary>
			Fallback,
			/// <summary>
			/// Navigation category.
			/// </summary>
			Navigation,
			/// <summary>
			/// Instruction category.
			/// </summary>
			Instruction,
			/// <summary>
			/// Notification category.
			/// </summary>
			Notification,
			/// <summary>
			/// Dialogue category.
			/// </summary>
			Dialogue,
			/// <summary>
			/// Description category.
			/// </summary>
			Description = 6,
			/// <summary>
			/// Subtitles category.
			/// </summary>
			Subtitles
		}
	}
}
