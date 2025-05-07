using Newtonsoft.Json;
using RhythmBase.Adofai.Components;
using RhythmBase.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that adjusts the position, rotation, scale, and opacity of a track in the level.  
	/// </summary>  
	public class ADPositionTrack : ADBaseTileEvent
	{
		/// <inheritdoc/>  
		public override ADEventType Type => ADEventType.PositionTrack;		/// <summary>  
		/// Gets or sets the position offset of the track.  
		/// </summary>  
		public RDPoint PositionOffset { get; set; }		/// <summary>  
		/// Gets or sets the reference tile relative to which the position is calculated.  
		/// </summary>  
		public TileReference RelativeTo { get; set; }		/// <summary>  
		/// Gets or sets the rotation of the track in degrees.  
		/// </summary>  
		public float Rotation { get; set; }		/// <summary>  
		/// Gets or sets the scale of the track.  
		/// </summary>  
		public float Scale { get; set; }		/// <summary>  
		/// Gets or sets the opacity of the track.  
		/// </summary>  
		public float Opacity { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the event applies only to the current tile.  
		/// </summary>  
		public bool JustThisTile { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the event is editor-only.  
		/// </summary>  
		public bool SditorOnly { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the track should stick to floors.  
		/// </summary>  
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool? StickToFloors { get; set; }
	}
}
