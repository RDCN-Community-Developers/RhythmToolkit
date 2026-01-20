namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Crosshatch</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Crosshatch")]
public struct DrawingCrosshatch : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Width</b>.
	/// </summary>
	[RDJsonAlias("Width")]
	public float Width { get; set; }
}