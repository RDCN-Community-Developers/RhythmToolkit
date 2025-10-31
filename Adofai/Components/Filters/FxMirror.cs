namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Mirror</b>.
/// </summary>
public struct FxMirror : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Mirror";
#else
	public static string Name => "CameraFilterPack_FX_Mirror";
#endif
}