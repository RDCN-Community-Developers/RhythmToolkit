namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a "Hold" event in the ADOFAI editor.  
	/// </summary>  
	public class ADHold : ADBaseTileEvent
	{
		/// <inheritdoc/>  
		public override ADEventType Type => ADEventType.Hold;		/// <summary>  
		/// Gets or sets the duration of the hold event.  
		/// </summary>  
		public int Duration { get; set; }		/// <summary>  
		/// Gets or sets the distance multiplier for the hold event.  
		/// </summary>  
		public int DistanceMultiplier { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the landing animation is enabled for the hold event.  
		/// </summary>  
		public bool LandingAnimation { get; set; }
	}
}
