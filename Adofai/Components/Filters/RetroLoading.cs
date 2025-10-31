namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Retro Loading</b>.
/// </summary>
public struct RetroLoading : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Retro_Loading";
#else
	public static string Name => "CameraFilterPack_Retro_Loading";
#endif
}