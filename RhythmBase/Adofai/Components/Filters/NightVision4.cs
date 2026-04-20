namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>NightVision 4</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_NightVision_4")]
[RDJsonObjectSerializable]
public struct NightVision4 : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.NightVision4;
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonAlias("FadeFX")]
	public float FadeFX { get; set; }
}