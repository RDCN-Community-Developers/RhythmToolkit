namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Aura</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Aura")]
public struct VisionAura : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Twist</b>.
	/// </summary>
	[RDJsonAlias("Twist")]
	public float Twist { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color</b>.
	/// </summary>
	[RDJsonAlias("Color")]
	public RDColor Color { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonAlias("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonAlias("PosY")]
	public float PosY { get; set; }
}