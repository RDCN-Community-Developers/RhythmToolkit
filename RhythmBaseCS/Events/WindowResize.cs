using Newtonsoft.Json;
using RhythmBase.Components.Easing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Events
{
	public class WindowResize : BaseEvent, IEaseEvent
	{
		/// <inheritdoc/>
		public override EventType Type => throw new NotImplementedException();
		/// <inheritdoc/>
		public override Tabs Tab => throw new NotImplementedException();
		/// <summary>
		/// Gets or sets the custom tab.
		/// </summary>
		[JsonProperty("tab")]
		public Tabs CustomTab { get; set; }
		/// <inheritdoc/>
		public EaseType Ease { get; set; }
		/// <inheritdoc/>
		public float Duration { get; set; }
	}
}
