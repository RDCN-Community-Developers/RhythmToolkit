namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Antialiasing FXAA</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Antialiasing_FXAA")]
[RDJsonObjectSerializable]
public struct AntialiasingFxaa : IFilter
{
	public FilterType Type => FilterType.AntialiasingFxaa;
}