using RhythmBase.RhythmDoctor.Extensions;

namespace RhythmBase.RhythmDoctor.Events;

public interface IAdvanceText : IBaseEvent, IDurationEvent
{
	float? Duration { get; set; }
}