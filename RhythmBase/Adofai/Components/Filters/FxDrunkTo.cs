namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Drunk2</b>.
/// </summary>
public struct FxDrunkTo : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Drunk2";
#else
	public static string Name => "CameraFilterPack_FX_Drunk2";
#endif
}