namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Plasma</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Plasma")]
[RDJsonObjectSerializable]
public struct FxPlasma : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxPlasma;
}