using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a custom flash event.
	/// </summary>
	public class CustomFlash : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomFlash"/> class.
		/// </summary>
		public CustomFlash() { }
		/// <inheritdoc />
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <inheritdoc />
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets or sets the start color of the flash.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(StartColor)} is not null")]
		public PaletteColor? StartColor { get; set; } = RDColor.White;
		/// <summary>
		/// Gets or sets the end color of the flash.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(EndColor)} is not null")]
		public PaletteColor? EndColor { get; set; } = RDColor.White;
		/// <inheritdoc />
		public float Duration { get; set; } = 2;
		/// <summary>
		/// Gets or sets the start opacity of the flash.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(StartOpacity)} is not null")]
		public int? StartOpacity { get; set; } = 100;
		/// <summary>
		/// Gets or sets the end opacity of the flash.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(EndOpacity)} is not null")]
		public int? EndOpacity { get; set; } = 0;
		/// <summary>
		/// Gets or sets a value indicating whether the background is affected.
		/// </summary>
		public bool Background { get; set; } = false;
		/// <inheritdoc />
		public override EventType Type { get; } = EventType.CustomFlash;
		/// <inheritdoc />
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <inheritdoc />
		public override string ToString() => base.ToString() + $" {StartColor} {StartOpacity}%=>{EndColor} {EndOpacity}%";
	}
}
