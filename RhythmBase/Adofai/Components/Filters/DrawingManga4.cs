namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga4</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga4")]
public struct DrawingManga4 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonProperty("DotSize")]
	public float DotSize { get; set; }
}