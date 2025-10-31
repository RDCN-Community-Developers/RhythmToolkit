namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Comics</b>.
/// </summary>
public struct DrawingComics : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	public float DotSize { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Comics";
#else
	public static string Name => "CameraFilterPack_Drawing_Comics";
#endif
}