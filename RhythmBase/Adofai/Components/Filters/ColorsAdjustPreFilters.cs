namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Adjust PreFilters</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Adjust_PreFilters")]
[RDJsonObjectSerializable]
public struct ColorsAdjustPreFilters : IFilter
{
	public FilterType Type => FilterType.ColorsAdjustPreFilters;
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonAlias("FadeFX")]
	public float FadeFX { get; set; }
}