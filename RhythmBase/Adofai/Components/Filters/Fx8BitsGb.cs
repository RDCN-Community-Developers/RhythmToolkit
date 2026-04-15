namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX 8bits gb</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_8bits_gb")]
[RDJsonObjectSerializable]
public struct Fx8BitsGb : IFilter
{
	public FilterType Type => FilterType.Fx8BitsGb;
}