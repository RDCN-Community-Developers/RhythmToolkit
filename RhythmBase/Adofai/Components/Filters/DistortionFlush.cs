namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Flush</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Flush")]
public struct DistortionFlush : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}