using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a tile event in the rhythm base system.
	/// </summary>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class Tile : BaseDecorationAction, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class.
		/// </summary>
		public Tile()
		{
			Type = EventType.Tile;
			Tab = Tabs.Decorations;
		}
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
		/// <summary>
		/// Gets or sets the position of the tile.
		/// </summary>
		[EaseProperty]
		public RDPoint? Position { get; set; }
		/// <summary>
		/// Gets or sets the tiling of the tile.
		/// </summary>
		[EaseProperty]
		public RDPoint? Tiling { get; set; }
		/// <summary>
		/// Gets or sets the speed of the tile.
		/// </summary>
		[EaseProperty]
		public RDPoint? Speed { get; set; }
		/// <summary>
		/// Gets or sets the type of tiling.
		/// </summary>
		public TilingTypes TilingType { get; set; }
		/// <summary>
		/// Gets or sets the interval for the tiling.
		/// </summary>
		public float Interval { get; set; }
		/// <summary>
		/// Gets or sets the Y coordinate. Always returns 0.
		/// </summary>
		[JsonIgnore]
		public override int Y => 0;
		/// <summary>
		/// Gets or sets the easing type for the event.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Defines the types of tiling available.
		/// </summary>
		public enum TilingTypes
		{
			/// <summary>
			/// Represents a scrolling tiling type.
			/// </summary>
			Scroll,
			/// <summary>
			/// Represents a pulsing tiling type.
			/// </summary>
			Pulse
		}
	}
}
