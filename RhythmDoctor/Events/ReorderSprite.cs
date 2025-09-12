using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an action to reorder a sprite in the Rhythm Doctor game.
	/// </summary>
	public class ReorderSprite : BaseDecorationAction
	{
		/// <summary>
		/// Gets the type of the event, which is specific to reordering sprites.
		/// </summary>
		/// <inheritdoc />
		public override EventType Type => EventType.ReorderSprite;

		/// <summary>
		/// Gets the tab where this action is categorized in the editor.
		/// </summary>
		/// <inheritdoc />
		public override Tabs Tab => Tabs.Decorations;

		/// <summary>
		/// Gets or sets the new room to which the sprite will be moved.
		/// </summary>
		[RDJsonDefaultSerializer]
		public RDRoomIndex NewRoom { get; set; } = 0;
		public int Depth { get; set; } = 0;
	}
}
