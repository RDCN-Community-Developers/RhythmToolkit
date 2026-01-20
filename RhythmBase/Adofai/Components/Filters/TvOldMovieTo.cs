namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Old Movie 2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Old_Movie_2")]
public struct TvOldMovieTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FramePerSecond</b>.
	/// </summary>
	[RDJsonAlias("FramePerSecond")]
	public float FramePerSecond { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonAlias("Contrast")]
	public float Contrast { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>SceneCut</b>.
	/// </summary>
	[RDJsonAlias("SceneCut")]
	public float SceneCut { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}