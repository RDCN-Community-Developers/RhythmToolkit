using System;
using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class TintRows : BaseRowAnimation, IEaseEvent
	{

		public TintRows()
		{
			TintColor = new PaletteColor(true);
			BorderColor = new PaletteColor(true);
			Type = EventType.TintRows;
			Tab = Tabs.Actions;
		}


		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public PaletteColor TintColor { get; set; }


		public Ease.EaseType Ease { get; set; }


		public Borders Border { get; set; }


		[EaseProperty]
		public PaletteColor BorderColor { get; set; }


		[EaseProperty]
		public int Opacity { get; set; }


		public bool Tint { get; set; }


		public float Duration { get; set; }


		public RowEffect Effect { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		[JsonIgnore]
		public bool TintAll
		{
			get
			{
				return Parent != null;
			}
		}


		internal bool ShouldSerializeDuration() => Duration != 0f;


		internal bool ShouldSerializeEase() => Duration != 0f;


		public override string ToString() => base.ToString() + string.Format(" {0}{1}", Border, (Border == Borders.None) ? "" : (":" + BorderColor.ToString()));


		public enum RowEffect
		{

			None,

			Electric,

			Smoke
		}
	}
}
