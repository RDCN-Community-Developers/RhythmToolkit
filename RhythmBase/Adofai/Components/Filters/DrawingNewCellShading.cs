namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing NewCellShading</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_NewCellShading")]
public struct DrawingNewCellShading : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonProperty("Threshold")]
	public float Threshold { get; set; }
}