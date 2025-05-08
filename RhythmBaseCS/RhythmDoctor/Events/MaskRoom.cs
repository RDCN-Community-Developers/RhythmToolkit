using Newtonsoft.Json;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a MaskRoom event in the RhythmBase system.
	/// </summary>
	public class MaskRoom : BaseEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MaskRoom"/> class.
		/// </summary>
		public MaskRoom()
		{
			KeyColor = new PaletteColor(false);
			Type = EventType.MaskRoom;
			Tab = Tabs.Rooms;
		}

		/// <summary>
		/// Gets or sets the type of the mask.
		/// </summary>
		public MaskTypes MaskType { get; set; }

		/// <summary>
		/// Gets or sets the alpha mode.
		/// </summary>
		public AlphaModes AlphaMode { get; set; }

		/// <summary>
		/// Gets or sets the source room.
		/// </summary>
		public byte SourceRoom { get; set; }

		/// <summary>
		/// Gets or sets the list of image assets.
		/// </summary>
		public List<string> Image { get; set; } = [];

		/// <summary>
		/// Gets or sets the frames per second.
		/// </summary>
		public uint Fps { get; set; }

		/// <summary>
		/// Gets or sets the key color.
		/// </summary>
		public PaletteColor KeyColor { get; set; }

		/// <summary>
		/// Gets or sets the color cutoff value.
		/// </summary>
		public int ColorCutoff { get; set; }

		/// <summary>
		/// Gets or sets the color feathering value.
		/// </summary>
		public int ColorFeathering { get; set; }

		/// <summary>
		/// Gets or sets the content mode.
		/// </summary>
		public ContentModes ContentMode { get; set; }

		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab type.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Gets the room associated with the event.
		/// </summary>
		[JsonIgnore]
		public RDRoom Room => new RDSingleRoom(checked((byte)Y));

		/// <summary>
		/// Defines the types of masks available.
		/// </summary>
		public enum MaskTypes
		{
			/// <summary>
			/// Uses an image as the mask.
			/// </summary>
			Image,
			/// <summary>
			/// Uses a room as the mask.
			/// </summary>
			Room,
			/// <summary>
			/// Uses a color as the mask.
			/// </summary>
			Color,
			/// <summary>
			/// No mask is applied.
			/// </summary>
			None
		}

		/// <summary>
		/// Defines the alpha modes available.
		/// </summary>
		public enum AlphaModes
		{
			/// <summary>
			/// Normal alpha mode.
			/// </summary>
			Normal,
			/// <summary>
			/// Inverted alpha mode.
			/// </summary>
			Inverted
		}

		/// <summary>
		/// Defines the content modes available.
		/// </summary>
		public enum ContentModes
		{
			/// <summary>
			/// Scales the content to fill the area.
			/// </summary>
			ScaleToFill
		}
	}
}
