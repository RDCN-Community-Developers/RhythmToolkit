namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion BlackHole</b>.
/// </summary>
public struct DistortionBlackHole : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_BlackHole";
#else
	public static string Name => "CameraFilterPack_Distortion_BlackHole";
#endif
}