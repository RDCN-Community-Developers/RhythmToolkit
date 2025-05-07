using System;
using Newtonsoft.Json;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the base class for all tile-specific ADOFAI events.
	/// </summary>
	public abstract class ADBaseTileEvent : ADBaseEvent
	{
		/// <summary>
		/// Gets or sets the parent tile associated with this event.
		/// </summary>
		[JsonIgnore]
		public ADTile? Parent { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the event is active.  
		/// </summary>  
		/// <value>  
		/// <c>true</c> if the event is active; otherwise, <c>false</c>.  
		/// </value>  
		public bool Active { get; set; } = true;
		/// <summary>
		/// Returns a string representation of the event type.
		/// </summary>
		/// <returns>A string that represents the event type.</returns>
		public override string ToString() => string.Format("{0}", Type);
	}
}
