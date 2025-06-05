using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public override EventType Type => throw new NotImplementedException();

		/// <summary>
		/// Gets the tab where this action is categorized in the editor.
		/// </summary>
		/// <inheritdoc />
		public override Tabs Tab => throw new NotImplementedException();

		/// <summary>
		/// Gets or sets the new room to which the sprite will be moved.
		/// </summary>
		public RDSingleRoom NewRoom { get; set; } = RDRoomIndex.Room1;
	}
}
