namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients ElectricGradient</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Gradients_ElectricGradient")]
public struct GradientsElectricGradient : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Switch</b>.
	/// </summary>
	[RDJsonProperty("Switch")]
	public float Switch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
}