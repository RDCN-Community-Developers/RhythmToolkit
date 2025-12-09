namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents a blend decoration action in the rhythm base.  
/// </summary>  
public class Blend : BaseDecorationAction
{
	///<inheritdoc/>
	public override EventType Type => EventType.Blend;
	///<inheritdoc/>
	public override Tab Tab => Tab.Decorations;
	/// <summary>  
	/// Gets or sets the type of blend effect to apply.  
	/// </summary>  
	public BlendType BlendType { get; set; } = BlendType.None;
}
/// <summary>  
/// Specifies the different types of blend effects available.  
/// </summary>  
[RDJsonEnumSerializable]
public enum BlendType
{
	/// <summary>  
	/// No blend effect.  
	/// </summary>  
	None,
	/// <summary>
	/// Additive blend effect.
	/// </summary>
	Additive,
	/// <summary>
	/// Multiply blend effect.
	/// </summary>
	Multiply,
	/// <summary>
	/// Invert blend effect.
	/// </summary>
	Invert,
}
