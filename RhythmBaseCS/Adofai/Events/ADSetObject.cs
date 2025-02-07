using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADSetObject : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADSetObject()
		{
			Type = ADEventType.SetObject;
		}
		public override ADEventType Type { get; }
		public float Duration { get; set; }
		public string Tag { get; set; }
		public EaseType Ease { get; set; }
		public RDColor? PlanetColor { get; set; }
		public RDColor? PlanetTailColor { get; set; }
		public float? TrackAngle { get; set; }
		public ADTrackColorTypes? TrackColorType { get; set; }
		public RDColor? TrackColor { get; set; }
		public RDColor? SecondaryTrackColor { get; set; }
		public float? TrackColorAnimDuration { get; set; }
		public float? TrackOpacity { get; set; }
		public ADTrackStyles? TrackStyle { get; set; }
		public string TrackIcon { get; set; }
		public float? TrackIconAngle { get; set; }
		public bool? TrackRedSwirl { get; set; }
		public bool? TrackGraySetSpeedIcon { get; set; }
		public bool? TrackGlowEnabled { get; set; }
		public RDColor? TrackGlowColor { get; set; }
	}
}
