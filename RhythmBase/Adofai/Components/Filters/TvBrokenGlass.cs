namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV BrokenGlass</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_BrokenGlass")]
public struct TvBrokenGlass : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Broken_Big</b>.
	/// </summary>
	[RDJsonAlias("Broken_Big")]
	public float BrokenBig { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>LightReflect</b>.
	/// </summary>
	[RDJsonAlias("LightReflect")]
	public float LightReflect { get; set; }
}