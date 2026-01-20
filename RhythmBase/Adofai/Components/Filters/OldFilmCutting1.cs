namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>OldFilm Cutting1</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_OldFilm_Cutting1")]
public struct OldFilmCutting1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Luminosity</b>.
	/// </summary>
	[RDJsonAlias("Luminosity")]
	public float Luminosity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vignette</b>.
	/// </summary>
	[RDJsonAlias("Vignette")]
	public float Vignette { get; set; }
}