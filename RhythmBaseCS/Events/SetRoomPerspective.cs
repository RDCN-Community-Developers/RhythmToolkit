using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>  
	/// Represents an event to set the room perspective.  
	/// </summary>  
	public class SetRoomPerspective : BaseEvent, IEaseEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetRoomPerspective"/> class.  
		/// </summary>  
		public SetRoomPerspective()
		{
			CornerPositions = new List<RDPointE?>(4);
			Type = EventType.SetRoomPerspective;
			Tab = Tabs.Rooms;
		}

		/// <summary>  
		/// Gets or sets the corner positions of the room.  
		/// </summary>  
		[EaseProperty]
		public List<RDPointE?> CornerPositions { get; set; }

		/// <summary>  
		/// Gets or sets the duration of the event.  
		/// </summary>  
		public float Duration { get; set; }

		/// <summary>  
		/// Gets or sets the ease type of the event.  
		/// </summary>  
		public Ease.EaseType Ease { get; set; }

		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; }

		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; }

		/// <summary>  
		/// Gets the room associated with the event.  
		/// </summary>  
		[JsonIgnore]
		public Room Room
		{
			get
			{
				return new SingleRoom(checked((byte)Y));
			}
		}
	}
}
