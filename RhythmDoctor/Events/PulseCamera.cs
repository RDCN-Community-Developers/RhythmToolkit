using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a camera pulse event in a room.
	/// </summary>
	public class PulseCamera : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PulseCamera"/> class.
		/// </summary>
		public PulseCamera() { }
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the strength of the pulse.
		/// </summary>
		public byte Strength { get; set; } = 1;
		/// <summary>
		/// Gets or sets the count of pulses.
		/// </summary>
		public int Count { get; set; } = 1;
		/// <summary>
		/// Gets or sets the frequency of the pulses.
		/// </summary>
		public float Frequency { get; set; } = 1;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.PulseCamera;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
	}
}
