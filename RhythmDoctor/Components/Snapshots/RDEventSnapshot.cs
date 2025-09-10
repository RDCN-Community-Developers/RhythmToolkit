using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components.Snapshots
{
	public class RDEventSnapshot
	{
		public RDBeat Beat { get; }
		public IBaseEvent Event { get; }
		internal RDEventSnapshot(IBaseEvent rdEvent, RDBeat beat)
		{
			Beat = beat;
			Event = rdEvent;
		}
	}
}
