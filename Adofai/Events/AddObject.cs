using RhythmBase.Adofai.Components;
namespace RhythmBase.Adofai.Events;

/// <summary>  
/// Represents an event to add an object in the game.  
/// </summary>  
public class AddObject : BaseTileEvent, IBeginningEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.AddObject;
	/// <summary>  
	/// Gets or sets the type of the object to be added.  
	/// </summary>  
	public ObjectType ObjectType { get; set; } = ObjectType.Floor;
	/// <summary>  
	/// Gets or sets the type of the planet's color.  
	/// </summary>  
	public PlanetColorType PlanetColorType { get; set; } = PlanetColorType.DefaultRed;
	/// <summary>  
	/// Gets or sets the color of the planet.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(PlanetColorType)} is RhythmBase.Adofai.Events.{nameof(Events.PlanetColorType)}.{nameof(PlanetColorType.Custom)}")]
	public RDColor PlanetColor { get; set; } = RDColor.Red;
	/// <summary>  
	/// Gets or sets the tail color of the planet.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(PlanetColorType)} is RhythmBase.Adofai.Events.{nameof(Events.PlanetColorType)}.{nameof(PlanetColorType.Custom)}")]
	public RDColor PlanetTailColor { get; set; } = RDColor.Red;
	/// <summary>  
	/// Gets or sets the type of the track.  
	/// </summary>  
	public TrackType TrackType { get; set; } = TrackType.Normal;
	/// <summary>  
	/// Gets or sets the angle of the track.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(TrackType)} is not RhythmBase.Adofai.Events.{nameof(Events.TrackType)}.{nameof(TrackType.Midspin)}")]
	public float TrackAngle { get; set; }
	/// <summary>  
	/// Gets or sets the type of the track's color.  
	/// </summary>  
	public TrackColorType TrackColorType { get; set; }
	/// <summary>  
	/// Gets or sets the primary color of the track.  
	/// </summary>  
	public RDColor TrackColor { get; set; } = RDColor.FromRgba("debb7b");
	/// <summary>  
	/// Gets or sets the secondary color of the track.  
	/// </summary>  
	public RDColor SecondaryTrackColor { get; set; } = RDColor.White;
	/// <summary>  
	/// Gets or sets the duration of the track color animation.  
	/// </summary>  
	public float TrackColorAnimDuration { get; set; } = 2f;
	/// <summary>  
	/// Gets or sets the opacity of the track.  
	/// </summary>  
	public float TrackOpacity { get; set; } = 100f;
	/// <summary>  
	/// Gets or sets the style of the track.  
	/// </summary>  
	public TrackStyle TrackStyle { get; set; } = TrackStyle.Standard;
	/// <summary>  
	/// Gets or sets the icon of the track.  
	/// </summary>  
	public TrackIconType TrackIcon { get; set; } = TrackIconType.None;
	/// <summary>  
	/// Gets or sets the angle of the track icon.  
	/// </summary>  
	public float TrackIconAngle { get; set; } = 0;
	/// <summary>
	/// Gets or sets a value indicating whether the track icon is flipped.
	/// </summary>
	public bool TrackIconFlpped { get; set; } = false;
	/// <summary>  
	/// Gets or sets a value indicating whether the track has a red swirl.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(TrackIcon)} is RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.Swirl)}")]
	public bool TrackRedSwirl { get; set; } = false;
	/// <summary>  
	/// Gets or sets a value indicating whether the track has a gray set speed icon.  
	/// </summary>  
	[RDJsonCondition($"""
		$&.{nameof(TrackIcon)}
		is RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.Snail)}
		or RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.DoubleSnail)}
		or RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.Rabbit)}
		or RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.DoubleRabbit)}
		""")]
	public bool TrackGraySetSpeedIcon { get; set; } = false;
	/// <summary>  
	/// Gets or sets the BPM value for the track's set speed icon.  
	/// </summary>  
	[RDJsonCondition($"""
		$&.{nameof(TrackIcon)}
		is RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.Snail)}
		or RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.DoubleSnail)}
		or RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.Rabbit)}
		or RhythmBase.Adofai.Events.{nameof(TrackIconType)}.{nameof(TrackIconType.DoubleRabbit)}
		""")]
	public float TrackSetSpeedIconBpm { get; set; } = 100f;
	/// <summary>  
	/// Gets or sets a value indicating whether the track glow is enabled.  
	/// </summary>  
	public bool TrackGlowEnabled { get; set; } = false;
	/// <summary>  
	/// Gets or sets the glow color of the track.  
	/// </summary>  
	public RDColor TrackGlowColor { get; set; } = RDColor.White;
	/// <summary>
	/// Gets or sets a value indicating whether icon outlines should be tracked.
	/// </summary>
	public bool TrackIconOutlines { get; set; } = false;
	/// <summary>  
	/// Gets or sets the position of the object.  
	/// </summary>  
	public RDPointN Position { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the relative position of the camera.  
	/// </summary>  
	public CameraRelativeTo RelativeTo { get; set; } = CameraRelativeTo.Global;
	/// <summary>  
	/// Gets or sets the pivot offset of the object.  
	/// </summary>  
	public RDPointN PivotOffset { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the rotation of the object.  
	/// </summary>  
	public float Rotation { get; set; } = 0f;
	/// <summary>  
	/// Gets or sets a value indicating whether the rotation is locked.  
	/// </summary>  
	public bool LockRotation { get; set; } = false;
	/// <summary>  
	/// Gets or sets the scale of the object.  
	/// </summary>  
	public RDSizeN Scale { get; set; } = new(100,100);
	/// <summary>  
	/// Gets or sets a value indicating whether the scale is locked.  
	/// </summary>  
	public bool LockScale { get; set; } = false;
	/// <summary>  
	/// Gets or sets the depth of the object.  
	/// </summary>  
	[RDJsonCondition($"!$&.{nameof(SyncFloorDepth)}")]
	public int Depth { get; set; } = -1;
	/// <summary>
	/// Gets or sets a value indicating whether the floor depth synchronization is enabled.
	/// </summary>
	public bool SyncFloorDepth { get; set; } = false;
	/// <summary>  
	/// Gets or sets the parallax effect of the object.  
	/// </summary>  
	public RDSizeN Parallax { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the parallax offset of the object.  
	/// </summary>  
	public RDSizeN ParallaxOffset { get; set; } = new(0, 0);
	/// <summary>  
	/// Gets or sets the tag associated with the object.  
	/// </summary>  
	public string Tag { get; set; } = string.Empty;
}

/// <summary>  
/// Represents the types of objects that can be added.  
/// </summary>  
[RDJsonEnumSerializable]
public enum ObjectType
{
	/// <summary>  
	/// Represents a floor object.  
	/// </summary>  
	Floor,
	/// <summary>  
	/// Represents a planet object.  
	/// </summary>  
	Planet
}
/// <summary>  
/// Represents the types of planet colors.  
/// </summary>  
[RDJsonEnumSerializable]
public enum PlanetColorType
{
	/// <summary>  
	/// Default red color.  
	/// </summary>  
	DefaultRed,
	/// <summary>  
	/// Custom planet color type.  
	/// </summary>  
	planetColorType,
	/// <summary>  
	/// Gold color.  
	/// </summary>  
	Gold,
	/// <summary>  
	/// Overseer color.  
	/// </summary>  
	Overseer,
	/// <summary>  
	/// Custom color.  
	/// </summary>  
	Custom
}
/// <summary>  
/// Represents the types of tracks.  
/// </summary>  
[RDJsonEnumSerializable]
public enum TrackType
{
	/// <summary>  
	/// Normal track type.  
	/// </summary>  
	Normal,
	/// <summary>  
	/// Midspin track type.  
	/// </summary>  
	Midspin
}
/// <summary>
/// Specifies the type of icon used to represent a track element in the application.
/// </summary>
/// <remarks>The <see cref="TrackIconType"/> enumeration provides various icon types that can be used to visually
/// represent different track elements. Each value corresponds to a specific icon style, such as a swirl, snail, or
/// rabbit, which can be used to convey different meanings or statuses within the application's user
/// interface.</remarks>
[RDJsonEnumSerializable]
public enum TrackIconType
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
	None,
	Swirl,
	Snail,
	DoubleSnail,
	Rabbit,
	DoubleRabbit,
	Checkpoint,
	HoldArrowShort,
	HoldArrowLong,
	HoldReleaseShort,
	HoldReleaseLong,
	MultiPlanetTwo,
	MultiPlanetThreeMore,
	Portal,
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}