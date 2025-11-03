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
	[RDJsonProperty("Cryptage")]
	public float Cryptage { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	[RDJsonProperty("Parasite")]
	public float Parasite { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite2</b>.
	/// </summary>
	[RDJsonProperty("Parasite2")]
	public float Parasite2 { get; set; }
}