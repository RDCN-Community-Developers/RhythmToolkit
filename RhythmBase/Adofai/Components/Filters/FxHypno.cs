namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hypno</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Hypno")]
public struct FxHypno : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green</b>.
	/// </summary>
	[RDJsonProperty("Green")]
	public float Green { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blue</b>.
	/// </summary>
	[RDJsonProperty("Blue")]
	public float Blue { get; set; }
}