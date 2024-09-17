using System;
namespace RhythmBase.Events
{
	public class HideRow : BaseRowAnimation
	{
		public HideRow()
		{
			Type = EventType.HideRow;
			Tab = Tabs.Actions;
		}

		public Transitions Transition { get; set; }

		public Shows Show { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public enum Transitions
		{
			Smooth,
			Instant,
			Full
		}

		public enum Shows
		{
			Visible,
			Hidden,
			OnlyCharacter,
			OnlyRow
		}
	}
}
