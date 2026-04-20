namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Dot Circle</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Dot_Circle")]
[RDJsonObjectSerializable]
public struct FxDotCircle : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxDotCircle;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}