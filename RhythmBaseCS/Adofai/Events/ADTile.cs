using Newtonsoft.Json;
using RhythmBase.Adofai.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents a tile in the ADOFAI level, containing events and properties related to the tile.
	/// </summary>
	public class ADTile : ADTypedList<ADBaseTileEvent>
	{
		/// <summary>
		/// Gets or sets the angle of the tile.
		/// The value is normalized to the range [-180, 180].
		/// If the value is outside the range [-360, 360], it is set to -999.
		/// </summary>
		public float Angle
		{
			get => _angle;
			set => _angle = value is > (-360f) and < 360f ? ((value + 540f) % 360f) - 180f : -999f;
		}		/// <summary>
		/// Gets a value indicating whether the tile is in a mid-spin state.
		/// A tile is considered mid-spin if its angle is less than -180 or greater than 180.
		/// </summary>
		public bool IsMidSpin => _angle < -180f || _angle > 180f;		/// <summary>
		/// Gets the beat associated with the tile, calculated based on its angle and index.
		/// </summary>
		public ADBeat Beat => new(Parent?.Calculator!, Index + (_angle / 180f));		/// <summary>
		/// Gets or sets the parent level of the tile.
		/// </summary>
		[JsonIgnore]
		public ADLevel? Parent { get; set; }		/// <summary>
		/// Initializes a new instance of the <see cref="ADTile"/> class.
		/// </summary>
		public ADTile() { }		/// <summary>
		/// Initializes a new instance of the <see cref="ADTile"/> class with a collection of tile events.
		/// </summary>
		/// <param name="actions">The collection of tile events to associate with this tile.</param>
		public ADTile(IEnumerable<ADBaseTileEvent> actions)
		{
			foreach (ADBaseTileEvent i in actions)
			{
				i.Parent = this;
				Add(i);
			}
		}		/// <summary>
		/// Gets the index of the tile within its parent level.
		/// Returns -1 if the tile does not belong to any level.
		/// </summary>
		public int Index => Parent?.IndexOf(this) ?? -1;		/// <summary>
		/// Returns a string representation of the tile, including its index, beat, angle, and event count.
		/// </summary>
		/// <returns>A string that represents the tile.</returns>
		public override string ToString() => $"[{Index}]{Beat}<{(IsMidSpin ? "MS".PadRight(4) : _angle.ToString().PadLeft(4))}>{(this.Any() ? string.Format(", Count = {0}", this.Count()) : string.Empty)}";		/// <summary>
		/// The internal storage for the angle of the tile.
		/// </summary>
		private float _angle = 0;
	}
}
