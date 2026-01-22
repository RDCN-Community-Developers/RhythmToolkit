namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Noise</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Noise")]
public struct TvNoise : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}