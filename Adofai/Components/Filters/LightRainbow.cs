namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Rainbow</b>.
/// </summary>
public struct LightRainbow : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Light_Rainbow";
#else
	public static string Name => "CameraFilterPack_Light_Rainbow";
#endif
}