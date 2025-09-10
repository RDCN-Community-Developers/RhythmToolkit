namespace RhythmBase.RhythmDoctor.Components.Snapshots
{
	public class RDDecorationSnapshot
	{
		public RDBeat Beat { get; }
		public Decoration Decoration { get; }
		internal RDDecorationSnapshot(Decoration decoration, RDBeat beat)
		{
			Beat = beat;
			Decoration = decoration;
		}
	}
}
