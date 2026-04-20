namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Retro Loading</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Retro_Loading")]
[RDJsonObjectSerializable]
public struct RetroLoading : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.RetroLoading;
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}