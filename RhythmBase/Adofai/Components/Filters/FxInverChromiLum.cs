namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX InverChromiLum</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_InverChromiLum")]
[RDJsonObjectSerializable]
public struct FxInverChromiLum : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxInverChromiLum;
}