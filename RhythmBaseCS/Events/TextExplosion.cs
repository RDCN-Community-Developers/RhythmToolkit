using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class TextExplosion : BaseEvent, IRoomEvent
	{
		public TextExplosion()
		{
			Rooms = new Room(false, new byte[1]);
			Color = new PaletteColor(false);
			Type = EventType.TextExplosion;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public PaletteColor Color { get; set; }

		public string Text { get; set; }

		public Directions Direction { get; set; }

		public Modes Mode { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", Text);

		public enum Directions
		{
			Left,
			Right
		}

		public enum Modes
		{
			OneColor,
			Random
		}
	}
}
