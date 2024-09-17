using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class CustomFlash : BaseEvent, IEaseEvent, IRoomEvent
	{
		public CustomFlash()
		{
			Rooms = new Room(true, new byte[1]);
			StartColor = new PaletteColor(false);
			EndColor = new PaletteColor(false);
			Type = EventType.CustomFlash;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public Ease.EaseType Ease { get; set; }

		public PaletteColor StartColor { get; set; }

		public bool Background { get; set; }

		[EaseProperty]
		public PaletteColor EndColor { get; set; }

		public float Duration { get; set; }

		public int StartOpacity { get; set; }

		[EaseProperty]
		public int EndOpacity { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}=>{1}", StartColor, EndColor);
	}
}
