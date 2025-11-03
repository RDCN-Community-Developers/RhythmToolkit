namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color BrightContrastSaturation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_BrightContrastSaturation")]
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
}