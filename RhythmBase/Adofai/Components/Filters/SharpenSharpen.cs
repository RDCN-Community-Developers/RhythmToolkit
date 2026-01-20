namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Sharpen Sharpen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Sharpen_Sharpen")]
public struct SharpenSharpen : IFilter
{
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