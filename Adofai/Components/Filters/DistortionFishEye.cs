namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion FishEye</b>.
/// </summary>
public struct DistortionFishEye : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_FishEye";
#else
	public static string Name => "CameraFilterPack_Distortion_FishEye";
#endif
}