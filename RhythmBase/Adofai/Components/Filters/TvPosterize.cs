namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Posterize</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Posterize")]
public struct TvPosterize : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Posterize</b>.
	/// </summary>
	[RDJsonProperty("Posterize")]
	public float Posterize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
}