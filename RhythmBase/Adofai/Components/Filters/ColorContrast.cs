namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Contrast</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Contrast")]
public struct ColorContrast : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonProperty("Contrast")]
	public float Contrast { get; set; }
}