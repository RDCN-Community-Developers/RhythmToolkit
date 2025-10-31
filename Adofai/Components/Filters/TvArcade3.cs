namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV ARCADE 3</b>.
/// </summary>
public struct TvArcade3 : IFilter
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
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_ARCADE_3";
#else
	public static string Name => "CameraFilterPack_TV_ARCADE_3";
#endif
}