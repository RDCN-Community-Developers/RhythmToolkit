using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to move the camera.
/// </summary>
public record class MoveCamera : BaseEvent, IEaseEvent, IRoomEvent
{
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the camera position.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(CameraPosition)} is not null")]
	public RDPoint? CameraPosition { get; set; }
	/// <summary>
	/// Gets or sets the zoom level.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Zoom)} is not null")]
	public int? Zoom { get; set; }
	/// <summary>
	/// Gets or sets the angle of the camera.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
	public RDExpression? Angle { get; set; }
	///<inheritdoc/>
	public float Duration { get; set; } = 1;
	/// <summary>
	/// Gets or sets a value indicating whether the camera is a real physical device.
	/// </summary>
	[RDJsonAlias("real")]
	[RDJsonCondition($"$&.{nameof(RealCamera)}")]
	public bool RealCamera { get; set; } = false;
	/// <summary>
	/// Gets or sets the window identifier associated with the camera.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(RealCamera)} && $&.{nameof(Window)} != -1")]
	public int Window { get; set; } = -1;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	///<inheritdoc/>
	public override EventType Type => EventType.MoveCamera;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}
