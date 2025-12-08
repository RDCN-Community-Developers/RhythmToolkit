using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set a theme in a room.  
	/// </summary>  
	public class SetTheme : BaseEvent, IRoomEvent, IEaseEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetTheme"/> class.  
		/// </summary>  
		public SetTheme()
		{
			Preset = Theme.None;
			Type = EventType.SetTheme;
			Tab = Tabs.Actions;
			Rooms = new RDRoom([0]);
		}
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
		[RDJsonCondition($"RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})")]
		public bool EnablePosition { get; set; }
		[RDJsonCondition($"""
			$&.{nameof(EnablePosition)} &&
			RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
			""")]
		public float PositionX { get; set; }
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; } = EventType.SetTheme;
		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <summary>  
		/// Gets or sets the rooms associated with the event.  
		/// </summary>  
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
		[RDJsonProperty("positionEase")]
		[RDJsonCondition($"""
			$&.{nameof(EnablePosition)} &&
			RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
			""")]
		public EaseType Ease { get; set; }
		[RDJsonProperty("positionDuration")]
		[RDJsonCondition($"""
			$&.{nameof(EnablePosition)} &&
			RhythmBase.RhythmDoctor.Events.{nameof(SetTheme)}.{nameof(ThemesHasPosition)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
			""")]
		public float Duration { get; set; }

		/// <summary>  
		/// Returns a string that represents the current object.  
		/// </summary>  
		/// <returns>A string that represents the current object.</returns>  
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
}
