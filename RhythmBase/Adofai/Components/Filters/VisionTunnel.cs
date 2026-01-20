namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Tunnel</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Tunnel")]
public struct VisionTunnel : IFilter
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