namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Noise TV 2</b>.
/// </summary>
public struct NoiseTvTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Noise_TV_2";
#else
	public static string Name => "CameraFilterPack_Noise_TV_2";
#endif
}