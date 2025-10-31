namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX 8bits gb</b>.
/// </summary>
public struct Fx8BitsGb : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_8bits_gb";
#else
	public static string Name => "CameraFilterPack_FX_8bits_gb";
#endif
}