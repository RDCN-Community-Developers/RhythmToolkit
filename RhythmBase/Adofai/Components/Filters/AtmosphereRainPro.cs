namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Atmosphere Rain Pro</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Atmosphere_Rain_Pro")]
public struct AtmosphereRainPro : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DirectionX</b>.
	/// </summary>
	[RDJsonAlias("DirectionX")]
	public float DirectionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StormFlashOnOff</b>.
	/// </summary>
	[RDJsonAlias("StormFlashOnOff")]
	public float StormFlashOnOff { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DropOnOff</b>.
	/// </summary>
	[RDJsonAlias("DropOnOff")]
	public float DropOnOff { get; set; }
}