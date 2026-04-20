namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Adjust ColorRGB</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Adjust_ColorRGB")]
[RDJsonObjectSerializable]
public struct ColorsAdjustColorRGB : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.ColorsAdjustColorRGB;
}