using RhythmBase.Adofai.Components;

namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to animate a track in the game.  
	/// </summary>  
	public class AnimateTrack : BaseTileEvent, ISingleEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.AnimateTrack;
		/// <summary>  
		/// Gets or sets the track animation type.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(TrackAnimation)} is not null")]
		public TrackAnimationType? TrackAnimation { get; set; }
		/// <summary>  
		/// Gets or sets the track disappear animation type.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(TrackDisappearAnimation)} is not null")]
		public TrackDisappearAnimationType? TrackDisappearAnimation { get; set; }
		/// <summary>  
		/// Gets or sets the number of beats ahead for the animation.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(TrackAnimation)} is not RhythmBase.Adofai.Components.{nameof(Components.TrackAnimationType)}.{nameof(Components.TrackAnimationType.None)}")]
		public float BeatsAhead { get; set; } = 3;
		/// <summary>  
		/// Gets or sets the number of beats behind for the animation.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(TrackDisappearAnimation)} is not RhythmBase.Adofai.Components.{nameof(Components.TrackDisappearAnimationType)}.{nameof(Components.TrackDisappearAnimationType.None)}")]
		public float BeatsBehind { get; set; }
	}
}
