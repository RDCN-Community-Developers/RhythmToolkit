using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set the room perspective.  
	/// </summary>  
	//[RDJsonObjectNotSerializable]
	public class SetRoomPerspective : BaseEvent, IEaseEvent
	{
		private RDPointE?[] cornerPositions = [
			new(0, 0),
			new(100, 0),
			new(0, 100),
			new(100, 100),
		];
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetRoomPerspective"/> class.  
		/// </summary>  
		public SetRoomPerspective() { }
		/// <summary>  
		/// Gets or sets the corner positions of the room.  
		/// </summary>  
		[Tween]
		[RDJsonCondition("$&.CornerPositions is not null")]
		public RDPointE?[] CornerPositions
		{
			get => cornerPositions;
			set => cornerPositions = value?.Length == 4 ? value : throw new RhythmBaseException();
		}
		/// <summary>  
		/// Gets or sets the duration of the event.  
		/// </summary>  
		public float Duration { get; set; } = 1;
		/// <summary>  
		/// Gets or sets the ease type of the event.  
		/// </summary>  
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; } = EventType.SetRoomPerspective;
		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; } = Tabs.Rooms;
		/// <summary>  
		/// Gets the room associated with the event.  
		/// </summary>  
		public RDRoom Room => new RDSingleRoom(checked((byte)Y));
	}
}
