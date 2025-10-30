using RhythmBase.Adofai.Components;
using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events;

/// <summary>
/// Represents an event to set object properties in the Adofai editor.
/// </summary>
public class SetObject : BaseTaggedTileEvent, IEaseEvent, IBeginningEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.SetObject;
	/// <summary>
	/// Gets or sets the duration of the event.
	/// </summary>
	public float Duration { get; set; } = 1f;
	/// <summary>
	/// Gets or sets the tag associated with the object.
	/// </summary>
	public string Tag { get; set; } = string.Empty;
	/// <summary>
	/// Gets or sets the easing type for the event.
	/// </summary>
	public EaseType Ease { get; set; }
	/// <summary>
	/// Gets or sets the color of the planet.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(PlanetColor)} is not null")]
	public RDColor? PlanetColor { get; set; }
	/// <summary>
	/// Gets or sets the color of the planet's tail.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(PlanetTailColor)} is not null")]
	public RDColor? PlanetTailColor { get; set; }
	/// <summary>
	/// Gets or sets the angle of the track.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackAngle)} is not null")]
	public float? TrackAngle { get; set; }
	/// <summary>
	/// Gets or sets the type of the track color.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackColorType)} is not null")]
	public TrackColorType? TrackColorType { get; set; }
	/// <summary>
	/// Gets or sets the primary color of the track.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackColor)} is not null")]
	public RDColor? TrackColor { get; set; } 
	/// <summary>
	/// Gets or sets the secondary color of the track.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SecondaryTrackColor)} is not null")]
	public RDColor? SecondaryTrackColor { get; set; }
	/// <summary>
	/// Gets or sets the duration of the track color animation.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackColorAnimDuration)} is not null")]
	public float? TrackColorAnimDuration { get; set; }
	/// <summary>
	/// Gets or sets the opacity of the track.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackOpacity)} is not null")]
	public float? TrackOpacity { get; set; } 
	/// <summary>
	/// Gets or sets the style of the track.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackStyle)} is not null")]
	public TrackStyle? TrackStyle { get; set; }
	/// <summary>
	/// Gets or sets the icon of the track.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackIcon)} is not null")]
	public TrackIcon? TrackIcon { get; set; }
	/// <summary>
	/// Gets or sets the angle of the track icon.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackIconAngle)} is not null")]
	public float? TrackIconAngle { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the track has a red swirl.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackIconFlipped)} is not null")]
	public bool? TrackIconFlipped { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the red swirl should be tracked.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackRedSwirl)} is not null")]
	public bool? TrackRedSwirl { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the track has a gray set speed icon.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackGraySetSpeedIcon)} is not null")]
	public bool? TrackGraySetSpeedIcon { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the track glow is enabled.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackGlowEnabled)} is not null")]
	public bool? TrackGlowEnabled { get; set; }
	/// <summary>
	/// Gets or sets the color of the track glow.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackGlowColor)} is not null")]
	public RDColor? TrackGlowColor { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether icon outlines should be tracked.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TrackIconOutlines)} is not null")]
	public bool? TrackIconOutlines { get; set; }
}
/// <summary>  
/// Represents the various track icons that can be used in the Adofai editor.  
/// </summary>  
[RDJsonEnumSerializable]
public enum TrackIcon
{
	/// <summary>  
	/// No icon.  
	/// </summary>  
	None,
	/// <summary>  
	/// A single snail icon.  
	/// </summary>  
	Snall,
	/// <summary>  
	/// A double snail icon.  
	/// </summary>  
	DoubleSnall,
	/// <summary>  
	/// A rabbit icon.  
	/// </summary>  
	Rabbit,
	/// <summary>  
	/// A double rabbit icon.  
	/// </summary>  
	DoubleRabbit,
	/// <summary>  
	/// A swirl icon.  
	/// </summary>  
	Swirl,
	/// <summary>  
	/// A checkpoint icon.  
	/// </summary>  
	Checkpoint,
	/// <summary>  
	/// A short hold arrow icon.  
	/// </summary>  
	HoldArrowShort,
	/// <summary>  
	/// A long hold arrow icon.  
	/// </summary>  
	HoldArrowLong,
	/// <summary>  
	/// A short hold release icon.  
	/// </summary>  
	HoldReleaseShort,
	/// <summary>  
	/// A long hold release icon.  
	/// </summary>  
	HoldReleaseLong,
	/// <summary>  
	/// A two-planet multi-planet icon.  
	/// </summary>  
	MultiPlanetTwo,
	/// <summary>  
	/// A three-planet multi-planet icon.  
	/// </summary>  
	MultiPlanetThree,
	/// <summary>  
	/// A portal icon.  
	/// </summary>  
	Portal,
}
