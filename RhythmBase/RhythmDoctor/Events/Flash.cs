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
		public Flash() { }
		/// <summary>
		/// Gets or sets the rooms associated with the flash event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the duration of the flash event.
		/// </summary>
		public DurationType Duration { get; set; } = DurationType.Short;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.Flash;

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Duration}";
	}
	/// <summary>
	/// Specifies the possible durations for a flash event.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum DurationType
	{
		/// <summary>
		/// A short duration.
		/// </summary>
		Short = 1,
		/// <summary>
		/// A medium duration.
		/// </summary>
		Medium = 2,
		/// <summary>
		/// A long duration.
		/// </summary>
		Long = 4,
	}
}
