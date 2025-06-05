using RhythmBase.RhythmDoctor.Components;

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
		public RDSingleRoom NewRoom { get; set; } = RDRoomIndex.Room1;

		/// <summary>  
		/// Gets or sets the transition type for reordering the row.  
		/// </summary>  
		public Transitions Transition = Transitions.Smooth;
	}
}
