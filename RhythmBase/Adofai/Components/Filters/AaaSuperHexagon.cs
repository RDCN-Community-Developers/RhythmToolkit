using RhythmBase.Global.Components.Vector;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>AAA SuperHexagon</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_AAA_SuperHexagon")]
public struct AaaSuperHexagon : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_AlphaHexa</b>.
	/// </summary>
	[RDJsonAlias("_AlphaHexa")]
	public float AlphaHexa { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>HexaSize</b>.
	/// </summary>
	[RDJsonAlias("HexaSize")]
	public float HexaSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_BorderSize</b>.
	/// </summary>
	[RDJsonAlias("_BorderSize")]
	public float BorderSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_BorderColor</b>.
	/// </summary>
	[RDJsonAlias("_BorderColor")]
	public RDColor BorderColor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_HexaColor</b>.
	/// </summary>
	[RDJsonAlias("_HexaColor")]
	public RDColor HexaColor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SpotSize</b>.
	/// </summary>
	[RDJsonAlias("_SpotSize")]
	public float SpotSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>center</b>.
	/// </summary>
	public RDPointN Center { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonAlias("Radius")]
	public float Radius { get; set; }
}