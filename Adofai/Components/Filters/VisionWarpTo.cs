namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Warp2</b>.
/// </summary>
public struct VisionWarpTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Value2</b>.
	/// </summary>
	public float Value2 { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	public float Intensity { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Warp2";
#else
	public static string Name => "CameraFilterPack_Vision_Warp2";
#endif
}