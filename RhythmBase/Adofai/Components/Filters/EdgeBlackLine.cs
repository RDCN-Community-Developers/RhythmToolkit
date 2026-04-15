namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge BlackLine</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_BlackLine")]
[RDJsonObjectSerializable]
public struct EdgeBlackLine : IFilter
{
	public FilterType Type => FilterType.EdgeBlackLine;
}