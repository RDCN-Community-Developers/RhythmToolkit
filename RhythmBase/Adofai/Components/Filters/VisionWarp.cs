namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Warp</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Warp")]
[RDJsonObjectSerializable]
public struct VisionWarp : IFilter
{
	public FilterType Type => FilterType.VisionWarp;
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
}