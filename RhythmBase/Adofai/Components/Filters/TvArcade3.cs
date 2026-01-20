namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV ARCADE 3</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_ARCADE_3")]
public struct TvArcade3 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Interferance_Size</b>.
	/// </summary>
	[RDJsonAlias("Interferance_Size")]
	public float InterferanceSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Interferance_Speed</b>.
	/// </summary>
	[RDJsonAlias("Interferance_Speed")]
	public float InterferanceSpeed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	[RDJsonAlias("Contrast")]
	public float Contrast { get; set; }
}