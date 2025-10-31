namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Plasma</b>.
/// </summary>
public struct FxPlasma : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Plasma";
#else
	public static string Name => "CameraFilterPack_FX_Plasma";
#endif
}