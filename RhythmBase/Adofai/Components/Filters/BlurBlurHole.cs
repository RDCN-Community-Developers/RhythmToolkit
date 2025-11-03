namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur BlurHole</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_BlurHole")]
public struct BlurBlurHole : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Radius</b>.
	/// </summary>
	[RDJsonProperty("_Radius")]
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SpotSize</b>.
	/// </summary>
	[RDJsonProperty("_SpotSize")]
	public float SpotSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_CenterX</b>.
	/// </summary>
	[RDJsonProperty("_CenterX")]
	public float CenterX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_CenterY</b>.
	/// </summary>
	[RDJsonProperty("_CenterY")]
	public float CenterY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_AlphaBlur</b>.
	/// </summary>
	[RDJsonProperty("_AlphaBlur")]
	public float AlphaBlur { get; set; }
}