namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Drunk</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Drunk")]
public struct FxDrunk : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Wavy</b>.
	/// </summary>
	[RDJsonAlias("Wavy")]
	public float Wavy { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColoredSaturate</b>.
	/// </summary>
	[RDJsonAlias("ColoredSaturate")]
	public float ColoredSaturate { get; set; }
}