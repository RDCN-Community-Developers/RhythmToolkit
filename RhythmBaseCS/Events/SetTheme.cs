using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>  
	/// Represents an event to set a theme in a room.  
	/// </summary>  
	public class SetTheme : BaseEvent, IRoomEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetTheme"/> class.  
		/// </summary>  
		public SetTheme()
		{
			Preset = Theme.None;
			Type = EventType.SetTheme;
			Tab = Tabs.Actions;
			Rooms = new Room(false, new byte[1]);
		}

		/// <summary>  
		/// Gets or sets the theme preset.  
		/// </summary>  
		public Theme Preset { get; set; }

		/// <summary>  
		/// Gets or sets the variant of the theme.  
		/// </summary>  
		public byte Variant { get; set; }

		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; }

		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; }

		/// <summary>  
		/// Gets or sets the rooms associated with the event.  
		/// </summary>  
		public Room Rooms { get; set; }

		/// <summary>  
		/// Returns a string that represents the current object.  
		/// </summary>  
		/// <returns>A string that represents the current object.</returns>  
		public override string ToString() => base.ToString() + string.Format(" {0}", Preset);

		/// <summary>  
		/// Represents the available themes.  
		/// </summary>  
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
			ProceduralTree
#pragma warning restore CS1591
		}
	}
}
