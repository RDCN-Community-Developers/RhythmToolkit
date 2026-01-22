namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>NightVisionFX</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_NightVisionFX")]
public struct NightVisionFX : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Greenness</b>.
	/// </summary>
	[RDJsonAlias("Greenness")]
	public float Greenness { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vignette</b>.
	/// </summary>
	[RDJsonAlias("Vignette")]
	public float Vignette { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vignette_Alpha</b>.
	/// </summary>
	[RDJsonAlias("Vignette_Alpha")]
	public float VignetteAlpha { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Noise</b>.
	/// </summary>
	[RDJsonAlias("Noise")]
	public float Noise { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Light</b>.
	/// </summary>
	[RDJsonAlias("Light")]
	public float Light { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Light2</b>.
	/// </summary>
	[RDJsonAlias("Light2")]
	public float Light2 { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Line</b>.
	/// </summary>
	[RDJsonAlias("Line")]
	public float Line { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Binocular_Size</b>.
	/// </summary>
	[RDJsonAlias("_Binocular_Size")]
	public float BinocularSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Binocular_Smooth</b>.
	/// </summary>
	[RDJsonAlias("_Binocular_Smooth")]
	public float BinocularSmooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Binocular_Dist</b>.
	/// </summary>
	[RDJsonAlias("_Binocular_Dist")]
	public float BinocularDist { get; set; }
}