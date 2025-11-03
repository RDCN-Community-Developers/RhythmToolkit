namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Glitch1</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Glitch1")]
public struct FxGlitch1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Glitch</b>.
	/// </summary>
	[RDJsonProperty("Glitch")]
	public float Glitch { get; set; }
}