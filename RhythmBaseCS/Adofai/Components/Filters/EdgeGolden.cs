namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Golden</b>.
/// </summary>
public struct EdgeGolden : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Edge_Golden";
#else
	public static string Name => "CameraFilterPack_Edge_Golden";
#endif
}