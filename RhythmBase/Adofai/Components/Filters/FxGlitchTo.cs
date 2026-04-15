namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Glitch2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Glitch2")]
[RDJsonObjectSerializable]
public struct FxGlitchTo : IFilter
{
	public FilterType Type => FilterType.FxGlitchTo;
	/// <summary>
	/// Gets or sets the value of the <b>Glitch</b>.
	/// </summary>
	[RDJsonAlias("Glitch")]
	public float Glitch { get; set; }
}