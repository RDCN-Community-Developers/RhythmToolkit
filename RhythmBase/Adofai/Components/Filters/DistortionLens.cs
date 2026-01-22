namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Lens</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Lens")]
public struct DistortionLens : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}