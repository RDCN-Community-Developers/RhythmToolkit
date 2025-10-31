namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga</b>.
/// </summary>
public struct DrawingManga : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonProperty("DotSize")]
	public float DotSize { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Manga";
#else
	public static string Name => "CameraFilterPack_Drawing_Manga";
#endif
}