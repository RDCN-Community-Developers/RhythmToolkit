namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga2")]
public struct DrawingMangaTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}