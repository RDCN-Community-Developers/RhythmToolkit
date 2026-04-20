namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga")]
[RDJsonObjectSerializable]
public struct DrawingManga : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingManga;
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}