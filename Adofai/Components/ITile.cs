using RhythmBase.Adofai.Events;

namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents a tile in the ADOFAI level, containing events and properties related to the tile.
	/// </summary>
	public interface ITile
	{
		/// <summary>
		/// Gets or sets the angle of the tile.
		/// The value is normalized to the range [-180, 180].
		/// </summary>
		float Angle { get; set; }

		/// <summary>
		/// Gets the index of the tile in the level.
		/// </summary>
		int Index { get; }

		/// <summary>
		/// Gets a value indicating whether the tile is a hairpin.
		/// A tile is considered a hairpin if the absolute difference between its angle and the previous tile's angle is exactly 180 degrees.
		/// </summary>
		bool IsHairPin { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the tile is in a mid-spin state.
		/// A tile is considered mid-spin if its angle is less than -180 or greater than 180.
		/// </summary>
		bool IsMidSpin { get; set; }

		/// <summary>
		/// Gets or sets the next tile in the sequence.
		/// </summary>
		ITile? Next { get; internal set; }

		/// <summary>
		/// Gets or sets the parent level of the tile.
		/// </summary>
		internal ADLevel? Parent { get; set; }

		/// <summary>
		/// Gets or sets the previous tile in the sequence.
		/// </summary>
		ITile? Previous { get; internal set; }

		/// <summary>
		/// Gets the tick value of the tile.
		/// The tick is calculated based on the relationship between the current tile, the previous tile, and the next tile.
		/// </summary>
		float Tick { get; }

		/// <summary>
		/// Creates a deep copy of the current tile instance.
		/// </summary>
		/// <returns>A new instance of <see cref="ITile"/> that is a copy of the current tile.</returns>
		ITile Clone();

		/// <summary>
		/// Adds a tile event to the tile.
		/// </summary>
		/// <param name="item">The tile event to add.</param>
		void Add(BaseTileEvent item);
	}
}