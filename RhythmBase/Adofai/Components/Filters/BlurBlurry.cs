namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Blurry</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Blurry")]
public struct BlurBlurry : IFilter
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
}