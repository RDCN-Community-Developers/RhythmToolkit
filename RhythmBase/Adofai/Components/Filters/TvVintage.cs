namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Vintage</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Vintage")]
public struct TvVintage : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}