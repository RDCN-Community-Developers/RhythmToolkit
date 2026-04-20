namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga5</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga5")]
[RDJsonObjectSerializable]
public struct DrawingManga5 : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingManga5;
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
}