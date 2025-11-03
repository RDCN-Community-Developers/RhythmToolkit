namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Noise</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Noise")]
public struct DistortionNoise : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}