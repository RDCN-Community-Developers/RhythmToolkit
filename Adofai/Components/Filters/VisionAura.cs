namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Aura</b>.
/// </summary>
public struct VisionAura : IFilter
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
	public readonly string Name => "CameraFilterPack_Vision_Aura";
#else
	public static string Name => "CameraFilterPack_Vision_Aura";
#endif
}