namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Old Movie 2</b>.
/// </summary>
public struct TvOldMovieTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FramePerSecond</b>.
	/// </summary>
	[RDJsonProperty("FramePerSecond")]
	public float FramePerSecond { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonProperty("Contrast")]
	public float Contrast { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>SceneCut</b>.
	/// </summary>
	[RDJsonProperty("SceneCut")]
	public float SceneCut { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Old_Movie_2";
#else
	public static string Name => "CameraFilterPack_TV_Old_Movie_2";
#endif
}