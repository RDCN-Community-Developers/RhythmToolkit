namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Vintage</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Vintage")]
[RDJsonObjectSerializable]
public struct TvVintage : IFilter
{
	public FilterType Type => FilterType.TvVintage;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}