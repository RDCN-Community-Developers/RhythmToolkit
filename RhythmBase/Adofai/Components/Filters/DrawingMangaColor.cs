namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga Color</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga_Color")]
[RDJsonObjectSerializable]
public struct DrawingMangaColor : IFilter
{
	public FilterType Type => FilterType.DrawingMangaColor;
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}