using System;

namespace RhythmBase.Events
{

	public class ShowDialogue : BaseEvent
	{

		public ShowDialogue()
		{
			Speed = 1;
			Type = EventType.ShowDialogue;
			Tab = Tabs.Actions;
		}


		public string Text { get; set; }


		public Sides PanelSide { get; set; }


		public PortraitSides PortraitSide { get; set; }


		public int Speed { get; set; }


		public bool PlayTextSounds { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public override string ToString() => base.ToString() + string.Format(" {0}", Text);


		public enum Sides
		{

			Bottom,

			Top
		}


		public enum PortraitSides
		{

			Left,

			Right
		}
	}
}
