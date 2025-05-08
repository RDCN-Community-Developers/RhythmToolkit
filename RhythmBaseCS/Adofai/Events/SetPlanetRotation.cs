using RhythmBase.Components.Easing;
using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that sets the rotation of a planet in the level.  
	/// </summary>  
	public class SetPlanetRotation : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetPlanetRotation;		/// <summary>  
		/// Gets or sets the easing function to be used for the rotation.  
		/// </summary>  
		public EaseType Ease { get; set; }		/// <summary>  
		/// Gets or sets the number of parts the easing function is divided into.  
		/// </summary>  
		public int EaseParts { get; set; }		/// <summary>  
		/// Gets or sets the behavior of the easing parts.  
		/// </summary>  
		public EasePartBehaviors EasePartBehavior { get; set; }
	}
}
