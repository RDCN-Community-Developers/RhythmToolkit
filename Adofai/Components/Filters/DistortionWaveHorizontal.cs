namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Wave Horizontal</b>.
/// </summary>
public struct DistortionWaveHorizontal : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>WaveIntensity</b>.
	/// </summary>
	public float WaveIntensity { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Wave_Horizontal";
#else
	public static string Name => "CameraFilterPack_Distortion_Wave_Horizontal";
#endif
}