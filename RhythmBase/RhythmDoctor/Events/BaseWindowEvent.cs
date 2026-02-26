using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents the base class for all window-related events.  
/// </summary>  
[RDJsonObjectHasSerializer]
public abstract record class BaseWindowEvent : BaseEvent
{
	/// <inheritdoc/>
	public override Tab Tab => Tab.Windows;

	/// <summary>  
	/// Gets the target window for this event.  
	/// </summary>
	public int TargetWindow { get => Y; set => Y = value; }
}
