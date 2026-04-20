namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga4</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga4")]
[RDJsonObjectSerializable]
public struct DrawingManga4 : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingManga4;
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}