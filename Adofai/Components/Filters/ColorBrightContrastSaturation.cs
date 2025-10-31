namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color BrightContrastSaturation</b>.
/// </summary>
public struct ColorBrightContrastSaturation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Brightness</b>.
	/// </summary>
	[RDJsonProperty("Brightness")]
	public float Brightness { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Saturation</b>.
	/// </summary>
	[RDJsonProperty("Saturation")]
	public float Saturation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonProperty("Contrast")]
	public float Contrast { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_BrightContrastSaturation";
#else
	public static string Name => "CameraFilterPack_Color_BrightContrastSaturation";
#endif
}