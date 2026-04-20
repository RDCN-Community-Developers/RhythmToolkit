namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Glitch Mozaic</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Glitch_Mozaic")]
[RDJsonObjectSerializable]
public struct GlitchMozaic : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.GlitchMozaic;
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
}