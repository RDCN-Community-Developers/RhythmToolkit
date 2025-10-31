namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing EnhancedComics</b>.
/// </summary>
public struct DrawingEnhancedComics : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	public float DotSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ColorR</b>.
	/// </summary>
	[RDJsonProperty("_ColorR")]
	public float ColorR { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ColorG</b>.
	/// </summary>
	[RDJsonProperty("_ColorG")]
	public float ColorG { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ColorB</b>.
	/// </summary>
	[RDJsonProperty("_ColorB")]
	public float ColorB { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Blood</b>.
	/// </summary>
	[RDJsonProperty("_Blood")]
	public float Blood { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SmoothStart</b>.
	/// </summary>
	[RDJsonProperty("_SmoothStart")]
	public float SmoothStart { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SmoothEnd</b>.
	/// </summary>
	[RDJsonProperty("_SmoothEnd")]
	public float SmoothEnd { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorRGB</b>.
	/// </summary>
	public RDColor ColorRGB { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_EnhancedComics";
#else
	public static string Name => "CameraFilterPack_Drawing_EnhancedComics";
#endif
}