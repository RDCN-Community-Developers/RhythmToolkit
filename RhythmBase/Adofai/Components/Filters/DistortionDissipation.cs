namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Dissipation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Dissipation")]
[RDJsonObjectSerializable]
public struct DistortionDissipation : IFilter
{
	public FilterType Type => FilterType.DistortionDissipation;
	/// <summary>
	/// Gets or sets the value of the <b>Dissipation</b>.
	/// </summary>
	[RDJsonAlias("Dissipation")]
	public float Dissipation { get; set; }
}