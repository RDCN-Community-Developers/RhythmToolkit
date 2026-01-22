namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV CompressionFX</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_CompressionFX")]
public struct TvCompressionFX : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	[RDJsonAlias("Parasite")]
	public float Parasite { get; set; }
}