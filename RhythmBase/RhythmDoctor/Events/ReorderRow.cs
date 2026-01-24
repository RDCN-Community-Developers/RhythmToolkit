using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an action to reorder a row in the Rhythm Doctor editor.  
/// </summary>  
public record class ReorderRow : BaseRowAction
{
	/// <inheritdoc/>  
	public override EventType Type => EventType.ReorderRow;

	/// <inheritdoc/>  
	public override Tab Tab => Tab.Rows;

	/// <summary>  
	/// Gets or sets the new room to which the row will be moved.  
	/// </summary>  
	[RDJsonDefaultSerializer]
	[RDJsonConverter(typeof(RoomIndexConverter))]
	[RDJsonCondition($"$&.{nameof(NewRoom)} is not null")]
	public RDRoomIndex? NewRoom { get; set; }
	/// <summary>
	/// Gets or sets the order of the room. The default value is <see langword="0"/>.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Order)} is not null")]
	public int? Order { get; set; }
	/// <summary>  
	/// Gets or sets the transition type for reordering the row.  
	/// </summary>  
	public Transition Transition { get; set; } = Transition.Smooth;
}
