using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADSetDefaultText : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADSetDefaultText()
		{
			Type = ADEventType.SetDefaultText;
		}

		public override ADEventType Type { get; }

		public float Duration { get; set; }

		public EaseType Ease { get; set; }

		public RDColor? DefaultTextColor { get; set; }

		public RDColor? DefaultTextShadowColor { get; set; }

		public RDPoint? LevelTitlePosition { get; set; }

		public string LevelTitleText { get; set; }

		public string CongratsText { get; set; }

		public string PerfectText { get; set; }
	}
}
