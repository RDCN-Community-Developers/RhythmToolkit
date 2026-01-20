namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion FishEye</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_FishEye")]
public struct DistortionFishEye : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}