namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Glow Glow Color</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Glow_Glow_Color")]
public struct GlowGlowColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonAlias("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	[RDJsonAlias("FastFilter")]
	public int FastFilter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonAlias("Threshold")]
	public float Threshold { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Precision</b>.
	/// </summary>
	[RDJsonAlias("Precision")]
	public float Precision { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>GlowColor</b>.
	/// </summary>
	[RDJsonAlias("GlowColor")]
	public RDColor GlowColor { get; set; }
}