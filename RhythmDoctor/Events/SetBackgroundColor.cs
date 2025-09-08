using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
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
			Rooms = new RDRoom([0]);
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
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled
			""")]
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the content mode for the event.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image")]
		public ContentModes ContentMode { get; set; }
		/// <summary>
		/// Gets or sets the filter mode for the event.
		/// </summary>
		public BackgroundFilterModes Filter { get; set; }
		/// <summary>
		/// Gets or sets the color for the background.
		/// </summary>
		[EaseProperty]
		public PaletteColor Color { get; set; }
		/// <summary>
		/// Gets or sets the interval for the event.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled &&
			$&.{nameof(TilingType)} == RhythmBase.RhythmDoctor.Events.TilingTypes.Pulse
			""")]
		public float Interval
		{
			get => field > 0.01f ? field : 0.01f;
			set => field = value > 0.01f ? value : 0.01f;
		}
		/// <summary>
		/// Gets or sets the background type for the event.
		/// </summary>
		public BackgroundTypes BackgroundType { get; set; }
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled
			""")]
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the frames per second for the event.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(Image)}.Count > 1
			""")]
		public int Fps { get; set; }
		/// <summary>
		/// Gets or sets the list of images for the background.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image")]
		public List<string> Image { get; set; } = [];
		/// <summary>
		/// Gets or sets the horizontal scroll value.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled
			""")]
		public int ScrollX { get; set; }
		/// <summary>
		/// Gets or sets the vertical scroll value.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled
			""")]
		public int ScrollY { get; set; }
		/// <summary>
		/// Gets or sets the tiling type for the background.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(BackgroundType)} == RhythmBase.RhythmDoctor.Events.BackgroundTypes.Image &&
			$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled
			""")]
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
#if NETSTANDARD
		: base.ToString() + $" {string.Join(",", Image)}";
#else
		: base.ToString() + $" {string.Join(',', Image)}";
#endif
	}
	/// <summary>
	/// Specifies the types of backgrounds.
	/// </summary>
	[RDJsonEnumSerializable]
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
	[RDJsonEnumSerializable]
	public enum BackgroundFilterModes
	{
		/// <summary>
		/// Nearest neighbor filtering.
		/// </summary>
		NearestNeighbor
	}
}
