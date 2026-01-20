namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX EarthQuake</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_EarthQuake")]
public struct FxEarthQuake : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>X</b>.
	/// </summary>
	[RDJsonAlias("X")]
	public float X { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Y</b>.
	/// </summary>
	[RDJsonAlias("Y")]
	public float Y { get; set; }
}