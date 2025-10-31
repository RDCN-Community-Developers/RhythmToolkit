namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Half Sphere</b>.
/// </summary>
public struct DistortionHalfSphere : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>SphereSize</b>.
	/// </summary>
	public float SphereSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Strength</b>.
	/// </summary>
	public float Strength { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Half_Sphere";
#else
	public static string Name => "CameraFilterPack_Distortion_Half_Sphere";
#endif
}