namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients Stripe</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Gradients_Stripe")]
[RDJsonObjectSerializable]
public struct GradientsStripe : IFilter
{
	public FilterType Type => FilterType.GradientsStripe;
	/// <summary>
	/// Gets or sets the value of the <b>Switch</b>.
	/// </summary>
	[RDJsonAlias("Switch")]
	public float Switch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}