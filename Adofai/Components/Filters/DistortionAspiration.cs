namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Aspiration</b>.
/// </summary>
public struct DistortionAspiration : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonProperty("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonProperty("PosY")]
	public float PosY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Aspiration";
#else
	public static string Name => "CameraFilterPack_Distortion_Aspiration";
#endif
}