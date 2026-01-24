using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a custom flash event.
/// </summary>
public record class CustomFlash : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
{
	/// <inheritdoc />
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <inheritdoc />
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <summary>
	/// Gets or sets the start color of the flash.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(StartColor)} is not null")]
	public PaletteColor? StartColor { get; set; }
	/// <summary>
	/// Gets or sets the end color of the flash.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(EndColor)} is not null")]
	public PaletteColor? EndColor { get; set; }
	/// <inheritdoc />
	public float Duration { get; set; }
	/// <summary>
	/// Gets or sets the start opacity of the flash.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(StartOpacity)} is not null")]
	public int? StartOpacity { get; set; }
	/// <summary>
	/// Gets or sets the end opacity of the flash.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(EndOpacity)} is not null")]
	public int? EndOpacity { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the background is affected.
	/// </summary>
	public bool Background { get; set; } = false;
	[RDJsonCondition($"$&.{nameof(ReducedStrength)} is not null")]
	public int? ReducedStrength { get; set; }
	/// <inheritdoc />
	public override EventType Type => EventType.CustomFlash;

	/// <inheritdoc />
	public override Tab Tab => Tab.Actions;

	/// <inheritdoc />
	public override string ToString() => base.ToString() + $" {StartColor} {StartOpacity}%=>{EndColor} {EndOpacity}%";
}
