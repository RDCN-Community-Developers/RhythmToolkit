using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a Tint event which is a type of BaseDecorationAction and implements IEaseEvent.
/// </summary>
public record class Tint : BaseDecorationAction, IEaseEvent, IColorEvent
{
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <summary>
	/// Gets or sets the border type for the tint event.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not null")]
	public Border? Border { get; set; }
	/// <summary>
	/// Gets or sets the border color for the tint event.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Border)} is not null and not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Events.Border.None)}")]
	public PaletteColor BorderColor { get; set; } = RDColor.White;
	/// <summary>
	/// Gets or sets a value indicating whether the hand border should pulse.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not null and not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Events.Border.None)} && $&.{nameof(BorderPulse)}")]
	public bool BorderPulse { get; set; }
	/// <summary>
	/// Gets or sets the minimum value for the border pulse effect.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not null and not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Events.Border.None)} && $&.{nameof(BorderPulse)}")]
	public float BorderPulseMin { get; set; }
	/// <summary>
	/// Gets or sets the maximum value for the border pulse effect.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not null and not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Events.Border.None)} && $&.{nameof(BorderPulse)}")]
	public float BorderPulseMax { get; set; }
	/// <summary>
	/// Gets or sets the opacity for the tint event.
	/// </summary>
	[Tween]
	public int? Opacity { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether this event is a tint.
	/// </summary>
	[RDJsonAlias("tint")]
	public bool? IsTint { get; set; }
	/// <summary>
	/// Gets or sets the tint color for the tint event.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(IsTint)}")]
	public PaletteColor TintColor { get; set; } = RDColor.White;
	///<inheritdoc/>
	[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
	public float Duration { get; set; } = 0f;
	///<inheritdoc/>
	public override EventType Type => EventType.Tint;
	///<inheritdoc/>
	public override Tab Tab => Tab.Decorations;
	///<inheritdoc/>
	public override string ToString() => base.ToString() +
	                                     $" {Border}{(Border == Events.Border.None ? "" : ":" + BorderColor.ToString())}";
}
