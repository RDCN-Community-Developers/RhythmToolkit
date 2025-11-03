namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Film Grain</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Film_Grain")]
public struct FilmGrain : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
}