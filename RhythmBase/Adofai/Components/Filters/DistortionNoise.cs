namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Noise</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Noise")]
[RDJsonObjectSerializable]
public struct DistortionNoise : IFilter
{
	public FilterType Type => FilterType.DistortionNoise;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}