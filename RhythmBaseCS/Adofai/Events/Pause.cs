using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a pause event in the ADOFAI level.  
	/// </summary>  
	public class Pause : BaseTileEvent
	{
		/// <inheritdoc/>  
		public override EventType Type => EventType.Pause;		/// <summary>  
		/// Gets or sets the duration of the pause in seconds.  
		/// </summary>  
		public float Duration { get; set; }		/// <summary>  
		/// Gets or sets the number of countdown ticks during the pause.  
		/// </summary>  
		public int CountdownTicks { get; set; }		/// <summary>  
		/// Gets or sets the direction of angle correction during the pause.  
		/// </summary>  
		public int AngleCorrectionDir { get; set; }
	}
}
