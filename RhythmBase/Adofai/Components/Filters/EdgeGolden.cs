namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Golden</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Golden")]
[RDJsonObjectSerializable]
public struct EdgeGolden : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.EdgeGolden;
}