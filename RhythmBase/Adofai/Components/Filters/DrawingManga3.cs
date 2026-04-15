namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga3</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga3")]
[RDJsonObjectSerializable]
public struct DrawingManga3 : IFilter
{
	public FilterType Type => FilterType.DrawingManga3;
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}