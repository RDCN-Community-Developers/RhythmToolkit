using RhythmBase.Components;
using sly.lexer.fsm.transitioncheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Events
{
	/// <summary>  
	/// Represents an action to change the character in a row event.  
	/// </summary>  
	public class ChangeCharacter : BaseRowAction
	{
		/// <inheritdoc/>  
		public override EventType Type => EventType.ChangeCharacter;

		/// <inheritdoc/>  
		public override Tabs Tab => Tabs.Rows;

		/// <summary>  
		/// Gets or sets the character to be changed to.  
		/// </summary>  
		public RDCharacter Character { get; set; } = new RDCharacter();

		/// <summary>  
		/// Gets or sets the transition type for the character change.  
		/// </summary>  
		public Transitions Transition { get; set; } = Transitions.Instant;
	}
}
