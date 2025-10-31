namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV CompressionFX</b>.
/// </summary>
public struct TvCompressionFX : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	public float Parasite { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_CompressionFX";
#else
	public static string Name => "CameraFilterPack_TV_CompressionFX";
#endif
}