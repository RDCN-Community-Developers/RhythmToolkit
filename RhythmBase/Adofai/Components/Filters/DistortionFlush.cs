namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Flush</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Flush")]
[RDJsonObjectSerializable]
public struct DistortionFlush : IFilter
{
	public FilterType Type => FilterType.DistortionFlush;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}