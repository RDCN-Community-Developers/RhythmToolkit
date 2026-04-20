namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients NeonGradient</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Gradients_NeonGradient")]
[RDJsonObjectSerializable]
public struct GradientsNeonGradient : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.GradientsNeonGradient;
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