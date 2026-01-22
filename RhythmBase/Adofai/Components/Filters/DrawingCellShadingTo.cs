namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing CellShading2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_CellShading2")]
public struct DrawingCellShadingTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>EdgeSize</b>.
	/// </summary>
	[RDJsonAlias("EdgeSize")]
	public float EdgeSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorLevel</b>.
	/// </summary>
	[RDJsonAlias("ColorLevel")]
	public float ColorLevel { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blur</b>.
	/// </summary>
	[RDJsonAlias("Blur")]
	public float Blur { get; set; }
}