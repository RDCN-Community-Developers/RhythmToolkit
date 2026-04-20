namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Bloom</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Bloom")]
[RDJsonObjectSerializable]
public struct BlurBloom : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlurBloom;
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonAlias("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Glow</b>.
	/// </summary>
	[RDJsonAlias("Glow")]
	public float Glow { get; set; }
}