namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Twist Square</b>.
/// </summary>
public struct DistortionTwistSquare : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>CenterX</b>.
	/// </summary>
	[RDJsonProperty("CenterX")]
	public float CenterX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>CenterY</b>.
	/// </summary>
	[RDJsonProperty("CenterY")]
	public float CenterY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Twist_Square";
#else
	public static string Name => "CameraFilterPack_Distortion_Twist_Square";
#endif
}