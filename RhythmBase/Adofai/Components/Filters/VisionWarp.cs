namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Warp</b>.
/// </summary>
public struct VisionWarp : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Value2</b>.
	/// </summary>
	[RDJsonProperty("Value2")]
	public float Value2 { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Warp";
#else
	public static string Name => "CameraFilterPack_Vision_Warp";
#endif
}