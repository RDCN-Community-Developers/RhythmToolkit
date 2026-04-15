namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Flag</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Flag")]
[RDJsonObjectSerializable]
public struct DistortionFlag : IFilter
{
	public FilterType Type => FilterType.DistortionFlag;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}