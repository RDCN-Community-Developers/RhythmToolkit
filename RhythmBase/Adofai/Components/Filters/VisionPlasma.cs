namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Plasma</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Plasma")]
[RDJsonObjectSerializable]
public struct VisionPlasma : IFilter
{
	public FilterType Type => FilterType.VisionPlasma;
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Value2</b>.
	/// </summary>
	[RDJsonAlias("Value2")]
	public float Value2 { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
}