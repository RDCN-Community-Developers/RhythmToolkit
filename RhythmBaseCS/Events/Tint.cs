using Newtonsoft.Json;
using RhythmBase.Components;
using SkiaSharp;

namespace RhythmBase.Events
{

	public class Tint : BaseDecorationAction, IEaseEvent
	{
		public Tint()
		{
			BorderColor = new PaletteColor(true);
			TintColor = new PaletteColor(true)
			{
				Color = new SKColor?(new SKColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue))
			};
			Type = EventType.Tint;
			Tab = Tabs.Decorations;
		}
		public Ease.EaseType Ease { get; set; }
		public Borders Border { get; set; }
		[EaseProperty]
		public PaletteColor BorderColor { get; set; }
		[EaseProperty]
		public int Opacity { get; set; }
		[JsonProperty("Tint")]
		public bool IsTint { get; set; }
		[EaseProperty]
		public PaletteColor TintColor { get; set; }
		public float Duration { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		internal bool ShouldSerializeDuration() => Duration != 0f;
		internal bool ShouldSerializeEase() => Duration != 0f;
		public override string ToString() => base.ToString() + string.Format(" {0}{1}", Border, (Border == Borders.None) ? "" : (":" + BorderColor.ToString()));
	}
}
