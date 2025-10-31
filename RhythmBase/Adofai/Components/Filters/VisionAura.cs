namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Aura</b>.
/// </summary>
public struct VisionAura : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Twist</b>.
	/// </summary>
	[RDJsonProperty("Twist")]
	public float Twist { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color</b>.
	/// </summary>
	[RDJsonProperty("Color")]
	public RDColor Color { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonProperty("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonProperty("PosY")]
	public float PosY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Aura";
#else
	public static string Name => "CameraFilterPack_Vision_Aura";
#endif
}