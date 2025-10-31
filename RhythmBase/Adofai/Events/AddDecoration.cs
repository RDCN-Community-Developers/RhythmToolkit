﻿namespace RhythmBase.Adofai.Events;
/// <summary>  
/// Represents an event to add a decoration to a tile in the game.  
/// </summary>  
public class AddDecoration : BaseTileEvent, IBeginningEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.AddDecoration;
	/// <summary>  
	/// Gets or sets the image of the decoration.  
	/// </summary>  
	public string DecorationImage { get; set; } = string.Empty;
	/// <summary>  
	/// Gets or sets the position of the decoration.  
	/// </summary>  
	public RDPointN Position { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the reference point for the decoration's position.  
	/// </summary>  
	public DecorationRelativeTo RelativeTo { get; set; } = DecorationRelativeTo.Global;
	/// <summary>
	/// Gets or sets a value indicating whether the object should adhere to the floor surface.
	/// </summary>
	public bool StickToFloor { get; set; } = false;
	/// <summary>  
	/// Gets or sets the pivot offset of the decoration.  
	/// </summary>  
	public RDSizeN PivotOffset { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the rotation of the decoration in degrees.  
	/// </summary>  
	public float Rotation { get; set; }
	/// <summary>  
	/// Gets or sets a value indicating whether the rotation is locked.  
	/// </summary>  
	[RDJsonCondition($"!$&.{nameof(StickToFloor)}")]
	public bool LockRotation { get; set; } = false;
	/// <summary>  
	/// Gets or sets the scale of the decoration.  
	/// </summary>  
	public RDSizeN Scale { get; set; } = new(100, 100);
	/// <summary>  
	/// Gets or sets a value indicating whether the scale is locked.  
	/// </summary>  
	[RDJsonCondition($"!$&.{nameof(StickToFloor)}")]
	public bool LockScale { get; set; }
	/// <summary>  
	/// Gets or sets the tile size of the decoration.  
	/// </summary>  
	[RDJsonCondition($"""
		$&.{nameof(BlendMode)} is RhythmBase.Adofai.Events.{nameof(BlendModes)}.{nameof(BlendModes.None)} &&
		$&.{nameof(MaskingType)} is not RhythmBase.Adofai.Events.{nameof(Events.MaskingType)}.{nameof(MaskingType.Mask)}
		""")]
	public RDSizeN Tile { get; set; } = new(1, 1);
	/// <summary>  
	/// Gets or sets the color of the decoration.  
	/// </summary>  
	public RDColor Color { get; set; } = RDColor.White;
	/// <summary>  
	/// Gets or sets the opacity of the decoration.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(MaskingType)} is not RhythmBase.Adofai.Events.{nameof(Events.MaskingType)}.{nameof(MaskingType.Mask)}")]
	public float Opacity { get; set; } = 100f;
	/// <summary>  
	/// Gets or sets the depth of the decoration.  
	/// </summary>  
	[RDJsonCondition($"""
		$&.{nameof(MaskingType)} is not RhythmBase.Adofai.Events.{nameof(Events.MaskingType)}.{nameof(MaskingType.Mask)} &&
		!$&.{nameof(SyncFloorDepth)}
		""")]
	public int Depth { get; set; } = -1;
	/// <summary>
	/// Gets or sets a value indicating whether the floor depth synchronization is enabled.
	/// </summary>
	public bool SyncFloorDepth { get; set; }
	/// <summary>  
	/// Gets or sets the parallax effect of the decoration.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(RelativeTo)} is not RhythmBase.Adofai.Events.{nameof(DecorationRelativeTo)}.{nameof(DecorationRelativeTo.Camera)}")]
	public RDSizeN Parallax { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the parallax offset of the decoration.  
	/// </summary>  
	public RDSizeN ParallaxOffset { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the tag associated with the decoration.  
	/// </summary>  
	public string Tag { get; set; } = string.Empty;
	/// <summary>  
	/// Gets or sets a value indicating whether image smoothing is applied.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(MaskingType)} is not RhythmBase.Adofai.Events.{nameof(Events.MaskingType)}.{nameof(MaskingType.Mask)}")]
	public bool ImageSmoothing { get; set; } = true;
	/// <summary>  
	/// Gets or sets the blend mode of the decoration.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(MaskingType)} is not RhythmBase.Adofai.Events.{nameof(Events.MaskingType)}.{nameof(MaskingType.Mask)}")]
	public BlendModes BlendMode { get; set; } = BlendModes.None;
	/// <summary>  
	/// Gets or sets the masking type of the decoration.  
	/// </summary>  
	public MaskingType MaskingType { get; set; } = MaskingType.None;
	/// <summary>
	/// Gets or sets the target string to be masked.
	/// </summary>
	public string MaskingTarget { get; set; } = string.Empty;
	/// <summary>  
	/// Gets or sets a value indicating whether masking depth is used.  
	/// </summary>  
	public bool UseMaskingDepth { get; set; } = false;
	/// <summary>  
	/// Gets or sets the front depth for masking.  
	/// </summary>  
	public int MaskingFrontDepth { get; set; } = -1;
	/// <summary>  
	/// Gets or sets the back depth for masking.  
	/// </summary>  
	public int MaskingBackDepth { get; set; } = -1;
	/// <summary>  
	/// Gets or sets the type of hitbox for the decoration.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(MaskingType)} is not RhythmBase.Adofai.Events.{nameof(Events.MaskingType)}.{nameof(MaskingType.Mask)}")]
	public HitboxTypes Hitbox { get; set; } = HitboxTypes.None;
	/// <summary>
	/// Gets or sets the type of trigger for the hitbox.
	/// </summary>
	public HitboxTriggerType HitboxTriggerType { get; set; } = HitboxTriggerType.Once;
	/// <summary>
	/// Gets or sets the interval, in milliseconds, at which the hitbox is repeatedly activated.
	/// </summary>
	public float HitboxRepeatInterval { get; set; } = 1000f;
	/// <summary>  
	/// Gets or sets the event tag associated with the hitbox.  
	/// </summary>  
	public string HitboxEventTag { get; set; } = string.Empty;
	/// <summary>
	/// Gets or sets the target for hitbox detection.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Hitbox)} is not RhythmBase.Adofai.Events.{nameof(HitboxTypes)}.{nameof(HitboxTypes.None)}")]
	public HitboxDetectTarget HitboxDetectTarget { get; set; } = HitboxDetectTarget.Planet;
	/// <summary>
	/// Gets or sets the target planet for hitbox detection.
	/// </summary>
	public HitboxTargetPlanet HitboxTargetPlanet { get; set; } = HitboxTargetPlanet.Any;
	/// <summary>
	/// Gets or sets the decorative tag associated with the hitbox.
	/// </summary>
	public string HitboxDecoTag { get; set; } = string.Empty;
	/// <summary>  
	/// Gets or sets the type of fail hitbox for the decoration.  
	/// </summary>  
	public FailHitboxTypes FailHitboxType { get; set; }
	/// <summary>  
	/// Gets or sets the scale of the fail hitbox.  
	/// </summary>  
	public RDSizeN FailHitboxScale { get; set; } = new(100, 100);
	/// <summary>  
	/// Gets or sets the offset of the fail hitbox.  
	/// </summary>  
	public RDSizeN FailHitboxOffset { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the rotation of the fail hitbox in degrees.  
	/// </summary>  
	public float FailHitboxRotation { get; set; } = 0f;
	/// <summary>  
	/// Gets or sets the components associated with the decoration.  
	/// </summary>  
	public string Components { get; set; } = string.Empty;
}
/// <summary>
/// Specifies how a decoration's hitbox trigger should behave when activated.
/// </summary>
[RDJsonEnumSerializable]
public enum HitboxTriggerType
{
	/// <summary>
	/// The hitbox triggers a single time and then stops until reactivated.
	/// </summary>
	Once,
	/// <summary>
	/// The hitbox triggers each time a distinct touch or contact occurs.
	/// </summary>
	PerTouch,
	/// <summary>
	/// The hitbox triggers repeatedly at the configured repeat interval while active.
	/// </summary>
	Repeat,
}
/// <summary>
/// Defines what kinds of objects the hitbox detection system should consider as valid targets.
/// </summary>
[RDJsonEnumSerializable]
public enum HitboxDetectTarget
{
	/// <summary>
	/// The hitbox detects collisions with planets.
	/// </summary>
	Planet,
	/// <summary>
	/// The hitbox detects collisions with decorations.
	/// </summary>
	Decoration,
}
/// <summary>
/// Specifies which planets are considered by the hitbox when the detect target is a planet.
/// </summary>
[RDJsonEnumSerializable]
public enum HitboxTargetPlanet
{
	/// <summary>
	/// The hitbox can target any planet (no restriction).
	/// </summary>
	Any,
	/// <summary>
	/// The hitbox targets the central planet.
	/// </summary>
	Center,
	/// <summary>
	/// The hitbox targets orbiting planets.
	/// </summary>
	Orbiting,
}