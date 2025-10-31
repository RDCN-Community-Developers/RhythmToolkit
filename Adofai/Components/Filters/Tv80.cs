namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV 80</b>.
/// </summary>
public struct Tv80 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_80";
#else
	public static string Name => "CameraFilterPack_TV_80";
#endif
}