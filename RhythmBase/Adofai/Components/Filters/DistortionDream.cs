namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Dream</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Dream")]
[RDJsonObjectSerializable]
public struct DistortionDream : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DistortionDream;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}