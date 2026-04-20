namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Videoflip</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Videoflip")]
[RDJsonObjectSerializable]
public struct TvVideoflip : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.TvVideoflip;
}