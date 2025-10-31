namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga Color</b>.
/// </summary>
public struct DrawingMangaColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	public float DotSize { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Manga_Color";
#else
	public static string Name => "CameraFilterPack_Drawing_Manga_Color";
#endif
}