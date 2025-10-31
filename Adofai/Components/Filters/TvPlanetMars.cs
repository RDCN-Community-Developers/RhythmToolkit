namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV PlanetMars</b>.
/// </summary>
public struct TvPlanetMars : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_PlanetMars";
#else
	public static string Name => "CameraFilterPack_TV_PlanetMars";
#endif
}