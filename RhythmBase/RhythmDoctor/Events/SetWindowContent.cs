using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an event to set the content of a window.  
/// </summary>  
[RDJsonObjectSerializable]
public record class SetWindowContent : BaseWindowEvent, IEaseEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.SetWindowContent;

	/// <summary>  
	/// Gets or sets the mode for displaying the content.  
	/// Defaults to <see cref="WindowContentMode.OnTop"/>.  
	/// </summary>  
	public WindowContentMode? ContentMode { get; set; } = WindowContentMode.OnTop;
	/// <summary>
	/// Gets or sets the index of the room.
	/// </summary>
	public int RoomIndex { get; set; }
	/// <summary>
	/// Gets or sets the position represented by a two-dimensional point.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Position)} is not null")]
	public RDPoint? Position { get; set; }
	/// <summary>
	/// Gets or sets the zoom level for the content.
	/// </summary>
	/// <remarks>
	/// Percentage of the original size. (100) is the original size.
	/// Leave it null to keep the original zoom level.
	/// </remarks>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Zoom)} is not null")]
	public int? Zoom { get; set; }
	/// <summary>
	/// Gets or sets the angle value.
	/// </summary>
	/// <remarks>
	/// Degree. (0) is the original angle.
	/// Leave it null to keep the original angle.
	/// </remarks>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
	public float? Angle { get; set; }
	///<inheritdoc/>
	public EaseType Ease { get; set; }
	///<inheritdoc/>
	public float Duration { get; set; }
}

