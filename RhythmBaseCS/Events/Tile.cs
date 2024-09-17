using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class Tile : BaseDecorationAction, IEaseEvent
	{
		public Tile()
		{
			Type = EventType.Tile;
			Tab = Tabs.Decorations;
		}

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		[EaseProperty]
		public RDPoint? Position { get; set; }

		[EaseProperty]
		public RDPoint? Tiling { get; set; }

		[EaseProperty]
		public RDPoint? Speed { get; set; }

		public TilingTypes TilingType { get; set; }

		public float Interval { get; set; }

		[JsonIgnore]
		public override int Y
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public Ease.EaseType Ease { get; set; }

		public float Duration { get; set; }

		public bool ShouldSerializeTilingType() => Speed != null;

		public bool ShouldSerializeInterval() => TilingType == TilingTypes.Pulse;

		public enum TilingTypes
		{
			Scroll,
			Pulse
		}
	}
}
