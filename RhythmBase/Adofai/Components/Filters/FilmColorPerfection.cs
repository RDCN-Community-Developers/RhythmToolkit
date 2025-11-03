namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Film ColorPerfection</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Film_ColorPerfection")]
public struct FilmColorPerfection : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Gamma</b>.
	/// </summary>
	[RDJsonProperty("Gamma")]
	public float Gamma { get; set; }
}