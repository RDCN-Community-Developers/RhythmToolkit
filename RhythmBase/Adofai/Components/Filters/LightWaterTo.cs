namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Water2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Water2")]
public struct LightWaterTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed_X</b>.
	/// </summary>
	[RDJsonAlias("Speed_X")]
	public float SpeedX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed_Y</b>.
	/// </summary>
	[RDJsonAlias("Speed_Y")]
	public float SpeedY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
}