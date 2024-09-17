using System;
using Newtonsoft.Json;
namespace RhythmBase.Events
{
	public class TagAction : BaseEvent
	{
		public TagAction()
		{
			Type = EventType.TagAction;
			Tab = Tabs.Actions;
		}

		[JsonIgnore]
		public Actions Action { get; set; }

		[JsonProperty("Tag")]
		public string ActionTag { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", ActionTag);

		[Flags]
		public enum Actions
		{
			Run = 2,
			All = 1,
			Enable = 6,
			Disable = 4
		}

		public enum SpecialTag
		{
			onHit,
			onMiss,
			onHeldPressHit,
			onHeldReleaseHit,
			onHeldPressMiss,
			onHeldReleaseMiss,
			row0,
			row1,
			row2,
			row3,
			row4,
			row5,
			row6,
			row7,
			row8,
			row9,
			row10,
			row11,
			row12,
			row13,
			row14,
			row15
		}
	}
}
