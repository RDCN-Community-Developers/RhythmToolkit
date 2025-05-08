using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a Flash event in the rhythm base.
	/// </summary>
	public class Flash : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Flash"/> class.
		/// </summary>
		public Flash()
		{
			Rooms = new RDRoom(true, [0]);
			Type = EventType.Flash;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the rooms associated with the flash event.
		/// </summary>
		public RDRoom Rooms { get; set; }

		/// <summary>
		/// Gets or sets the duration of the flash event.
		/// </summary>
		public Durations Duration { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Duration);

		/// <summary>
		/// Specifies the possible durations for a flash event.
		/// </summary>
		public enum Durations
		{
			/// <summary>
			/// A short duration.
			/// </summary>
			Short,
			/// <summary>
			/// A medium duration.
			/// </summary>
			Medium,
			/// <summary>
			/// A long duration.
			/// </summary>
			Long
		}
	}
}
