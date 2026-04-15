namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DigitalMatrix</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_DigitalMatrix")]
[RDJsonObjectSerializable]
public struct FxDigitalMatrix : IFilter
{
	public FilterType Type => FilterType.FxDigitalMatrix;
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
	/// Gets or sets the value of the <b>ColorR</b>.
	/// </summary>
	[RDJsonAlias("ColorR")]
	public float ColorR { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorG</b>.
	/// </summary>
	[RDJsonAlias("ColorG")]
	public float ColorG { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorB</b>.
	/// </summary>
	[RDJsonAlias("ColorB")]
	public float ColorB { get; set; }
}