using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an event to set the room perspective.  
/// </summary>  
[RDJsonObjectSerializable]
public record class SetRoomPerspective : BaseEvent, IEaseEvent
{
    /// <summary>  
    /// Gets or sets the corner positions of the room.  
    /// </summary>  
    /// <remarks>
    /// Percentage of the screen width and height.
    /// (0,0) is the bottom-left corner, (100,100) is the top-right corner.
    /// The order of the corners is bottom-left, bottom-right, top-left, top-right. Leave it null to keep the original corner positions.
    /// </remarks>
    [Tween]
    [RDJsonConverter(typeof(RDCornerConverter))]
    [RDJsonAlias("cornerPositions")]
    public Corner Corner { get; set; }
    ///<inheritdoc/>
    public float Duration { get; set; }
    ///<inheritdoc/>
    public EaseType Ease { get; set; }
    ///<inheritdoc/>
    public override EventType Type => EventType.SetRoomPerspective;
    ///<inheritdoc/>
    public override Tab Tab => Tab.Rooms;

    /// <summary>  
    /// Gets the room associated with the event.  
    /// </summary>  
    public RDRoom Room => new RDSingleRoom(checked((byte)Y));
}
