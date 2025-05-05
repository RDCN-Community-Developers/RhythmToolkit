using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Events
{
	/// <summary>  
	/// Represents a blend decoration action in the rhythm base.  
	/// </summary>  
	public class Blend : BaseDecorationAction
	{
		/// <summary>  
		/// Gets the type of the event, which is <see cref="EventType.Blend"/>.  
		/// </summary>  
		public override EventType Type => EventType.Blend;

		/// <summary>  
		/// Gets the tab where this action is categorized, which is <see cref="Tabs.Decorations"/>.  
		/// </summary>  
		public override Tabs Tab => Tabs.Decorations;

		/// <summary>  
		/// Gets or sets the type of blend effect to apply.  
		/// </summary>  
		public BlendTypes BlendType { get; set; } = BlendTypes.None;

		/// <summary>  
		/// Specifies the different types of blend effects available.  
		/// </summary>  
		public enum BlendTypes
		{
			/// <summary>  
			/// No blend effect.  
			/// </summary>  
			None,
		}
	}
}
