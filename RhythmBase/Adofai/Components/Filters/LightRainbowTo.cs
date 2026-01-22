namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Rainbow2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Rainbow2")]
public struct LightRainbowTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}