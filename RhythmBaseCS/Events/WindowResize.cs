using Newtonsoft.Json;
using RhythmBase.Components.Easing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Events
{
	/// <summary>  
	/// Represents a window resize event.  
	/// </summary>
	public class WindowResize : BaseEvent, IEaseEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.WindowResize;
		/// <inheritdoc/>
		public override Tabs Tab => CustomTab;
		/// <summary>
		/// Gets or sets the custom tab.
		/// </summary>
		[JsonProperty("tab")]
		public Tabs CustomTab
		{
			get; set
			{
				if (CustomTab is Tabs.Actions or Tabs.Windows)
					field = value;
				throw new InvalidOperationException();
			}
		}
		/// <inheritdoc/>
		public EaseType Ease { get; set; }
		/// <inheritdoc/>
		public float Duration { get; set; }
	}
}
