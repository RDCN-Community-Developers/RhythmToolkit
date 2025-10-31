namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>OldFilm Cutting1</b>.
/// </summary>
public struct OldFilmCutting1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Luminosity</b>.
	/// </summary>
	[RDJsonProperty("Luminosity")]
	public float Luminosity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vignette</b>.
	/// </summary>
	[RDJsonProperty("Vignette")]
	public float Vignette { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_OldFilm_Cutting1";
#else
	public static string Name => "CameraFilterPack_OldFilm_Cutting1";
#endif
}