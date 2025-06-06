﻿using System;
namespace RhythmBase.Adofai.Events
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
		public int Position { get; set; }
		/// <summary>  
		/// Gets or sets the size of the area affected by the free roam removal.  
		/// </summary>  
		public int Size { get; set; }
	}
}
