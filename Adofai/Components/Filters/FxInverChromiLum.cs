namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX InverChromiLum</b>.
/// </summary>
public struct FxInverChromiLum : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_InverChromiLum";
#else
	public static string Name => "CameraFilterPack_FX_InverChromiLum";
#endif
}