namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion ShockWave</b>.
/// </summary>
public struct DistortionShockWave : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_ShockWave";
#else
	public static string Name => "CameraFilterPack_Distortion_ShockWave";
#endif
}