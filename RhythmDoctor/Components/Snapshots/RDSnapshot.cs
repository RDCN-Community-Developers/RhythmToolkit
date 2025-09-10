using RhythmBase.RhythmDoctor.Events;
using System.Collections.ObjectModel;

namespace RhythmBase.RhythmDoctor.Components.Snapshots
{
	public class RDSnapshot
	{
		public RDBeat Beat { get; }
		public ReadOnlyDictionary<RDRoomIndex, RDRoomSnapshot> RoomSnapshots { get; }
		public ReadOnlyCollection<RDRowSnapshot> RowSnapshots { get; }
		public ReadOnlyCollection<RDDecorationSnapshot> DecorationSnapshots { get; }
		internal RDSnapshot(RDLevel level, RDBeat beat)
		{
			Beat = beat;
		}
	}
}
