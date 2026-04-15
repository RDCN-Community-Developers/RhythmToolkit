namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients Tech</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Gradients_Tech")]
[RDJsonObjectSerializable]
public struct GradientsTech : IFilter
{
	public FilterType Type => FilterType.GradientsTech;
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