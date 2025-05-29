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
		public PulseCamera()
		{
			Rooms = new RDRoom(true, [0]);
			Type = EventType.PulseCamera;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }
		/// <summary>
		/// Gets or sets the strength of the pulse.
		/// </summary>
		public byte Strength { get; set; }
		/// <summary>
		/// Gets or sets the count of pulses.
		/// </summary>
		public int Count { get; set; }
		/// <summary>
		/// Gets or sets the frequency of the pulses.
		/// </summary>
		public float Frequency { get; set; }
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
