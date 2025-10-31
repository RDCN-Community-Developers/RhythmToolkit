namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Psycho</b>.
/// </summary>
public struct FxPsycho : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Psycho";
#else
	public static string Name => "CameraFilterPack_FX_Psycho";
#endif
}