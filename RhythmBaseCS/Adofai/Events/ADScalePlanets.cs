using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Adofai.Events
{
	public class ADScalePlanets : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADScalePlanets()
		{
			Type = ADEventType.ScalePlanets;
		}

		public override ADEventType Type { get; }

		public float Duration { get; set; }

		public TargetPlanets TargetPlanet { get; set; }

		[EaseProperty]
		public int Scale { get; set; }

		public EaseType Ease { get; set; }

		public enum TargetPlanets
		{
			FirePlanet,
			IcePlanet,
			GreenPlanet,
			All
		}
	}
}
