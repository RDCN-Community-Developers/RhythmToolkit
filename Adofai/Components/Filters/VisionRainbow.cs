namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Rainbow</b>.
/// </summary>
public struct VisionRainbow : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Colors</b>.
	/// </summary>
	public float Colors { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vision</b>.
	/// </summary>
	public float Vision { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Rainbow";
#else
	public static string Name => "CameraFilterPack_Vision_Rainbow";
#endif
}