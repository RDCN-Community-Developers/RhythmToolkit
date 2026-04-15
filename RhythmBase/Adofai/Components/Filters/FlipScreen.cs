namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FlipScreen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FlipScreen")]
[RDJsonObjectSerializable]
public struct FlipScreen : IFilter
{
	public FilterType Type => FilterType.FlipScreen;
}