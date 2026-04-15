namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Rainbow</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Rainbow")]
[RDJsonObjectSerializable]
public struct LightRainbow : IFilter
{
	public FilterType Type => FilterType.LightRainbow;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}