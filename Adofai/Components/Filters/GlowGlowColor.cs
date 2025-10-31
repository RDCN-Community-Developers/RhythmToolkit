namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Glow Glow Color</b>.
/// </summary>
public struct GlowGlowColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonProperty("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	[RDJsonProperty("FastFilter")]
	public int FastFilter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonProperty("Threshold")]
	public float Threshold { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Precision</b>.
	/// </summary>
	[RDJsonProperty("Precision")]
	public float Precision { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>GlowColor</b>.
	/// </summary>
	[RDJsonProperty("GlowColor")]
	public RDColor GlowColor { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Glow_Glow_Color";
#else
	public static string Name => "CameraFilterPack_Glow_Glow_Color";
#endif
}