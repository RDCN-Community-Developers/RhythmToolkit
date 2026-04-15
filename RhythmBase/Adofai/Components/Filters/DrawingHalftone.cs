namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Halftone</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Halftone")]
[RDJsonObjectSerializable]
public struct DrawingHalftone : IFilter
{
	public FilterType Type => FilterType.DrawingHalftone;
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonAlias("Threshold")]
	public float Threshold { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}