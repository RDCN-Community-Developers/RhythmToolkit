using Newtonsoft.Json;
using RhythmBase.Assets;
using RhythmBase.Components;

namespace RhythmBase.Events
{
	public class MaskRoom : BaseEvent
	{
		public MaskRoom()
		{
			KeyColor = new PaletteColor(false);
			Type = EventType.MaskRoom;
			Tab = Tabs.Rooms;
		}
		public MaskTypes MaskType { get; set; }
		public AlphaModes AlphaMode { get; set; }
		public byte SourceRoom { get; set; }
		public List<Asset<ImageFile>> Image { get; set; }
		public uint Fps { get; set; }
		public PaletteColor KeyColor { get; set; }
		public float ColorCutoff { get; set; }
		public float ColorFeathering { get; set; }
		public ContentModes ContentMode { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		[JsonIgnore]
		public Room Room
		{
			get
			{
				return new SingleRoom(checked((byte)Y));
			}
		}
		public enum MaskTypes
		{
			Image,
			Room,
			Color,
			None
		}
		public enum AlphaModes
		{
			Normal,
			Inverted
		}
		public enum ContentModes
		{
			ScaleToFill
		}
	}
}
