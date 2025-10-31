namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Noise</b>.
/// </summary>
public struct TvNoise : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Noise";
#else
	public static string Name => "CameraFilterPack_TV_Noise";
#endif
}