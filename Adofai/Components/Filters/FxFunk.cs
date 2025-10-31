namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Funk</b>.
/// </summary>
public struct FxFunk : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Funk";
#else
	public static string Name => "CameraFilterPack_FX_Funk";
#endif
}