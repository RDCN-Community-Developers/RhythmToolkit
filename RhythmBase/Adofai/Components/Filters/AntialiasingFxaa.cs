namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Antialiasing FXAA</b>.
/// </summary>
public struct AntialiasingFxaa : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Antialiasing_FXAA";
#else
	public static string Name => "CameraFilterPack_Antialiasing_FXAA";
#endif
}