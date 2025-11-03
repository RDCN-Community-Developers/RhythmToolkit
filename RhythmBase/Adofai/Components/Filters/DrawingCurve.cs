namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Curve</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Curve")]
public struct DrawingCurve : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
}