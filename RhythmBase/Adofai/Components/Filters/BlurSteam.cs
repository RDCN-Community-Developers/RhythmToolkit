namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Steam</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Steam")]
[RDJsonObjectSerializable]
public struct BlurSteam : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlurSteam;
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonAlias("Radius")]
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Quality</b>.
	/// </summary>
	[RDJsonAlias("Quality")]
	public float Quality { get; set; }
}