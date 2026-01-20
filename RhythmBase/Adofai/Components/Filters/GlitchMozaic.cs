namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Glitch Mozaic</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Glitch_Mozaic")]
public struct GlitchMozaic : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
}