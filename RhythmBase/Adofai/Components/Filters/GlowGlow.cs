namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Glow Glow</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Glow_Glow")]
public struct GlowGlow : IFilter
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
}