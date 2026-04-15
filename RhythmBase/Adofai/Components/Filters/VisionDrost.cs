namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Drost</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Drost")]
[RDJsonObjectSerializable]
public struct VisionDrost : IFilter
{
	public FilterType Type => FilterType.VisionDrost;
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}