namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Mirror</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Mirror")]
[RDJsonObjectSerializable]
public struct FxMirror : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxMirror;
}