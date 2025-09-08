using Newtonsoft.Json;
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
		[RDJsonCondition($"$&.{nameof(Position)} is not null")]
		public RDPoint? Position { get; set; }
		/// <summary>
		/// Gets or sets the tiling of the tile.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(Tiling)} is not null")]
		public RDPoint? Tiling { get; set; }
		/// <summary>
		/// Gets or sets the speed of the tile.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(Speed)} is not null")]
		public RDPoint? Speed { get; set; }
		/// <summary>
		/// Gets or sets the type of tiling.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Speed)} is not null")]
		public TilingTypes TilingType { get; set; }
		/// <summary>
		/// Gets or sets the interval for the tiling.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(TilingType)} is RhythmBase.RhythmDoctor.Events.TilingTypes.Pulse")]
		public float Interval { get; set; } = 0.01f;
		/// <summary>
		/// Gets or sets the Y coordinate. Always returns 0.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }
	}
}
