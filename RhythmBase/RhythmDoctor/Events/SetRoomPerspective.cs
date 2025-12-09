using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an event to set the room perspective.  
/// </summary>  
public class SetRoomPerspective : BaseEvent, IEaseEvent
{
	private RDPoint[] cornerPositions = [
		new(0, 0),
		new(100, 0),
		new(0, 100),
		new(100, 100),
	];
	/// <summary>  
	/// Gets or sets the corner positions of the room.  
	/// </summary>  
	[Tween]
	[RDJsonConverter(typeof(RDPointArrayConverter<RDPoint>))]
	[RDJsonCondition("$&.CornerPositions is not null")]
	public RDPoint[] CornerPositions
	{
		get => cornerPositions;
		set => cornerPositions = value?.Length == 4 ? value : throw new RhythmBaseException();
	}
	///<inheritdoc/>
	public float Duration { get; set; } = 1;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	///<inheritdoc/>
	public override EventType Type => EventType.SetRoomPerspective;
	///<inheritdoc/>
	public override Tab Tab => Tab.Rooms;

	/// <summary>  
	/// Gets the room associated with the event.  
	/// </summary>  
	public RDRoom Room => new RDSingleRoom(checked((byte)Y));
}
