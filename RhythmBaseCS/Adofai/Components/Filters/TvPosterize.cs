namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Posterize</b>.
/// </summary>
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
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Posterize";
#else
	public static string Name => "CameraFilterPack_TV_Posterize";
#endif
}