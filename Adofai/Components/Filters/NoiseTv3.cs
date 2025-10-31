namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Noise TV 3</b>.
/// </summary>
public struct NoiseTv3 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Noise_TV_3";
#else
	public static string Name => "CameraFilterPack_Noise_TV_3";
#endif
}