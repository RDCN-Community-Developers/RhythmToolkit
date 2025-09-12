using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set the foreground in a room.  
	/// </summary>  
	public class SetForeground : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetForeground"/> class.  
		/// </summary>  
		public SetForeground()
		{
			Rooms = new RDRoom([0]);
			Color = new PaletteColor(true);
			Type = EventType.SetForeground;
			Tab = Tabs.Actions;
		}
		/// <summary>  
		/// Gets or sets the rooms associated with the event.  
		/// </summary>  
		public RDRoom Rooms { get; set; }
		/// <summary>  
		/// Gets or sets the content mode for the event.  
		/// </summary>  
		public ContentModes ContentMode { get; set; }
		/// <summary>  
		/// Gets or sets the tiling type for the event.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public TilingTypes TilingType { get; set; }
		/// <summary>  
		/// Gets or sets the color for the foreground.  
		/// </summary>  
		[Tween]
		public PaletteColor Color { get; set; }
		/// <summary>  
		/// Gets or sets the list of images for the foreground.  
		/// </summary>  
		public List<string> Image { get; set; } = [];
		/// <summary>  
		/// Gets or sets the frames per second for the foreground animation.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public float Fps { get; set; }
		/// <summary>  
		/// Gets or sets the horizontal scroll value.  
		/// </summary>  
		[Tween]
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public float ScrollX { get; set; }
		/// <summary>  
		/// Gets or sets the vertical scroll value.  
		/// </summary>  
		[Tween]
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public float ScrollY { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the event.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the interval between frames.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public float Interval
		{
			get => field;
			set => field = value > 0.01f ? value : 0.01f;
		}
		/// <summary>  
		/// Gets or sets the easing type for the event.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public EaseType Ease { get; set; }
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(ContentMode)} == RhythmBase.RhythmDoctor.Events.ContentModes.Tiled")]
		public override EventType Type { get; }
		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; }
		/// <summary>  
		/// Returns a string that represents the current object.  
		/// </summary>  
		/// <returns>A string that represents the current object.</returns>  
#if NETSTANDARD
		public override string ToString() => base.ToString() + $" {Color},{string.Join(",", Image.Select(i => i.ToString()))}";
#else
		public override string ToString() => base.ToString() + $" {Color},{string.Join(',', Image.Select(i => i.ToString()))}";
#endif
	}
}
