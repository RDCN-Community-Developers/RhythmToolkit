using RhythmBase.Global.Components.Easing;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a tile event in the rhythm base system.
	/// </summary>
	public class Tile : BaseDecorationAction, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class.
		/// </summary>
		public Tile() { }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.Tile;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Decorations;
		/// <summary>
		/// Gets or sets the position of the tile.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Position)} is not null")]
		public RDPoint? Position { get; set; } = null;
		/// <summary>
		/// Gets or sets the tiling of the tile.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Tiling)} is not null")]
		public RDPoint? Tiling { get; set; }
		/// <summary>
		/// Gets or sets the speed of the tile.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Speed)} is not null")]
		public RDPoint? Speed { get; set; } = null;
		/// <summary>
		/// Gets or sets the type of tiling.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Speed)} is not null")]
		public TilingTypes TilingType { get; set; } = TilingTypes.Scroll;
		/// <summary>
		/// Gets or sets the interval for the tiling.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(TilingType)} is RhythmBase.RhythmDoctor.Events.TilingTypes.Pulse")]
		public float Interval { get; set; } = 0.01f;
		/// <summary>
		/// Gets or sets the Y coordinate. Always returns 0.
		/// </summary>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; } = 0f;
	}
}
