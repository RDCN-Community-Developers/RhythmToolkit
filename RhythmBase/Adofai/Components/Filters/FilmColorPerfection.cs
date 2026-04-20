namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Film ColorPerfection</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Film_ColorPerfection")]
[RDJsonObjectSerializable]
public struct FilmColorPerfection : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FilmColorPerfection;
	/// <summary>
	/// Gets or sets the value of the <b>Gamma</b>.
	/// </summary>
	[RDJsonAlias("Gamma")]
	public float Gamma { get; set; }
}