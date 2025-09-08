using Newtonsoft.Json;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that fades a room.
	/// </summary>
	public class FadeRoom : BaseEvent, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FadeRoom"/> class.
		/// </summary>
		public FadeRoom() { }
		/// <summary>
		/// Gets or sets the easing type for the fade effect.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the opacity level for the fade effect.
		/// </summary>
		[EaseProperty]
		public uint Opacity { get; set; }
		/// <summary>
		/// Gets or sets the duration of the fade effect.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.FadeRoom;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Rooms;
		/// <summary>
		/// Gets the room associated with the event.
		/// </summary>
		[JsonIgnore]
		public RDRoom Room => new RDSingleRoom((byte)Y);
	}
}
