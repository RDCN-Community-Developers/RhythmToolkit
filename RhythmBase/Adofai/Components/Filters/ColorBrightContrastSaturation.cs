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
	[RDJsonAlias("Brightness")]
	public float Brightness { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Saturation</b>.
	/// </summary>
	[RDJsonAlias("Saturation")]
	public float Saturation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonAlias("Contrast")]
	public float Contrast { get; set; }
}