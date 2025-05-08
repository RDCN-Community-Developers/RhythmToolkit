using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
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
		public CustomFlash()
		{
			Rooms = new RDRoom(true, [0]);
			StartColor = new PaletteColor(false);
			EndColor = new PaletteColor(false);
			StartOpacity = 100;
			EndOpacity = 100;
			Type = EventType.CustomFlash;
			Tab = Tabs.Actions;
		}
		/// <inheritdoc />
		public RDRoom Rooms { get; set; }
		/// <inheritdoc />
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the start color of the flash.
		/// </summary>
		public PaletteColor StartColor { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the background is affected.
		/// </summary>
		public bool Background { get; set; }
		/// <summary>
		/// Gets or sets the end color of the flash.
		/// </summary>
		[EaseProperty]
		public PaletteColor EndColor { get; set; }
		/// <inheritdoc />
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the start opacity of the flash.
		/// </summary>
		public int StartOpacity { get; set; }
		/// <summary>
		/// Gets or sets the end opacity of the flash.
		/// </summary>
		[EaseProperty]
		public int EndOpacity { get; set; }
		/// <inheritdoc />
		public override EventType Type { get; }
		/// <inheritdoc />
		public override Tabs Tab { get; }
		/// <inheritdoc />
		public override string ToString() => base.ToString() + $" {StartColor} {StartOpacity}%=>{EndColor} {EndOpacity}%";
	}
}
