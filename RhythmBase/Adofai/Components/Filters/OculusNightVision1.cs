namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision1</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Oculus_NightVision1")]
public struct OculusNightVision1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Vignette</b>.
	/// </summary>
	[RDJsonAlias("Vignette")]
	public float Vignette { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Linecount</b>.
	/// </summary>
	[RDJsonAlias("Linecount")]
	public float Linecount { get; set; }
}