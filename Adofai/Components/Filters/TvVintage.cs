namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Vintage</b>.
/// </summary>
public struct TvVintage : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Vintage";
#else
	public static string Name => "CameraFilterPack_TV_Vintage";
#endif
}