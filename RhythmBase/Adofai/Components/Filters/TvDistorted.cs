namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Distorted</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Distorted")]
public struct TvDistorted : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>RGB</b>.
	/// </summary>
	[RDJsonProperty("RGB")]
	public float Rgb { get; set; }
}