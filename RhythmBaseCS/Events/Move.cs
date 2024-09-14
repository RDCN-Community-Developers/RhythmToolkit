using System;
using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class Move : BaseDecorationAction, IEaseEvent
	{

		public Move()
		{
			Type = EventType.Move;
			Tab = Tabs.Decorations;
		}


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		[EaseProperty]
		public PointE? Position { get; set; }


		[EaseProperty]
		public PointE? Scale { get; set; }


		[EaseProperty]
		public Expression? Angle { get; set; }


		[EaseProperty]
		public PointE? Pivot { get; set; }


		public float Duration { get; set; }


		public Ease.EaseType Ease { get; set; }


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


		public override string ToString() => base.ToString();
	}
}
