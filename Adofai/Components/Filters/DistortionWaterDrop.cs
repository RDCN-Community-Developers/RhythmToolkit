namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Water Drop</b>.
/// </summary>
public struct DistortionWaterDrop : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>WaveIntensity</b>.
	/// </summary>
	public float WaveIntensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>NumberOfWaves</b>.
	/// </summary>
	public int NumberOfWaves { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Water_Drop";
#else
	public static string Name => "CameraFilterPack_Distortion_Water_Drop";
#endif
}