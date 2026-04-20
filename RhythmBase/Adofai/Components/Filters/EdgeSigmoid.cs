namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Sigmoid</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Sigmoid")]
[RDJsonObjectSerializable]
public struct EdgeSigmoid : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.EdgeSigmoid;
	/// <summary>
	/// Gets or sets the value of the <b>Gain</b>.
	/// </summary>
	[RDJsonAlias("Gain")]
	public float Gain { get; set; }
}