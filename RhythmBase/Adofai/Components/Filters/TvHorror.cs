namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Horror</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Horror")]
public struct TvHorror : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}