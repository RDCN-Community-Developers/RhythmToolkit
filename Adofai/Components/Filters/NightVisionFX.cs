namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>NightVisionFX</b>.
/// </summary>
public struct NightVisionFX : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Greenness</b>.
	/// </summary>
	public float Greenness { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vignette</b>.
	/// </summary>
	public float Vignette { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vignette_Alpha</b>.
	/// </summary>
	[RDJsonProperty("Vignette_Alpha")]
	public float VignetteAlpha { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Noise</b>.
	/// </summary>
	public float Noise { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Light</b>.
	/// </summary>
	public float Light { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Light2</b>.
	/// </summary>
	public float Light2 { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Line</b>.
	/// </summary>
	public float Line { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Binocular_Size</b>.
	/// </summary>
	[RDJsonProperty("_Binocular_Size")]
	public float BinocularSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Binocular_Smooth</b>.
	/// </summary>
	[RDJsonProperty("_Binocular_Smooth")]
	public float BinocularSmooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Binocular_Dist</b>.
	/// </summary>
	[RDJsonProperty("_Binocular_Dist")]
	public float BinocularDist { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_NightVisionFX";
#else
	public static string Name => "CameraFilterPack_NightVisionFX";
#endif
}