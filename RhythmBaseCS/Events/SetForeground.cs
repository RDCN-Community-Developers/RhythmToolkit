using RhythmBase.Assets;
using RhythmBase.Components;
using System.Linq;

namespace RhythmBase.Events
{
	public class SetForeground : BaseEvent, IEaseEvent, IRoomEvent
	{
		public SetForeground()
		{
			Rooms = new Room(false, new byte[1]);
			Color = new PaletteColor(true);
			Type = EventType.SetForeground;
			Tab = Tabs.Actions;
		}
		public Room Rooms { get; set; }
		public ContentModes ContentMode { get; set; }
		public TilingTypes TilingType { get; set; }
		[EaseProperty]
		public PaletteColor Color { get; set; }
		public List<string> Image { get; set; }
		public float Fps { get; set; }
		[EaseProperty]
		public float ScrollX { get; set; }
		[EaseProperty]
		public float ScrollY { get; set; }
		public float Duration { get; set; }
		public float Interval { get; set; }
		public Ease.EaseType Ease { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public override string ToString() => base.ToString() + $" {Color},{string.Join(',', Image.Select(i => i.ToString()))}";
	}
}
