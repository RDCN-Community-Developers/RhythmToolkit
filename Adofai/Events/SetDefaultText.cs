using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to set default text properties in the Adofai editor.  
	/// </summary>  
	public class SetDefaultText : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetDefaultText;
		/// <summary>  
		/// Gets or sets the duration of the event.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the event.  
		/// </summary>  
		public EaseType Ease { get; set; }
		/// <summary>  
		/// Gets or sets the default text color.  
		/// </summary>  
		public RDColor? DefaultTextColor { get; set; }
		/// <summary>  
		/// Gets or sets the default text shadow color.  
		/// </summary>  
		public RDColor? DefaultTextShadowColor { get; set; }
		/// <summary>  
		/// Gets or sets the position of the level title.  
		/// </summary>  
		public RDPoint? LevelTitlePosition { get; set; }
		/// <summary>  
		/// Gets or sets the text for the level title.  
		/// </summary>  
		public string LevelTitleText { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the text to display upon level completion.  
		/// </summary>  
		public string CongratsText { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the text to display for a perfect score.  
		/// </summary>  
		public string PerfectText { get; set; } = string.Empty;
	}
}
