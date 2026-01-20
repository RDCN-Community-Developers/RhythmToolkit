namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV PlanetMars</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_PlanetMars")]
public struct TvPlanetMars : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}