namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>AAA SuperComputer</b>.
/// </summary>
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
	public float ShapeFormula { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Shape</b>.
	/// </summary>
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
	[RDJsonProperty("center")]
	public RDPointN Center { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	public float Radius { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_AAA_SuperComputer";
#else
	public static string Name => "CameraFilterPack_AAA_SuperComputer";
#endif
}