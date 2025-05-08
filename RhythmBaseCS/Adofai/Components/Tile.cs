using Newtonsoft.Json;
using RhythmBase.Adofai.Events;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents a tile in the ADOFAI level, containing events and properties related to the tile.
	/// </summary>
	public class Tile : ADTypedList<BaseTileEvent>
	{
		/// <summary>
		/// Gets or sets the angle of the tile.
		/// The value is normalized to the range [-180, 180].
		/// If the value is outside the range [-360, 360], it is set to -999.
		/// </summary>
		public float Angle
		{
			get => _angle;
			set
			{
				_angle = (value % 360 + 540) % 360 - 180;
			}
		}
		/// <summary>
		/// Gets a value indicating whether the tile is in a mid-spin state.
		/// A tile is considered mid-spin if its angle is less than -180 or greater than 180.
		/// </summary>
		public bool IsMidSpin { get; set; } = false;
		/// <summary>  
		/// Gets a value indicating whether the tile is a hairpin.  
		/// A tile is considered a hairpin if the absolute difference between its angle and the previous tile's angle is exactly 180 degrees.  
		/// </summary>  
		public bool IsHairPin => float.Abs(_angle - (Previous?._angle ?? 0)) == 180f;
		/// <summary>  
		/// Gets the tick value of the tile.  
		/// The tick is calculated based on the relationship between the current tile, the previous tile, and the next tile.  
		/// If the tile is a hairpin, the tick is set to 2.  
		/// If there is no next tile, the tick is set to 0.  
		/// Otherwise, the tick is calculated as the normalized angular difference between the previous tile and the current tile.  
		/// </summary>  
		public float Tick => Next is null ? 0 : (((Previous?._angle ?? 0) - _angle) % 360 + 540) % 360 / 180;
		///// <summary>
		///// Gets the beat associated with the tile, calculated based on its angle and index.
		///// </summary>
		//public ADBeat Beat => new(Parent?.Calculator, Index + (_angle / 180f));
		/// <summary>
		/// Gets or sets the parent level of the tile.
		/// </summary>
		public ADLevel? Parent { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class.
		/// </summary>
		public Tile() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class with a specified angle.
		/// </summary>
		/// <param name="angle">The angle of the tile.</param>
		public Tile(float angle) => Angle = angle;
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class with a specified mid-spin state.
		/// </summary>
		/// <param name="isMidSpin">A value indicating whether the tile is in a mid-spin state.</param>
		public Tile(bool isMidSpin) => IsMidSpin = isMidSpin;
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class with a collection of tile events.
		/// </summary>
		/// <param name="actions">The collection of tile events to associate with this tile.</param>
		public Tile(IEnumerable<BaseTileEvent> actions)
		{
			foreach (BaseTileEvent i in actions)
			{
				i.Parent = this;
				Add(i);
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class with a specified angle and a collection of tile events.
		/// </summary>
		/// <param name="angle">The angle of the tile.</param>
		/// <param name="actions">The collection of tile events to associate with this tile.</param>
		public Tile(float angle, IEnumerable<BaseTileEvent> actions) : this(actions) => Angle = angle;
		/// <summary>  
		/// Gets or sets the previous tile in the sequence.  
		/// This property is set internally and represents the tile that comes before the current tile.  
		/// </summary>  
		public Tile? Previous { get; internal set; }
		/// <summary>  
		/// Gets or sets the next tile in the sequence.  
		/// This property is set internally and represents the tile that comes after the current tile.  
		/// </summary>  
		[NotNull]
		public Tile? Next { get; internal set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class with a specified mid-spin state and a collection of tile events.
		/// </summary>
		/// <param name="isMidSpin">A value indicating whether the tile is in a mid-spin state.</param>
		/// <param name="actions">The collection of tile events to associate with this tile.</param>
		public Tile(bool isMidSpin, IEnumerable<BaseTileEvent> actions) : this(actions) => IsMidSpin = isMidSpin;
		/// <summary>
		/// Gets the index of the tile within its parent level.
		/// Returns -1 if the tile does not belong to any level.
		/// </summary>
		public int Index => Parent?.IndexOf(this) ?? Parent?.Count ?? -1;
		/// <inheritdoc />
		public override void Add(BaseTileEvent item)
		{
			if ((item is not IStartEvent) || (Previous is null))
				base.Add(item);
		}
		/// <summary>
		/// Returns a string representation of the tile, including its index, beat, angle, and event count.
		/// </summary>
		/// <returns>A string that represents the tile.</returns>
		public override string ToString() => $"[{Index}]<{(IsMidSpin ? "MS".PadRight(4) : _angle.ToString().PadLeft(4))}>{(this.Any() ? string.Format(", Count = {0}", this.Count()) : string.Empty)}";
		/// <summary>
		/// The internal storage for the angle of the tile.
		/// </summary>
		private float _angle = 0;
	}
}
