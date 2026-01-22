namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Noise TV 2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Noise_TV_2")]
public struct NoiseTvTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}