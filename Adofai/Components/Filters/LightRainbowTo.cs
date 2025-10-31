namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Rainbow2</b>.
/// </summary>
public struct LightRainbowTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Light_Rainbow2";
#else
	public static string Name => "CameraFilterPack_Light_Rainbow2";
#endif
}