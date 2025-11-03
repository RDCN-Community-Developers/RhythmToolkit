namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga5</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga5")]
public struct DrawingManga5 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonProperty("DotSize")]
	public float DotSize { get; set; }
}