namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors HSV</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_HSV")]
public struct ColorsHsv : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_HueShift</b>.
	/// </summary>
	[RDJsonProperty("_HueShift")]
	public float HueShift { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Saturation</b>.
	/// </summary>
	[RDJsonProperty("_Saturation")]
	public float Saturation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ValueBrightness</b>.
	/// </summary>
	[RDJsonProperty("_ValueBrightness")]
	public float ValueBrightness { get; set; }
}