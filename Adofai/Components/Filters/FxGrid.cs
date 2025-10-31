namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Grid</b>.
/// </summary>
public struct FxGrid : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Grid";
#else
	public static string Name => "CameraFilterPack_FX_Grid";
#endif
}