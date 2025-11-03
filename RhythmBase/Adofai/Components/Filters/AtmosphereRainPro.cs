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
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DirectionX</b>.
	/// </summary>
	[RDJsonProperty("DirectionX")]
	public float DirectionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StormFlashOnOff</b>.
	/// </summary>
	[RDJsonProperty("StormFlashOnOff")]
	public float StormFlashOnOff { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DropOnOff</b>.
	/// </summary>
	[RDJsonProperty("DropOnOff")]
	public float DropOnOff { get; set; }
}