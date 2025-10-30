using RhythmBase.Adofai.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to set the speed in the Adofai level.  
	/// </summary>  
	public class SetSpeed : BaseTaggedTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetSpeed;
		/// <summary>  
		/// Gets or sets the type of speed adjustment.  
		/// </summary>  
		public SpeedType SpeedType { get; set; } = SpeedType.Bpm;
		/// <summary>  
		/// Gets or sets the beats per minute (BPM) value.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(SpeedType)} is RhythmBase.Adofai.Components.{nameof(Components.SpeedType)}.{nameof(SpeedType.Bpm)}")]
		public float BeatsPerMinute { get; set; } = 100f;
		/// <summary>  
		/// Gets or sets the BPM multiplier value.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(SpeedType)} is RhythmBase.Adofai.Components.{nameof(Components.SpeedType)}.{nameof(SpeedType.Multiplier)}")]
		public float BpmMultiplier { get; set; } = 1f;
	}
}
