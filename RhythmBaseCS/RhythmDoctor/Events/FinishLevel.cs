namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that occurs when a level is finished.
	/// </summary>
	public class FinishLevel : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FinishLevel"/> class.
		/// </summary>
		public FinishLevel()
		{
			Type = EventType.FinishLevel;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
}
