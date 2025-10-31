namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV ARCADE Fast</b>.
/// </summary>
public struct TvArcadeFast : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Interferance_Size</b>.
	/// </summary>
	[RDJsonProperty("Interferance_Size")]
	public float InterferanceSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Interferance_Speed</b>.
	/// </summary>
	[RDJsonProperty("Interferance_Speed")]
	public float InterferanceSpeed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonProperty("Contrast")]
	public float Contrast { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_ARCADE_Fast";
#else
	public static string Name => "CameraFilterPack_TV_ARCADE_Fast";
#endif
}