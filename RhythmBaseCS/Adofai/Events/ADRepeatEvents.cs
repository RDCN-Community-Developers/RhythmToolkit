using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that repeats specific actions in the level.  
	/// </summary>  
	public class ADRepeatEvents : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.RepeatEvents;		/// <summary>  
		/// Gets or sets the type of repetition for the event.  
		/// </summary>  
		public RepeatTypes RepeatType { get; set; }		/// <summary>  
		/// Gets or sets the number of repetitions for the event.  
		/// </summary>  
		public int Repetitions { get; set; }		/// <summary>  
		/// Gets or sets the number of floors affected by the event.  
		/// </summary>  
		public int FloorCount { get; set; }		/// <summary>  
		/// Gets or sets the interval between repetitions.  
		/// </summary>  
		public float Interval { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the event should execute on the current floor.  
		/// </summary>  
		public bool ExecuteOnCurrentFloor { get; set; }		/// <summary>  
		/// Gets or sets the tag associated with the event.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;		/// <summary>  
		/// Defines the types of repetition available for the event.  
		/// </summary>  
		public enum RepeatTypes
		{
			/// <summary>  
			/// Repeats the event based on beats.  
			/// </summary>  
			Beat,			/// <summary>  
			/// Repeats the event based on floors.  
			/// </summary>  
			Floor
		}
	}
}
