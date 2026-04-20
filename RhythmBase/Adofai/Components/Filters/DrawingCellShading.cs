namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing CellShading</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_CellShading")]
[RDJsonObjectSerializable]
public struct DrawingCellShading : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingCellShading;
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
}