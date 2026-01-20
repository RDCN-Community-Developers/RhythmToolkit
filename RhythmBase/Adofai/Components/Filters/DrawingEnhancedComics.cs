namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing EnhancedComics</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_EnhancedComics")]
public struct DrawingEnhancedComics : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	[RDJsonAlias("DotSize")]
	public float DotSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ColorR</b>.
	/// </summary>
	[RDJsonAlias("_ColorR")]
	public float ColorR { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ColorG</b>.
	/// </summary>
	[RDJsonAlias("_ColorG")]
	public float ColorG { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_ColorB</b>.
	/// </summary>
	[RDJsonAlias("_ColorB")]
	public float ColorB { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Blood</b>.
	/// </summary>
	[RDJsonAlias("_Blood")]
	public float Blood { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SmoothStart</b>.
	/// </summary>
	[RDJsonAlias("_SmoothStart")]
	public float SmoothStart { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SmoothEnd</b>.
	/// </summary>
	[RDJsonAlias("_SmoothEnd")]
	public float SmoothEnd { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorRGB</b>.
	/// </summary>
	[RDJsonAlias("ColorRGB")]
	public RDColor ColorRGB { get; set; }
}