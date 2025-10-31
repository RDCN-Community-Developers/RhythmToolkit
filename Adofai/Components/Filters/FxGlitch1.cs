namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Glitch1</b>.
/// </summary>
public struct FxGlitch1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Glitch</b>.
	/// </summary>
	public float Glitch { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Glitch1";
#else
	public static string Name => "CameraFilterPack_FX_Glitch1";
#endif
}