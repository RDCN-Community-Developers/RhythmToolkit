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
		public RoomMaskTypes MaskType { get; set; }
		/// <summary>
		/// Gets or sets the alpha mode.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(MaskType)} != RhythmBase.RhythmDoctor.Events.RoomMaskTypes.None")]
		public MaskAlphaModes AlphaMode { get; set; }
		/// <summary>
		/// Gets or sets the source room.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(MaskType)} == RhythmBase.RhythmDoctor.Events.RoomMaskTypes.Room")]
		public byte SourceRoom { get; set; }
		/// <summary>
		/// Gets or sets the list of image assets.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(MaskType)} == RhythmBase.RhythmDoctor.Events.RoomMaskTypes.Image")]
		public List<string> Image { get; set; } = [];
		/// <summary>
		/// Gets or sets the frames per second.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Image)}.Count > 1")]
		public uint Fps { get; set; }
		/// <summary>
		/// Gets or sets the key color.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(MaskType)} == RhythmBase.RhythmDoctor.Events.RoomMaskTypes.Color")]
		public PaletteColor KeyColor { get; set; }
		/// <summary>
		/// Gets or sets the color cutoff value.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(MaskType)} == RhythmBase.RhythmDoctor.Events.RoomMaskTypes.Color")]
		public int ColorCutoff { get; set; }
		/// <summary>
		/// Gets or sets the color feathering value.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(MaskType)} == RhythmBase.RhythmDoctor.Events.RoomMaskTypes.Color")]
		public int ColorFeathering { get; set; }
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
		public RDRoom Room => new RDSingleRoom(checked((byte)Y));
	}
	/// <summary>
	/// Defines the types of masks available.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum RoomMaskTypes
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
	[RDJsonEnumSerializable]
	public enum MaskAlphaModes
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
}
