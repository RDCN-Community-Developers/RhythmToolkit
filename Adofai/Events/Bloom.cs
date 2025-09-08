using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a Bloom event in the Adofai event system.  
	/// </summary>  
	public class Bloom : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.Bloom;
		/// <summary>  
		/// Gets or sets a value indicating whether the bloom effect is enabled.  
		/// </summary>  
		public bool Enabled { get; set; }
		/// <summary>  
		/// Gets or sets the threshold value for the bloom effect.  
		/// </summary>  
		public int Threshold { get; set; }
		/// <summary>  
		/// Gets or sets the intensity of the bloom effect.  
		/// </summary>  
		public int Intensity { get; set; }
		/// <summary>  
		/// Gets or sets the color of the bloom effect.  
		/// </summary>  
		public RDColor Color { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the bloom effect.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the bloom effect.  
		/// </summary>  
		public EaseType Ease { get; set; }
	}
}
