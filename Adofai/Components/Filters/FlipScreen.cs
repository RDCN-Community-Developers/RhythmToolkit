namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FlipScreen</b>.
/// </summary>
public struct FlipScreen : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FlipScreen";
#else
	public static string Name => "CameraFilterPack_FlipScreen";
#endif
}