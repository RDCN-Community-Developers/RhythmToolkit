using RhythmBase.Adofai.Events;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>AAA SuperHexagon</b>.
/// </summary>
public struct AaaSuperHexagon : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_AlphaHexa</b>.
	/// </summary>
	[RDJsonProperty("_AlphaHexa")]
	public float AlphaHexa { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>HexaSize</b>.
	/// </summary>
	public float HexaSize { get; set; }
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
	/// Gets or sets the value of the <b>_HexaColor</b>.
	/// </summary>
	[RDJsonProperty("_HexaColor")]
	public RDColor HexaColor { get; set; }
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
	public readonly string Name => "CameraFilterPack_AAA_SuperHexagon";
#else
	public static string Name => "CameraFilterPack_AAA_SuperHexagon";
#endif
}