namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Dream2</b>.
/// </summary>
public struct DistortionDreamTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Dream2";
#else
	public static string Name => "CameraFilterPack_Distortion_Dream2";
#endif
}