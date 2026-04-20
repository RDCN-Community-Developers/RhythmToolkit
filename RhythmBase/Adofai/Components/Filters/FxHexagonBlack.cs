namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hexagon Black</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Hexagon_Black")]
[RDJsonObjectSerializable]
public struct FxHexagonBlack : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxHexagonBlack;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}