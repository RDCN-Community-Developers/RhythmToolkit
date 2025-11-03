namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga")]
public struct DrawingManga : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonProperty("DotSize")]
	public float DotSize { get; set; }
}