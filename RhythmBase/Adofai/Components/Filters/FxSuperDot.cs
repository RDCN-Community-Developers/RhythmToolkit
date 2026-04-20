namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX superDot</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_superDot")]
[RDJsonObjectSerializable]
public struct FxSuperDot : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxSuperDot;
}