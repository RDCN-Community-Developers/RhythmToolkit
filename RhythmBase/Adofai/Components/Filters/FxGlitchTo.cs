namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Glitch2</b>.
/// </summary>
public struct FxGlitchTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Glitch</b>.
	/// </summary>
	[RDJsonProperty("Glitch")]
	public float Glitch { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Glitch2";
#else
	public static string Name => "CameraFilterPack_FX_Glitch2";
#endif
}