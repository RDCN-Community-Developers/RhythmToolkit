using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to scale planets in the Adofai level.  
	/// </summary>  
	public class ScalePlanets : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type { get; }		/// <summary>  
		/// Gets or sets the duration of the scaling event.  
		/// </summary>  
		public float Duration { get; set; }		/// <summary>  
		/// Gets or sets the target planet(s) for the scaling event.  
		/// </summary>  
		public TargetPlanets TargetPlanet { get; set; }		/// <summary>  
		/// Gets or sets the scale factor for the planets.  
		/// </summary>  
		[EaseProperty]
		public int Scale { get; set; }		/// <summary>  
		/// Gets or sets the easing type for the scaling transition.  
		/// </summary>  
		public EaseType Ease { get; set; }		/// <summary>  
		/// Represents the target planets that can be scaled.  
		/// </summary>  
		public enum TargetPlanets
		{
			/// <summary>  
			/// The fire planet.  
			/// </summary>  
			FirePlanet,			/// <summary>  
			/// The ice planet.  
			/// </summary>  
			IcePlanet,			/// <summary>  
			/// The green planet.  
			/// </summary>  
			GreenPlanet,			/// <summary>  
			/// All planets.  
			/// </summary>  
			All
		}
	}
}
