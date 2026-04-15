namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Lens</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Lens")]
[RDJsonObjectSerializable]
public struct DistortionLens : IFilter
{
	public FilterType Type => FilterType.DistortionLens;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}