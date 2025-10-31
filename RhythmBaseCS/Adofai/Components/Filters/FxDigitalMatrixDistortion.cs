namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DigitalMatrixDistortion</b>.
/// </summary>
public struct FxDigitalMatrixDistortion : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_DigitalMatrixDistortion";
#else
	public static string Name => "CameraFilterPack_FX_DigitalMatrixDistortion";
#endif
}