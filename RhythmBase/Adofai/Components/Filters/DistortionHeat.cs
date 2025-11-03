namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Heat</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Heat")]
public struct DistortionHeat : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}