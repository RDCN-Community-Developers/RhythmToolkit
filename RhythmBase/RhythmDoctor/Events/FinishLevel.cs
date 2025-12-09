namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that occurs when a level is finished.
/// </summary>
public class FinishLevel : BaseEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.FinishLevel;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}
