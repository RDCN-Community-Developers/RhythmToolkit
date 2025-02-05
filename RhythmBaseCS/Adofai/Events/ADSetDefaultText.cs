using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Events;
using SkiaSharp;
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

		public Ease.EaseType Ease { get; set; }

		public SKColor? DefaultTextColor { get; set; }

		public SKColor? DefaultTextShadowColor { get; set; }

		public RDPoint? LevelTitlePosition { get; set; }

		public string LevelTitleText { get; set; }

		public string CongratsText { get; set; }

		public string PerfectText { get; set; }
	}
}
