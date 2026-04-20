namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing NewCellShading</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_NewCellShading")]
[RDJsonObjectSerializable]
public struct DrawingNewCellShading : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingNewCellShading;
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonAlias("Threshold")]
	public float Threshold { get; set; }
}