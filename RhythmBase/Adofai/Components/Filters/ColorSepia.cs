namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Sepia</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Sepia")]
[RDJsonObjectSerializable]
public struct ColorSepia : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.ColorSepia;
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonAlias("_Fade")]
	public float Fade { get; set; }
}