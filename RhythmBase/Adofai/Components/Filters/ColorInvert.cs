namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Invert</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Invert")]
[RDJsonObjectSerializable]
public struct ColorInvert : IFilter
{
	public FilterType Type => FilterType.ColorInvert;
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonAlias("_Fade")]
	public float Fade { get; set; }
}