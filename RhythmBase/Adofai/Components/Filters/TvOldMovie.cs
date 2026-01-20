namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Old Movie</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Old_Movie")]
public struct TvOldMovie : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}