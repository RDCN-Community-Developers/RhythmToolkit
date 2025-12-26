using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an action to reorder a sprite in the Rhythm Doctor game.
/// </summary>
public class ReorderSprite : BaseDecorationAction
{
	///<inheritdoc/>
	public override EventType Type => EventType.ReorderSprite;
	///<inheritdoc/>
	public override Tab Tab => Tab.Decorations;

	/// <summary>
	/// Gets or sets the new room to which the sprite will be moved.
	/// </summary>
	[RDJsonConverter(typeof(RoomIndexConverter))]
	public RDRoomIndex NewRoom { get; set; } = RDRoomIndex.Room1;
	/// <summary>
	/// Gets or sets the depth level of the object.
	/// </summary>
	public int Depth { get; set; } = 0;
	/// <summary>
	/// Gets or sets the type of layer used for sorting or rendering purposes.
	/// </summary>
	[RDJsonProperty("sortingLayerName")]
	public LayerType LayerType { get; set; } = LayerType.Default;
}