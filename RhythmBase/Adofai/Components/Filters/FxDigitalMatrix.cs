namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DigitalMatrix</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_DigitalMatrix")]
public struct FxDigitalMatrix : IFilter
{
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
	/// Gets or sets the value of the <b>ColorR</b>.
	/// </summary>
	[RDJsonProperty("ColorR")]
	public float ColorR { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorG</b>.
	/// </summary>
	[RDJsonProperty("ColorG")]
	public float ColorG { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorB</b>.
	/// </summary>
	[RDJsonProperty("ColorB")]
	public float ColorB { get; set; }
}