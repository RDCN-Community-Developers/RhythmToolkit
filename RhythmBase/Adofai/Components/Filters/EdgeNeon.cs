namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Neon</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Neon")]
[RDJsonObjectSerializable]
public struct EdgeNeon : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.EdgeNeon;
	/// <summary>
	/// Gets or sets the value of the <b>EdgeWeight</b>.
	/// </summary>
	[RDJsonAlias("EdgeWeight")]
	public float EdgeWeight { get; set; }
}