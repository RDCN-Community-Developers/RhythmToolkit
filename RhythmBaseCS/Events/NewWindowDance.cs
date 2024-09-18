using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class NewWindowDance : BaseEvent, IEaseEvent
	{
		public NewWindowDance()
		{
			Amplitude = 0f;
			Type = EventType.NewWindowDance;
			Tab = Tabs.Actions;
		}
		public string Preset { get; set; }
		public string SamePresetBehavior { get; set; }
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RDPointE Position { get; set; }
		public References Reference { get; set; }
		public bool UseCircle { get; set; }
		[EaseProperty]
		public float Speed { get; set; }
		[EaseProperty]
		public float Amplitude { get; set; }
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RDPointE AmplitudeVector { get; set; }
		[EaseProperty]
		public Expression? Angle { get; set; }
		[EaseProperty]
		public float Frequency { get; set; }
		[EaseProperty]
		public float Period { get; set; }
		public EaseTypes EaseType { get; set; }
		public Ease.EaseType SubEase { get; set; }
		[JsonProperty("EasingDuration")]
		public float Duration { get; set; }
		public Ease.EaseType Ease { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public enum Presets
		{
			Move,
			Sway,
			Wrap,
			Ellipse,
			ShakePer
		}
		public enum SamePresetBehaviors
		{
			Reset,
			Keep
		}
		public enum References
		{
			Center,
			Edge
		}
		public enum EaseTypes
		{
			Repeat
		}
	}
}
