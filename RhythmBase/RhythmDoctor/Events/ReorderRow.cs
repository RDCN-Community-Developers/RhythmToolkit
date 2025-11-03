using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an action to reorder a row in the Rhythm Doctor editor.  
	/// </summary>  
	public class ReorderRow : BaseRowAction
	{
		/// <inheritdoc/>  
		public override EventType Type => EventType.ReorderRow;

		/// <inheritdoc/>  
		public override Tabs Tab => Tabs.Rows;

		/// <summary>  
		/// Gets or sets the new room to which the row will be moved.  
		/// </summary>  
		[RDJsonDefaultSerializer]
		[RDJsonConverter(typeof(RoomIndexConverter))]
		public RDRoomIndex NewRoom { get; set; } = RDRoomIndex.Room1;
		/// <summary>
		/// Gets or sets the order of the room. The default value is <see langword="0"/>.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Order)} is not null")]
		public int? Order { get; set; }
		/// <summary>  
		/// Gets or sets the transition type for reordering the row.  
		/// </summary>  
		public Transitions Transition { get; set; } = Transitions.Smooth;
	}
}
