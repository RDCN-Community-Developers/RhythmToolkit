namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing CellShading</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_CellShading")]
public struct DrawingCellShading : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>EdgeSize</b>.
	/// </summary>
	[RDJsonProperty("EdgeSize")]
	public float EdgeSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorLevel</b>.
	/// </summary>
	[RDJsonProperty("ColorLevel")]
	public float ColorLevel { get; set; }
}