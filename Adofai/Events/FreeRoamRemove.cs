﻿namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that removes free roam mode in the level.  
	/// </summary>  
	public class FreeRoamRemove : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.FreeRoamRemove;
		/// <summary>  
		/// Gets or sets the position associated with the free roam removal.  
		/// </summary>  
		public RDPointN Position { get; set; } = new(1, 0);
		/// <summary>  
		/// Gets or sets the size of the area affected by the free roam removal.  
		/// </summary>  
		public RDPointN Size { get; set; } = new(1, 0);
	}
}
