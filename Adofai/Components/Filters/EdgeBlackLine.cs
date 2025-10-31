namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge BlackLine</b>.
/// </summary>
public struct EdgeBlackLine : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Edge_BlackLine";
#else
	public static string Name => "CameraFilterPack_Edge_BlackLine";
#endif
}