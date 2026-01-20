namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV VHS</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_VHS")]
public struct TvVhs : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Cryptage</b>.
	/// </summary>
	[RDJsonAlias("Cryptage")]
	public float Cryptage { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	[RDJsonAlias("Parasite")]
	public float Parasite { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>WhiteParasite</b>.
	/// </summary>
	[RDJsonAlias("WhiteParasite")]
	public float WhiteParasite { get; set; }
}