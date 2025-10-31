namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX superDot</b>.
/// </summary>
public struct FxSuperDot : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_superDot";
#else
	public static string Name => "CameraFilterPack_FX_superDot";
#endif
}