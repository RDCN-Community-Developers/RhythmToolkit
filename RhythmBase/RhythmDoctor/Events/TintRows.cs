using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that tints rows with specified colors and effects.
	/// </summary>
	public class TintRows : BaseRowAnimation, IEaseEvent, IColorEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TintRows"/> class.
		/// </summary>
		public TintRows() { }
		/// <summary>
		/// Gets or sets the tint color.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(IsTint)}")]
		public PaletteColor TintColor { get; set; } = RDColor.White;
		/// <summary>
		/// Gets or sets the easing type for the animation.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets or sets the border style.
		/// </summary>
		public Borders Border { get; set; } = Borders.None;
		/// <summary>
		/// Gets or sets the border color.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Border)} != RhythmBase.RhythmDoctor.Events.Borders.None")]
		public PaletteColor BorderColor { get; set; } = RDColor.White;
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
		/// <summary>
		/// Gets or sets the duration of the tint effect.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Duration)} != 0f")]
		public float Duration { get; set; } = 0f;
		/// <summary>
		/// Gets or sets the row effect.
		/// </summary>
		public TintRowEffects Effect { get; set; } = TintRowEffects.None;
		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type => EventType.TintRows;

		/// <summary>
		/// Gets the tab category.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;

		/// <inheritdoc/>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets a value indicating whether to tint all rows.
		/// </summary>
		[RDJsonIgnore]
		public bool TintAll => Parent != null;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() +
		                                     $" {Border}{(Border == Borders.None ? "" : ":" + BorderColor.ToString())}";
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
}
