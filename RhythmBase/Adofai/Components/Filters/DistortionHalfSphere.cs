namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Half Sphere</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Half_Sphere")]
public struct DistortionHalfSphere : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>SphereSize</b>.
	/// </summary>
	[RDJsonAlias("SphereSize")]
	public float SphereSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Strength</b>.
	/// </summary>
	[RDJsonAlias("Strength")]
	public float Strength { get; set; }
}