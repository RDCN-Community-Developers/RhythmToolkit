namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Lines</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Lines")]
public struct DrawingLines : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Number</b>.
	/// </summary>
	[RDJsonAlias("Number")]
	public float Number { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Random</b>.
	/// </summary>
	[RDJsonAlias("Random")]
	public float Random { get; set; }
}