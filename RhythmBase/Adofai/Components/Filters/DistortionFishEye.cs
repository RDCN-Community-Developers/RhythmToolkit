namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion FishEye</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_FishEye")]
[RDJsonObjectSerializable]
public struct DistortionFishEye : IFilter
{
	public FilterType Type => FilterType.DistortionFishEye;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}