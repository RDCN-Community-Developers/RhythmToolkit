namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Curve</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Curve")]
[RDJsonObjectSerializable]
public struct DrawingCurve : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingCurve;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
}