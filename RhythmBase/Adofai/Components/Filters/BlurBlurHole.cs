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
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Radius</b>.
	/// </summary>
	[RDJsonAlias("_Radius")]
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SpotSize</b>.
	/// </summary>
	[RDJsonAlias("_SpotSize")]
	public float SpotSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_CenterX</b>.
	/// </summary>
	[RDJsonAlias("_CenterX")]
	public float CenterX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_CenterY</b>.
	/// </summary>
	[RDJsonAlias("_CenterY")]
	public float CenterY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_AlphaBlur</b>.
	/// </summary>
	[RDJsonAlias("_AlphaBlur")]
	public float AlphaBlur { get; set; }
}