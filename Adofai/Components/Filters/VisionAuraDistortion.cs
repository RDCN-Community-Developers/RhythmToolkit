namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision AuraDistortion</b>.
/// </summary>
public struct VisionAuraDistortion : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Twist</b>.
	/// </summary>
	public float Twist { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color</b>.
	/// </summary>
	public RDColor Color { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	public float PosY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_AuraDistortion";
#else
	public static string Name => "CameraFilterPack_Vision_AuraDistortion";
#endif
}