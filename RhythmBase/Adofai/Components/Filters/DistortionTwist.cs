namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Twist</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Twist")]
public struct DistortionTwist : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>CenterX</b>.
	/// </summary>
	[RDJsonAlias("CenterX")]
	public float CenterX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>CenterY</b>.
	/// </summary>
	[RDJsonAlias("CenterY")]
	public float CenterY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
}