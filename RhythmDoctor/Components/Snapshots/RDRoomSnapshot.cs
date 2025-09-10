using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;

namespace RhythmBase.RhythmDoctor.Components.Snapshots
{
	public class RDRoomSnapshot
	{
		public RDBeat Beat { get; }
		public RDRoomIndex Index { get; }
		public SetVFXPreset? VFX { get; }
		internal RDRoomSnapshot(RDLevel level, RDRoomIndex index, RDBeat beat)
		{
			Beat = beat;
			Index = index;
			//VFX = level.LastOrDefault<SetVFXPreset>(beat, e => (e.Rooms & index) != 0);
		}
	}
}
