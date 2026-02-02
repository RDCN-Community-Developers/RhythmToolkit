using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that tints rows with specified colors and effects.
/// </summary>
public record class TintRows : BaseRowAnimation, IEaseEvent, IColorEvent, IRoomEvent
{
	/// <summary>
	/// Gets or sets the tint color.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(IsTint)} is true")]
	public PaletteColor TintColor { get; set; } = RDColor.White;
	///<inheritdoc/>
	[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <summary>
	/// Gets or sets the border style.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not null")]
	public Border? Border { get; set; }
	/// <summary>
	/// Gets or sets the border color.
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
	/// Gets or sets the opacity level.
	/// </summary>
	[Tween]
	public int? Opacity { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether to apply tint.
	/// </summary>
	[RDJsonAlias("tint")]
	[RDJsonCondition($"$&.{nameof(IsTint)} is not null")]
	public bool? IsTint { get; set; }
	///<inheritdoc/>
	[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
	public float Duration { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the row effect.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Effect)} is not null")]
	public TintRowEffect? Effect { get; set; } = TintRowEffect.None;
	///<inheritdoc/>
	public override EventType Type => EventType.TintRows;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
	[RDJsonCondition($"$&.{nameof(Heart)} is not null")]
	public HeartType? Heart { get; set; }
	[RDJsonCondition($"$&.{nameof(HeartTransition)} is not null")]
	public bool? HeartTransition { get; set; }
	/// <inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets a value indicating whether to tint all rows.
	/// </summary>
	[RDJsonIgnore]
	public bool TintAll => Parent != null;
	///<inheritdoc/>
	public override string ToString() => base.ToString() +
	                                     $" {Border}{(Border == Events.Border.None ? "" : ":" + BorderColor.ToString())}";
}

