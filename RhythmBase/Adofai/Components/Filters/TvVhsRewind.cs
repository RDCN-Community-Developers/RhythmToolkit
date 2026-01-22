namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV VHS Rewind</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_VHS_Rewind")]
public struct TvVhsRewind : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Cryptage</b>.
	/// </summary>
	[RDJsonAlias("Cryptage")]
	public float Cryptage { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	[RDJsonAlias("Parasite")]
	public float Parasite { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite2</b>.
	/// </summary>
	[RDJsonAlias("Parasite2")]
	public float Parasite2 { get; set; }
}