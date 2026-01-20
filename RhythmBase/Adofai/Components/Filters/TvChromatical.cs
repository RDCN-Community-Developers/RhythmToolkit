namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Chromatical</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Chromatical")]
public struct TvChromatical : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}