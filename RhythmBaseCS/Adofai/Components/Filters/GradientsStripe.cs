namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients Stripe</b>.
/// </summary>
public struct GradientsStripe : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Switch</b>.
	/// </summary>
	[RDJsonProperty("Switch")]
	public float Switch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Gradients_Stripe";
#else
	public static string Name => "CameraFilterPack_Gradients_Stripe";
#endif
}