namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Ascii</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Ascii")]
public struct FxAscii : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}