namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation OilPaintHQ</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Pixelisation_OilPaintHQ")]
[RDJsonObjectSerializable]
public struct PixelisationOilPaintHQ : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.PixelisationOilPaintHQ;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}