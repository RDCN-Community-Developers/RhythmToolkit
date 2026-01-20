namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients FireGradient</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Gradients_FireGradient")]
public struct GradientsFireGradient : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Switch</b>.
	/// </summary>
	[RDJsonAlias("Switch")]
	public float Switch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}