namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Glitch3</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Glitch3")]
public struct FxGlitch3 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Glitch</b>.
	/// </summary>
	[RDJsonProperty("_Glitch")]
	public float Glitch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Noise</b>.
	/// </summary>
	[RDJsonProperty("_Noise")]
	public float Noise { get; set; }
}