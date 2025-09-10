namespace RhythmBase.RhythmDoctor.Components.Snapshots
{
	public class RDRowSnapshot
	{
		public RDBeat Beat { get; }
		public Row Row { get; }
		internal RDRowSnapshot(Row row, RDBeat beat)
		{
			Beat = beat;
			Row = row;
		}
	}
}
