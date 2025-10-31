namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Water2</b>.
/// </summary>
public struct LightWaterTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed_X</b>.
	/// </summary>
	[RDJsonProperty("Speed_X")]
	public float SpeedX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed_Y</b>.
	/// </summary>
	[RDJsonProperty("Speed_Y")]
	public float SpeedY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Light_Water2";
#else
	public static string Name => "CameraFilterPack_Light_Water2";
#endif
}