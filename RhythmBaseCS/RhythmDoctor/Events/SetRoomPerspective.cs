﻿using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set the room perspective.  
	/// </summary>  
	public class SetRoomPerspective : BaseEvent, IEaseEvent
	{
		private RDPointE?[] cornerPositions = [
			new(0,0),
			new(100,0),
			new(0,100),
			new(100,100),
		];
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetRoomPerspective"/> class.  
		/// </summary>  
		public SetRoomPerspective()
		{
			Type = EventType.SetRoomPerspective;
			Tab = Tabs.Rooms;
		}
		/// <summary>  
		/// Gets or sets the corner positions of the room.  
		/// </summary>  
		[EaseProperty]
		public RDPointE?[] CornerPositions { get => cornerPositions; set => cornerPositions = value.Length == 4 ? value : throw new RhythmBaseException(); }

		/// <summary>  
		/// Gets or sets the duration of the event.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the ease type of the event.  
		/// </summary>  
		public EaseType Ease { get; set; }
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
		public RDRoom Room => new RDSingleRoom(checked((byte)Y));
	}
}
