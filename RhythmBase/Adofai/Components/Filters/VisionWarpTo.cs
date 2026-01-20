namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Warp2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Warp2")]
public struct VisionWarpTo : IFilter
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
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
}