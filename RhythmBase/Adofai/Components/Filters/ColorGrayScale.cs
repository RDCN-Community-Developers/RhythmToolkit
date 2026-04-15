namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color GrayScale</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_GrayScale")]
[RDJsonObjectSerializable]
public struct ColorGrayScale : IFilter
{
	public FilterType Type => FilterType.ColorGrayScale;
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonAlias("_Fade")]
	public float Fade { get; set; }
}