using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a Tint event which is a type of BaseDecorationAction and implements IEaseEvent.
	/// </summary>
	public class Tint : BaseDecorationAction, IEaseEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tint"/> class.
		/// </summary>
		public Tint() { }
		/// <summary>
		/// Gets or sets the ease type for the tint event.
		/// </summary>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets or sets the border type for the tint event.
		/// </summary>
		public Border Border { get; set; } = Border.None;
		/// <summary>
		/// Gets or sets the border color for the tint event.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)}")]
		public PaletteColor BorderColor { get; set; } = RDColor.White;
		[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)} && $&.{nameof(BorderPulse)}")]
		public bool BorderPulse { get; set; }
		[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)} && $&.{nameof(BorderPulse)}")]
		public float BorderPulseMin { get; set; }
		[RDJsonCondition($"$&.{nameof(Border)} is not RhythmBase.RhythmDoctor.Events.{nameof(Events.Border)}.{nameof(Border.None)} && $&.{nameof(BorderPulse)}")]
		public float BorderPulseMax { get; set; }
		/// <summary>
		/// Gets or sets the opacity for the tint event.
		/// </summary>
		[Tween]
		public int Opacity { get; set; } = 100;
		/// <summary>
		/// Gets or sets a value indicating whether this event is a tint.
		/// </summary>
		[RDJsonProperty("tint")]
		public bool IsTint { get; set; } = false;
		/// <summary>
		/// Gets or sets the tint color for the tint event.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(IsTint)}")]
		public PaletteColor TintColor { get; set; } = RDColor.White;
		/// <summary>
		/// Gets or sets the duration of the tint event.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
		public float Duration { get; set; } = 0f;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.Tint;

		/// <summary>
		/// Gets the tab where the event is categorized.
		/// </summary>
		public override Tabs Tab => Tabs.Decorations;

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() +
		                                     $" {Border}{(Border == Border.None ? "" : ":" + BorderColor.ToString())}";
	}
}
