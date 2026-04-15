namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Sharpen Sharpen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Sharpen_Sharpen")]
[RDJsonObjectSerializable]
public struct SharpenSharpen : IFilter
{
	public FilterType Type => FilterType.SharpenSharpen;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Value2</b>.
	/// </summary>
	[RDJsonAlias("Value2")]
	public float Value2 { get; set; }
}