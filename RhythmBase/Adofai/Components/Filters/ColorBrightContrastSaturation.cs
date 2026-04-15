namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color BrightContrastSaturation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_BrightContrastSaturation")]
[RDJsonObjectSerializable]
public struct ColorBrightContrastSaturation : IFilter
{
	public FilterType Type => FilterType.ColorBrightContrastSaturation;
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