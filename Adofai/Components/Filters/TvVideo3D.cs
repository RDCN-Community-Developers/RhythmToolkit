namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Video3D</b>.
/// </summary>
public struct TvVideo3D : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Video3D";
#else
	public static string Name => "CameraFilterPack_TV_Video3D";
#endif
}