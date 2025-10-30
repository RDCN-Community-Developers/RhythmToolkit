﻿using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events;

/// <summary>  
/// Represents a flash event in the Adofai event system.  
/// </summary>  
public class Flash : BaseTaggedTileEvent, IEaseEvent, IBeginningEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.Flash;
	/// <summary>  
	/// Gets or sets the duration of the flash event.  
	/// </summary>  
	public float Duration { get; set; } = 1f;
	/// <summary>  
	/// Gets or sets the plane on which the flash occurs.  
	/// </summary>  
	public FlashPlane Plane { get; set; } = FlashPlane.Background;
	/// <summary>  
	/// Gets or sets the starting color of the flash.  
	/// </summary>  
	public RDColor StartColor { get; set; } = RDColor.White;
	/// <summary>  
	/// Gets or sets the starting opacity of the flash.  
	/// </summary>  
	public float StartOpacity { get; set; } = 100f;
	/// <summary>  
	/// Gets or sets the ending color of the flash.  
	/// </summary>  
	public RDColor EndColor { get; set; } = RDColor.White;
	/// <summary>  
	/// Gets or sets the ending opacity of the flash.  
	/// </summary>  
	public float EndOpacity { get; set; } = 0f;
	/// <summary>  
	/// Gets or sets the easing type for the flash transition.  
	/// </summary>  
	public EaseType Ease { get; set; }
}
[RDJsonEnumSerializable]
public enum FlashPlane
{
#warning Review the names of these enum members for accuracy.
	Background,
	Foreground
}