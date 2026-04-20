namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Ascii</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Ascii")]
[RDJsonObjectSerializable]
public struct FxAscii : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxAscii;
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