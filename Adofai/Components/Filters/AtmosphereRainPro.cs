namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Atmosphere Rain Pro</b>.
/// </summary>
public struct AtmosphereRainPro : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DirectionX</b>.
	/// </summary>
	public float DirectionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StormFlashOnOff</b>.
	/// </summary>
	public float StormFlashOnOff { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DropOnOff</b>.
	/// </summary>
	public float DropOnOff { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Atmosphere_Rain_Pro";
#else
	public static string Name => "CameraFilterPack_Atmosphere_Rain_Pro";
#endif
}