using System;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class PaintHands : BaseEvent, IEaseEvent, IRoomEvent
	{

		public PaintHands()
		{
			TintColor = new PaletteColor(true);
			BorderColor = new PaletteColor(true);
			Rooms = new Room(true, new byte[1]);
			Type = EventType.PaintHands;
			Tab = Tabs.Actions;
		}


		[EaseProperty]
		public PaletteColor TintColor { get; set; }


		public Ease.EaseType Ease { get; set; }


		public Borders Border { get; set; }


		[EaseProperty]
		public PaletteColor BorderColor { get; set; }


		[EaseProperty]
		public int Opacity { get; set; }


		public bool Tint { get; set; }


		public float Duration { get; set; }


		public Room Rooms { get; set; }


		public PlayerHands Hands { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public enum Borders
		{

			None,

			Outline,

			Glow
		}
	}
}
