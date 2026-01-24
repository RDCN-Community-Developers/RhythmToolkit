using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that fades a room.
/// </summary>
public record class FadeRoom : BaseEvent, IEaseEvent
{
	///<inheritdoc/>
	public EaseType Ease { get; set; }
	/// <summary>
	/// Gets or sets the opacity level for the fade effect.
	/// </summary>
	[Tween]
	public int Opacity { get; set; } = 100;
	///<inheritdoc/>
	public float Duration { get; set; }
	///<inheritdoc/>
	public override EventType Type => EventType.FadeRoom;
	///<inheritdoc/>
	public override Tab Tab => Tab.Rooms;
	/// <summary>
	/// Gets the room associated with the event.
	/// </summary>
	public RDRoom Room => new RDSingleRoom((byte)Y);
}
