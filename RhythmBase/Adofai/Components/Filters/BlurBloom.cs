namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Bloom</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Bloom")]
public struct BlurBloom : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonProperty("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Glow</b>.
	/// </summary>
	[RDJsonProperty("Glow")]
	public float Glow { get; set; }
}