namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation OilPaint</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Pixelisation_OilPaint")]
[RDJsonObjectSerializable]
public struct PixelisationOilPaint : IFilter
{
	public FilterType Type => FilterType.PixelisationOilPaint;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}