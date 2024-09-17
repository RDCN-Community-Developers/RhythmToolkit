using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveRow : BaseRowAnimation, IEaseEvent
	{
		public MoveRow()
		{
			Type = EventType.MoveRow;
			Tab = Tabs.Actions;
		}

		public bool CustomPosition { get; set; }

		public Targets Target { get; set; }

		[EaseProperty]
		public PointE? RowPosition { get; set; }

		[EaseProperty]
		public PointE? Scale { get; set; }

		[EaseProperty]
		public Expression? Angle { get; set; }

		[EaseProperty]
		public float? Pivot { get; set; }

		public float Duration { get; set; }

		public Ease.EaseType Ease { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public enum Targets
		{
			WholeRow,
			Heart,
			Character
		}
	}
}
