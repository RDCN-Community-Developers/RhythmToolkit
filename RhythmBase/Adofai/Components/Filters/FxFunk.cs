namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Funk</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Funk")]
[RDJsonObjectSerializable]
public struct FxFunk : IFilter
{
	public FilterType Type => FilterType.FxFunk;
}