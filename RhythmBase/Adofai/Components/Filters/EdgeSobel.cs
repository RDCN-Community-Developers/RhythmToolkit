namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Sobel</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Sobel")]
[RDJsonObjectSerializable]
public struct EdgeSobel : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.EdgeSobel;
}