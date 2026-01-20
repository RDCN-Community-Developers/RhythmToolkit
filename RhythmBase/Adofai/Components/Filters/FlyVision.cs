namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Fly Vision</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Fly_Vision")]
public struct FlyVision : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Zoom</b>.
	/// </summary>
	[RDJsonAlias("Zoom")]
	public float Zoom { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}