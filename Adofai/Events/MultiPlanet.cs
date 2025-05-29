using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents an event that enables multi-planet mode in the level.
	/// </summary>
	public class MultiPlanet : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.MultiPlanet;		/// <summary>
		/// Gets or sets the planets associated with this event.
		/// </summary>
		public Planets Planets { get; set; }
	}
	/// <summary>  
	/// Represents the number of planets associated with the multi-planet event.  
	/// </summary>  
	public enum Planets
	{
		/// <summary>  
		/// Two planets are active in the event.  
		/// </summary>  
		TwoPlanets,		/// <summary>  
		/// Three planets are active in the event.  
		/// </summary>  
		ThreePlanets,
	}
}
