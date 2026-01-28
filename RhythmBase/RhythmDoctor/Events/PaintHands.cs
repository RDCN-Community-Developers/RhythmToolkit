using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to paint hands with specified properties.
/// </summary>
public record class PaintHands : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
{
	/// <summary>
	/// Gets or sets the tint color of the hands.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Tint)}")]
	public PaletteColor TintColor { get; set; } = RDColor.White;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <summary>
	/// Gets or sets the border style of the hands.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Border)} is not null")]
	public Border? Border { get; set; }
	/// <summary>
	/// Gets or sets the border color of the hands.
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
	/// Gets or sets the opacity of the hands.
	/// </summary>
	[Tween]
	public int? Opacity { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the hands should be tinted.
	/// </summary>
	public bool? Tint { get; set; }
	///<inheritdoc/>
	public float Duration { get; set; } = 0;
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the player hands associated with the event.
	/// </summary>
	public PlayerHand Hands { get; set; } = PlayerHand.Right;
	///<inheritdoc/>
	public override EventType Type => EventType.PaintHands;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
	///<inheritdoc/>
	public override string ToString() => base.ToString() +
																			 $" {Border}{(Border == Events.Border.None ? "" : ":" + BorderColor.ToString())}";
}
