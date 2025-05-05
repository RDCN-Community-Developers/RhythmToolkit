using RhythmBase.Components;
using RhythmBase.Components.Easing;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to set the background color.
	/// </summary>
	public class SetBackgroundColor : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetBackgroundColor"/> class.
		/// </summary>
		public SetBackgroundColor()
		{
			Rooms = new RDRoom(false, [0]);
			Color = new PaletteColor(true);
			Type = EventType.SetBackgroundColor;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }

		/// <summary>
		/// Gets or sets the easing type for the event.
		/// </summary>
		public EaseType Ease { get; set; }

		/// <summary>
		/// Gets or sets the content mode for the event.
		/// </summary>
		public ContentModes ContentMode { get; set; }

		/// <summary>
		/// Gets or sets the filter mode for the event.
		/// </summary>
		public FilterModes Filter { get; set; }

		/// <summary>
		/// Gets or sets the color for the background.
		/// </summary>
		[EaseProperty]
		public PaletteColor Color { get; set; }

		/// <summary>
		/// Gets or sets the interval for the event.
		/// </summary>
		public float Interval { get; set; }

		/// <summary>
		/// Gets or sets the background type for the event.
		/// </summary>
		public BackgroundTypes BackgroundType { get; set; }

		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets or sets the frames per second for the event.
		/// </summary>
		public int Fps { get; set; }

		/// <summary>
		/// Gets or sets the list of images for the background.
		/// </summary>
		public List<string> Image { get; set; } = [];

		/// <summary>
		/// Gets or sets the horizontal scroll value.
		/// </summary>
		[EaseProperty]
		public int ScrollX { get; set; }

		/// <summary>
		/// Gets or sets the vertical scroll value.
		/// </summary>
		[EaseProperty]
		public int ScrollY { get; set; }

		/// <summary>
		/// Gets or sets the tiling type for the background.
		/// </summary>
		public TilingTypes TilingType { get; set; }

		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => BackgroundType == BackgroundTypes.Color
		? base.ToString() + $" {Color}"
		: base.ToString() + $" {string.Join(',', Image)}";

		/// <summary>
		/// Specifies the types of backgrounds.
		/// </summary>
		public enum BackgroundTypes
		{
			/// <summary>
			/// Background is a color.
			/// </summary>
			Color,

			/// <summary>
			/// Background is an image.
			/// </summary>
			Image
		}

		/// <summary>
		/// Specifies the filter modes.
		/// </summary>
		public enum FilterModes
		{
			/// <summary>
			/// Nearest neighbor filtering.
			/// </summary>
			NearestNeighbor
		}
	}
}
