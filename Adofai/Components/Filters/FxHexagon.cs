namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hexagon</b>.
/// </summary>
public struct FxHexagon : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Hexagon";
#else
	public static string Name => "CameraFilterPack_FX_Hexagon";
#endif
}