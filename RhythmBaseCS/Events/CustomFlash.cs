using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents a custom flash event.
	/// </summary>
	public class CustomFlash : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomFlash"/> class.
		/// </summary>
		public CustomFlash()
		{
			Rooms = new Room(true, new byte[1]);
			StartColor = new PaletteColor(false);
			EndColor = new PaletteColor(false);
			Type = EventType.CustomFlash;
			Tab = Tabs.Actions;
		}

		/// <inheritdoc />
		public Room Rooms { get; set; }

		/// <inheritdoc />
		public Ease.EaseType Ease { get; set; }

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
