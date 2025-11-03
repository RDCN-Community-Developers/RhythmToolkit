namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Rgb</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Rgb")]
public struct TvRgb : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}