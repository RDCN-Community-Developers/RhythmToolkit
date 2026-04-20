namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Drunk2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Drunk2")]
[RDJsonObjectSerializable]
public struct FxDrunkTo : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxDrunkTo;
}