using RhythmBase.Global.Components.Vector;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>AAA SuperComputer</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_AAA_SuperComputer")]
public struct AaaSuperComputer : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_AlphaHexa</b>.
	/// </summary>
	[RDJsonProperty("_AlphaHexa")]
	public float AlphaHexa { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ShapeFormula</b>.
	/// </summary>
	[RDJsonProperty("ShapeFormula")]
	public float ShapeFormula { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Shape</b>.
	/// </summary>
	[RDJsonProperty("Shape")]
	public float Shape { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_BorderSize</b>.
	/// </summary>
	[RDJsonProperty("_BorderSize")]
	public float BorderSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_BorderColor</b>.
	/// </summary>
	[RDJsonProperty("_BorderColor")]
	public RDColor BorderColor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SpotSize</b>.
	/// </summary>
	[RDJsonProperty("_SpotSize")]
	public float SpotSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>center</b>.
	/// </summary>
	public RDPointN Center { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonProperty("Radius")]
	public float Radius { get; set; }
}