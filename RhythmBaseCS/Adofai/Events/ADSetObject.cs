using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Events;
using SkiaSharp;
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
		public Ease.EaseType Ease { get; set; }
		public SKColor? PlanetColor { get; set; }
		public SKColor? PlanetTailColor { get; set; }
		public float? TrackAngle { get; set; }
		public ADTrackColorTypes? TrackColorType { get; set; }
		public SKColor? TrackColor { get; set; }
		public SKColor? SecondaryTrackColor { get; set; }
		public float? TrackColorAnimDuration { get; set; }
		public float? TrackOpacity { get; set; }
		public ADTrackStyles? TrackStyle { get; set; }
		public string TrackIcon { get; set; }
		public float? TrackIconAngle { get; set; }
		public bool? TrackRedSwirl { get; set; }
		public bool? TrackGraySetSpeedIcon { get; set; }
		public bool? TrackGlowEnabled { get; set; }
		public SKColor? TrackGlowColor { get; set; }
	}
}
