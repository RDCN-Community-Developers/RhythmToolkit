namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Dissipation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Dissipation")]
public struct DistortionDissipation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Dissipation</b>.
	/// </summary>
	[RDJsonProperty("Dissipation")]
	public float Dissipation { get; set; }
}