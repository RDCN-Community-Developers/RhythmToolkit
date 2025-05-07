using Newtonsoft.Json;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to animate a track in the game.  
	/// </summary>  
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADAnimateTrack : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.AnimateTrack;
		/// <summary>  
		/// Gets or sets the track animation type.  
		/// </summary>  
		public TrackAnimations? TrackAnimation { get; set; }
		/// <summary>  
		/// Gets or sets the track disappear animation type.  
		/// </summary>  
		public TrackDisappearAnimations? TrackDisappearAnimation { get; set; }
		/// <summary>  
		/// Gets or sets the number of beats ahead for the animation.  
		/// </summary>  
		public int BeatsAhead { get; set; }
		/// <summary>  
		/// Gets or sets the number of beats behind for the animation.  
		/// </summary>  
		public int BeatsBehind { get; set; }
		/// <summary>  
		/// Defines the available track animation types.  
		/// </summary>  
		public enum TrackAnimations
		{
			/// <summary>No animation.</summary>  
			None,
			/// <summary>Assemble animation.</summary>  
			Assemble,
			/// <summary>Assemble animation from a far distance.</summary>  
			Assemble_Far,
			/// <summary>Extend animation.</summary>  
			Extend,
			/// <summary>Grow animation.</summary>  
			Grow,
			/// <summary>Grow and spin animation.</summary>  
			Grow_Spin,
			/// <summary>Fade animation.</summary>  
			Fade,
			/// <summary>Drop animation.</summary>  
			Drop,
			/// <summary>Rise animation.</summary>  
			Rise
		}
		/// <summary>  
		/// Defines the available track disappear animation types.  
		/// </summary>  
		public enum TrackDisappearAnimations
		{
			/// <summary>No disappear animation.</summary>  
			None,
			/// <summary>Scatter animation.</summary>  
			Scatter,
			/// <summary>Scatter animation from a far distance.</summary>  
			Scatter_Far,
			/// <summary>Retract animation.</summary>  
			Retract,
			/// <summary>Shrink animation.</summary>  
			Shrink,
			/// <summary>Shrink and spin animation.</summary>  
			Shrink_Spin,
			/// <summary>Fade animation.</summary>  
			Fade
		}
	}
}
