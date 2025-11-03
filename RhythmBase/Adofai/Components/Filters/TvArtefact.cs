namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Artefact</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Artefact")]
public struct TvArtefact : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Colorisation</b>.
	/// </summary>
	[RDJsonProperty("Colorisation")]
	public float Colorisation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	[RDJsonProperty("Parasite")]
	public float Parasite { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Noise</b>.
	/// </summary>
	[RDJsonProperty("Noise")]
	public float Noise { get; set; }
}