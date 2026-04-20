namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hexagon</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Hexagon")]
[RDJsonObjectSerializable]
public struct FxHexagon : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxHexagon;
}