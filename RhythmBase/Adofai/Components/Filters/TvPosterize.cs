namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Posterize</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Posterize")]
[RDJsonObjectSerializable]
public struct TvPosterize : IFilter
{
	public FilterType Type => FilterType.TvPosterize;
	/// <summary>
	/// Gets or sets the value of the <b>Posterize</b>.
	/// </summary>
	[RDJsonAlias("Posterize")]
	public float Posterize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}