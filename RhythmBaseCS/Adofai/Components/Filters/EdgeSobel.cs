namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Sobel</b>.
/// </summary>
public struct EdgeSobel : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Edge_Sobel";
#else
	public static string Name => "CameraFilterPack_Edge_Sobel";
#endif
}