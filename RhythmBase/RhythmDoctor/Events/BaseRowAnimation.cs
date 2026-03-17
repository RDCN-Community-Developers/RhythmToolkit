namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents the base class for row animations.
/// </summary>
[RDJsonObjectHasSerializer]
public abstract record class BaseRowAnimation : BaseRowAction, IBaseEvent
{
}
