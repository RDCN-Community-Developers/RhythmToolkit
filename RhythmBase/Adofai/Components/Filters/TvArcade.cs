namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV ARCADE</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_ARCADE")]
[RDJsonObjectSerializable]
public struct TvArcade : IFilter
{
	public FilterType Type => FilterType.TvArcade;
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}