namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion BigFace</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_BigFace")]
[RDJsonObjectSerializable]
public struct DistortionBigFace : IFilter
{
	public FilterType Type => FilterType.DistortionBigFace;
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonAlias("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}