namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Flag</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Flag")]
public struct DistortionFlag : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}