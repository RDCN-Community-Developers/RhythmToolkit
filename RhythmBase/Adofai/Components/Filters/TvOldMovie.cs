namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Old Movie</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Old_Movie")]
[RDJsonObjectSerializable]
public struct TvOldMovie : IFilter
{
	public FilterType Type => FilterType.TvOldMovie;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}