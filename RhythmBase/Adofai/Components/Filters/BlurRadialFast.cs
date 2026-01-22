namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Radial Fast</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Radial_Fast")]
public struct BlurRadialFast : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>MovX</b>.
	/// </summary>
	[RDJsonAlias("MovX")]
	public float MovX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>MovY</b>.
	/// </summary>
	[RDJsonAlias("MovY")]
	public float MovY { get; set; }
}