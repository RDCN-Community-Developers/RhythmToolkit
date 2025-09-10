using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components.Snapshots
{
	public class RDEventSnapshot<TEvent> : RDEventSnapshot where TEvent:IBaseEvent, IEaseEvent
	{
		internal RDEventSnapshot(TEvent rdEvent, RDBeat beat) : base(rdEvent, beat)
		{
		}
	}
}
