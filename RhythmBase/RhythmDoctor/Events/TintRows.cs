using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that tints rows with specified colors and effects.
/// </summary>
public class TintRows : BaseRowAnimation, IEaseEvent, IColorEvent, IRoomEvent
{
	/// <summary>
	/// Gets or sets the tint color.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(IsTint)}")]
	public PaletteColor TintColor { get; set; } = RDColor.White;
	///<inheritdoc/>
	[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <summary>
	/// Gets or sets the border style.
	/// </summary>
	public Border Border { get; set; } = Border.None;
	/// <summary>
	/// Gets or sets the border color.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)}")]
	public PaletteColor BorderColor { get; set; } = RDColor.White;
	/// <summary>
	/// Gets or sets a value indicating whether the hand border should pulse.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)} && $&.{nameof(BorderPulse)}")]
	public bool BorderPulse { get; set; }
	/// <summary>
	/// Gets or sets the minimum value for the border pulse effect.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)} && $&.{nameof(BorderPulse)}")]
	public float BorderPulseMin { get; set; }
	/// <summary>
	/// Gets or sets the maximum value for the border pulse effect.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)} && $&.{nameof(BorderPulse)}")]
	public float BorderPulseMax { get; set; }
	/// <summary>
	/// Gets or sets the opacity level.
	/// </summary>
	[Tween]
	public int Opacity { get; set; } = 100;
	/// <summary>
	/// Gets or sets a value indicating whether to apply tint.
	/// </summary>
	[RDJsonProperty("tint")]
	public bool IsTint { get; set; } = false;
	///<inheritdoc/>
	[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
	public float Duration { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the row effect.
	/// </summary>
	public TintRowEffects Effect { get; set; } = TintRowEffects.None;
	///<inheritdoc/>
	public override EventType Type => EventType.TintRows;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;

	/// <inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets a value indicating whether to tint all rows.
	/// </summary>
	[RDJsonIgnore]
	public bool TintAll => Parent != null;
	///<inheritdoc/>
	public override string ToString() => base.ToString() +
	                                     $" {Border}{(Border == Border.None ? "" : ":" + BorderColor.ToString())}";
}

/// <summary>
/// Specifies the row effects.
/// </summary>
[RDJsonEnumSerializable]
public enum TintRowEffects
{
	/// <summary>
	/// No effect.
	/// </summary>
	None,
	/// <summary>
	/// Electric effect.
	/// </summary>
	Electric,
	/// <summary>
	/// Smoke effect.
	/// </summary>
	Smoke
}
