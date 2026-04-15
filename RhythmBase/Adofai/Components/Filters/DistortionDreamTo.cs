namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Dream2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Dream2")]
[RDJsonObjectSerializable]
public struct DistortionDreamTo : IFilter
{
	public FilterType Type => FilterType.DistortionDreamTo;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}