using System;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class SetTheme : BaseEvent, IRoomEvent
	{

		public SetTheme()
		{
			Preset = Theme.None;
			Type = EventType.SetTheme;
			Tab = Tabs.Actions;
			Rooms = new Room(false, new byte[1]);
		}


		public Theme Preset { get; set; }


		public byte Variant { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public Room Rooms { get; set; }


		public override string ToString() => base.ToString() + string.Format(" {0}", Preset);


		public enum Theme
		{

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
		}
	}
}
