using RhythmBase.Adofai.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to set the speed in the Adofai level.  
	/// </summary>  
	public class SetSpeed : BaseTaggedTileAction
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetSpeed;
		/// <summary>  
		/// Gets or sets the type of speed adjustment.  
		/// </summary>  
		public SpeedType SpeedType { get; set; }
		/// <summary>  
		/// Gets or sets the beats per minute (BPM) value.  
		/// </summary>  
		public float BeatsPerMinute { get; set; }
		/// <summary>  
		/// Gets or sets the BPM multiplier value.  
		/// </summary>  
		public float BpmMultiplier { get; set; }
	}
}
