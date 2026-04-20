namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Laplacian</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Laplacian")]
[RDJsonObjectSerializable]
public struct DrawingLaplacian : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingLaplacian;
}