namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Laplacian</b>.
/// </summary>
public struct DrawingLaplacian : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Laplacian";
#else
	public static string Name => "CameraFilterPack_Drawing_Laplacian";
#endif
}