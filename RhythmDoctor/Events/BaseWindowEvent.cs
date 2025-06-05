using Newtonsoft.Json;

using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents the base class for all window-related events.  
	/// Inherits from <see cref="BaseEvent"/> and provides additional functionality specific to window events.  
	/// </summary>  
	public abstract class BaseWindowEvent : BaseEvent
	{
		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Windows;

		/// <summary>  
		/// Gets the target window for this event.  
		/// This is derived from the <see cref="BaseEvent.Y"/> property.  
		/// </summary>  
		[JsonIgnore]
		public RDSingleRoom TargetWindow => new((byte)Y);
	}
}
