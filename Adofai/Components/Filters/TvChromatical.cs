namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Chromatical</b>.
/// </summary>
public struct TvChromatical : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Chromatical";
#else
	public static string Name => "CameraFilterPack_TV_Chromatical";
#endif
}