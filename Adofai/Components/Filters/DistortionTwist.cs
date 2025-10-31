namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Twist</b>.
/// </summary>
public struct DistortionTwist : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>CenterX</b>.
	/// </summary>
	public float CenterX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>CenterY</b>.
	/// </summary>
	public float CenterY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Twist";
#else
	public static string Name => "CameraFilterPack_Distortion_Twist";
#endif
}