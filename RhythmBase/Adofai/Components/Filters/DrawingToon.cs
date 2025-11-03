namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Toon</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Toon")]
public struct DrawingToon : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonProperty("Threshold")]
	public float Threshold { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonProperty("DotSize")]
	public float DotSize { get; set; }
}