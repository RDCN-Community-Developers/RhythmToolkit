namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Rainbow</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Rainbow")]
public struct LightRainbow : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
}