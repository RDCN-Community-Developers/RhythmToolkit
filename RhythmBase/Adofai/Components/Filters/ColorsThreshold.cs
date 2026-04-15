namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Threshold</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Threshold")]
[RDJsonObjectSerializable]
public struct ColorsThreshold : IFilter
{
	public FilterType Type => FilterType.ColorsThreshold;
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonAlias("Threshold")]
	public float Threshold { get; set; }
}