using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events;

/// <summary>  
/// Represents an event to set default text properties in the Adofai editor.  
/// </summary>  
public class SetDefaultText : BaseTaggedTileEvent, IEaseEvent, IBeginningEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.SetDefaultText;
	/// <summary>  
	/// Gets or sets the duration of the event.  
	/// </summary>  
	public float Duration { get; set; } = 1f;
	/// <summary>  
	/// Gets or sets the easing type for the event.  
	/// </summary>  
	public EaseType Ease { get; set; }
	/// <summary>  
	/// Gets or sets the default text color.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(DefaultTextColor)} is not null")]
	public RDColor? DefaultTextColor { get; set; }
	/// <summary>  
	/// Gets or sets the default text shadow color.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(DefaultTextShadowColor)} is not null")]
	public RDColor? DefaultTextShadowColor { get; set; }
	/// <summary>  
	/// Gets or sets the position of the level title.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(LevelTitlePosition)} is not null")]
	public RDPoint? LevelTitlePosition { get; set; }
	/// <summary>  
	/// Gets or sets the text for the level title.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(LevelTitleText)} is not null")]
	public string LevelTitleText { get; set; } = string.Empty;
	/// <summary>  
	/// Gets or sets the text to display upon level completion.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(CongratsText)} is not null")]
	public string CongratsText { get; set; } = string.Empty;
	/// <summary>  
	/// Gets or sets the text to display for a perfect score.  
	/// </summary>
	[RDJsonCondition($"$&.{nameof(PerfectText)} is not null")]
	public string PerfectText { get; set; } = string.Empty;
}
