namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga3</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga3")]
public struct DrawingManga3 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonProperty("DotSize")]
	public float DotSize { get; set; }
}