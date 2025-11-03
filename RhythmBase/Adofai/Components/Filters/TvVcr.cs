namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Vcr</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Vcr")]
public struct TvVcr : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}