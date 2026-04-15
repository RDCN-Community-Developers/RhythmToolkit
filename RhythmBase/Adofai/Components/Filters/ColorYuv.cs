namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color YUV</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_YUV")]
[RDJsonObjectSerializable]
public struct ColorYuv : IFilter
{
	public FilterType Type => FilterType.ColorYuv;
}