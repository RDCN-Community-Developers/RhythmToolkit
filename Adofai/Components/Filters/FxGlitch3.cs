namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Glitch3</b>.
/// </summary>
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
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Glitch3";
#else
	public static string Name => "CameraFilterPack_FX_Glitch3";
#endif
}