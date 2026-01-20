namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Tiles</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Tiles")]
public struct TvTiles : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StretchX</b>.
	/// </summary>
	[RDJsonAlias("StretchX")]
	public float StretchX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StretchY</b>.
	/// </summary>
	[RDJsonAlias("StretchY")]
	public float StretchY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}