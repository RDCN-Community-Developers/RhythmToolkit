namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion BigFace</b>.
/// </summary>
public struct DistortionBigFace : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonProperty("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_BigFace";
#else
	public static string Name => "CameraFilterPack_Distortion_BigFace";
#endif
}