using RhythmBase.Assets;
using RhythmBase.Components;
using RhythmBase.Converters;
using System.Text.Json.Serialization;
namespace RhythmBase.Events
{
	public class SetBackgroundColor : BaseEvent, IEaseEvent, IRoomEvent
	{
		public SetBackgroundColor()
		{
			Rooms = new Room(false, new byte[1]);
			Color = new PaletteColor(true);
			Type = EventType.SetBackgroundColor;
			Tab = Tabs.Actions;
		}
		public Room Rooms { get; set; }
		public Ease.EaseType Ease { get; set; }
		public ContentModes ContentMode { get; set; }
		public FilterModes Filter { get; set; }
		[EaseProperty]
		public PaletteColor Color { get; set; }
		public float Interval { get; set; }
		public BackgroundTypes BackgroundType { get; set; }
		public float Duration { get; set; }
		public int Fps { get; set; }
		public List<Asset<ImageFile>> Image { get; set; } = [];
		[EaseProperty]
		public int ScrollX { get; set; }
		[EaseProperty]
		public int ScrollY { get; set; }
		public TilingTypes TilingType { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public override string ToString() => BackgroundType == BackgroundTypes.Color
		? base.ToString() + string.Format(" {0}", Color.ToString())
		: base.ToString() + string.Format(" {0}", string.Join(',', Image.Select(i => i.Name)));
		public enum BackgroundTypes
		{
			Color,
			Image
		}
		public enum FilterModes
		{
			NearestNeighbor
		}
	}
}
