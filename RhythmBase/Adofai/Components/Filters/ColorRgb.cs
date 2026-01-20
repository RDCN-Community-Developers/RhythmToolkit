namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color RGB</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_RGB")]
public struct ColorRgb : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>ColorRGB</b>.
	/// </summary>
	[RDJsonAlias("ColorRGB")]
	public RDColor ColorRGB { get; set; }
}