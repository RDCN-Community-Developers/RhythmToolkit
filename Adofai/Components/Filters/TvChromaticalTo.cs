namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Chromatical2</b>.
/// </summary>
public struct TvChromaticalTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Aberration</b>.
	/// </summary>
	public float Aberration { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ZoomFade</b>.
	/// </summary>
	public float ZoomFade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ZoomSpeed</b>.
	/// </summary>
	public float ZoomSpeed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Chromatical2";
#else
	public static string Name => "CameraFilterPack_TV_Chromatical2";
#endif
}