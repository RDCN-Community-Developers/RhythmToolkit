namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation OilPaintHQ</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Pixelisation_OilPaintHQ")]
public struct PixelisationOilPaintHQ : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}