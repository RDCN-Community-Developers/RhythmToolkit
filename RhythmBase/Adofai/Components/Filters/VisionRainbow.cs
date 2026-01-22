namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Rainbow</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Rainbow")]
public struct VisionRainbow : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
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
	/// <summary>
	/// Gets or sets the value of the <b>Colors</b>.
	/// </summary>
	[RDJsonAlias("Colors")]
	public float Colors { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Vision</b>.
	/// </summary>
	[RDJsonAlias("Vision")]
	public float Vision { get; set; }
}