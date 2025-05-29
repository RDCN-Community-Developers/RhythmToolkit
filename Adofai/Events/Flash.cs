using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a flash event in the Adofai event system.  
	/// </summary>  
	public class Flash : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.Flash;
		/// <summary>  
		/// Gets or sets the duration of the flash event.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the plane on which the flash occurs.  
		/// </summary>  
		public string Plane { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the starting color of the flash.  
		/// </summary>  
		public RDColor StartColor { get; set; }
		/// <summary>  
		/// Gets or sets the starting opacity of the flash.  
		/// </summary>  
		public float StartOpacity { get; set; }
		/// <summary>  
		/// Gets or sets the ending color of the flash.  
		/// </summary>  
		public RDColor EndColor { get; set; }
		/// <summary>  
		/// Gets or sets the ending opacity of the flash.  
		/// </summary>  
		public float EndOpacity { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the flash transition.  
		/// </summary>  
		public EaseType Ease { get; set; }
	}
}
