using RhythmBase.Components;
using sly.lexer.fsm.transitioncheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Events
{
	public class ReorderRow : BaseRowAction
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ReorderRow;
		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Rows;
		public RDSingleRoom NewRoom { get; set; } = RDRoomIndex.Room1;
		public Transitions Transition = Transitions.Smooth;
	}
}
