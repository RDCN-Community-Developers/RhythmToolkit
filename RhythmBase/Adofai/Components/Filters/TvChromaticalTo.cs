namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Chromatical2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Chromatical2")]
public struct TvChromaticalTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Aberration</b>.
	/// </summary>
	[RDJsonAlias("Aberration")]
	public float Aberration { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ZoomFade</b>.
	/// </summary>
	[RDJsonAlias("ZoomFade")]
	public float ZoomFade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ZoomSpeed</b>.
	/// </summary>
	[RDJsonAlias("ZoomSpeed")]
	public float ZoomSpeed { get; set; }
}