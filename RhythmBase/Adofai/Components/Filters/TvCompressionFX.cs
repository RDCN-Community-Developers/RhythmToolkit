namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV CompressionFX</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_CompressionFX")]
[RDJsonObjectSerializable]
public struct TvCompressionFX : IFilter
{
	public FilterType Type => FilterType.TvCompressionFX;
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	[RDJsonAlias("Parasite")]
	public float Parasite { get; set; }
}