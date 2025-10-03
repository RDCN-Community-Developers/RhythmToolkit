using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that inverts colors in a room.
	/// </summary>
	public class InvertColors : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvertColors"/> class.
		/// </summary>
		public InvertColors() { }
		/// <summary>
		/// Gets or sets the rooms associated with this event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets a value indicating whether the color inversion is enabled.
		/// </summary>
		public bool Enable { get; set; } = true;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.InvertColors;
		/// <summary>
		/// Gets the tab associated with this event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Enable}";
	}
}
