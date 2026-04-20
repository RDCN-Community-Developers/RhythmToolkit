namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Rainbow2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Rainbow2")]
[RDJsonObjectSerializable]
public struct LightRainbowTo : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.LightRainbowTo;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}