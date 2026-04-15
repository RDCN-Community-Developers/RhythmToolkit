namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color RGB</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_RGB")]
[RDJsonObjectSerializable]
public struct ColorRgb : IFilter
{
	public FilterType Type => FilterType.ColorRgb;
	/// <summary>
	/// Gets or sets the value of the <b>ColorRGB</b>.
	/// </summary>
	[RDJsonAlias("ColorRGB")]
	public RDColor ColorRGB { get; set; }
}