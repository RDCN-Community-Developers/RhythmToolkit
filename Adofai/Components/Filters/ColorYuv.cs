namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color YUV</b>.
/// </summary>
public struct ColorYuv : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_YUV";
#else
	public static string Name => "CameraFilterPack_Color_YUV";
#endif
}