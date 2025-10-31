namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Videoflip</b>.
/// </summary>
public struct TvVideoflip : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Videoflip";
#else
	public static string Name => "CameraFilterPack_TV_Videoflip";
#endif
}