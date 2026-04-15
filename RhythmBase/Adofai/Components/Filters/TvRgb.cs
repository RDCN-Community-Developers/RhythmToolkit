namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Rgb</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Rgb")]
[RDJsonObjectSerializable]
public struct TvRgb : IFilter
{
	public FilterType Type => FilterType.TvRgb;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}