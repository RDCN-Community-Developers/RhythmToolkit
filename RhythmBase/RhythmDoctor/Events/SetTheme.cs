using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an event to set a theme in a room.  
/// </summary>  
public class SetTheme : BaseEvent, IRoomEvent, IEaseEvent
{
	/// <summary>  
	/// Gets or sets the theme preset.  
	/// </summary>  
	public Theme Preset { get; set; } = Theme.None;
	/// <summary>  
	/// Gets or sets the variant of the theme.  
	/// </summary>  
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.Intimate)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.InsomniacDay)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.CrossesStraight)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.CubesFalling)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.Rooftop)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.Sky)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.CoffeeShop)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.Garden)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.TrainDay)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.DesertDay)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.HospitalWard)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.ColeWardNight)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.Skyline)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.FloatingHeart)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.FloatingHeartBroken)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.Stadium)}
		or RhythmBase.RhythmDoctor.Events.{nameof(Events.Theme)}.{nameof(Theme.AthleteWard)}
		""")]
	public byte Variant { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether the theme's horizontal position is enabled.
	/// </summary>
	[RDJsonCondition($"RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})")]
	public bool EnablePosition { get; set; }
	/// <summary>
	/// Gets or sets the horizontal position offset (X) for themes that support positioning.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(EnablePosition)} &&
		RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
		""")]
	public float PositionX { get; set; }
	///<inheritdoc/>
	public override EventType Type => EventType.SetTheme;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets a value indicating whether to skip paint effects.
	/// </summary>
	public bool SkipPaintEffects { get; set; } = false;
	/// <summary>
	/// Gets or sets a value indicating whether the first row is positioned on the floor.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(FirstRowOnFloor)}")]
	public bool FirstRowOnFloor { get; set; } = false;
	///<inheritdoc/>
	[RDJsonProperty("positionEase")]
	[RDJsonCondition($"""
		$&.{nameof(EnablePosition)} &&
		RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
		""")]
	public EaseType Ease { get; set; }
	///<inheritdoc/>
	[RDJsonProperty("positionDuration")]
	[RDJsonCondition($"""
		$&.{nameof(EnablePosition)} &&
		RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
		""")]
	public float Duration { get; set; }
	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {Preset}";
	internal static readonly ReadOnlyEnumCollection<Theme> ThemesHasPosition = new(
		Theme.TrainDay,
		Theme.TrainNight,
		Theme.DesertDay,
		Theme.DesertNight,
		Theme.HospitalWard,
		Theme.PaigeOffice,
		Theme.Basement,
		Theme.RollerDisco,
		Theme.AthleteWard,
		Theme.AthleteWardNight,
		Theme.Airport,
		Theme.RecordsRoom,
		Theme.AbandonedWard);
}
/// <summary>  
/// Represents the available themes.  
/// </summary>  
[RDJsonEnumSerializable]
public enum Theme
{
#pragma warning disable CS1591
	None,
	Intimate,
	IntimateSimple,
	InsomniacDay,
	InsomniacNight,
	Matrix,
	NeonMuseum,
	CrossesStraight,
	CrossesFalling,
	CubesFalling,
	CubesFallingNiceBlue,
	OrientalTechno,
	Kaleidoscope,
	PoliticiansRally,
	Rooftop,
	RooftopSummer,
	RooftopAutumn,
	BackAlley,
	Sky,
	NightSky,
	HallOfMirrors,
	CoffeeShop,
	CoffeeShopNight,
	Garden,
	GardenNight,
	TrainDay,
	TrainNight,
	DesertDay,
	DesertNight,
	HospitalWard,
	HospitalWardNight,
	PaigeOffice,
	Basement,
	ColeWardNight,
	ColeWardSunrise,
	BoyWard,
	GirlWard,
	Skyline,
	SkylineBlue,
	FloatingHeart,
	FloatingHeartWithCubes,
	FloatingHeartBroken,
	FloatingHeartBrokenWithCubes,
	ZenGarden,
	Space,
	Vaporwave,
	RollerDisco,
	Stadium,
	StadiumStormy,
	AthleteWard,
	AthleteWardNight,
	ProceduralTree,
	RecordsRoom,
	Airport,
	AbandonedWard,
#pragma warning restore CS1591
}
