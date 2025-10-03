using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to show rooms.  
	/// </summary>  
	public class ShowRooms : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="ShowRooms"/> class.  
		/// </summary>  
		public ShowRooms() { }
		/// <summary>  
		/// Gets or sets the height configuration for the room.  
		/// </summary>  
		//[JsonIgnore]
		[RDJsonProperty("heights")]
		public RoomHeight Height { get => field; set => field = value; }
		/// <summary>  
		/// Gets or sets the room configuration associated with the height.  
		/// </summary>  
		//[JsonIgnore]
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>  
		/// Gets or sets the ease type for the event.  
		/// </summary>  
		public EaseType Ease { get; set; } = EaseType.InOutSine;
		/// <summary>  
		/// Gets or sets the duration of the transition.  
		/// </summary>  
		[RDJsonProperty("transitionTime")]
		public float Duration { get; set; } = 1;
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; } = EventType.ShowRooms;

		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; } = Tabs.Rooms;
	}
}
