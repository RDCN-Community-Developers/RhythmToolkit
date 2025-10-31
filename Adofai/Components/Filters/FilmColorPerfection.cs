namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Film ColorPerfection</b>.
/// </summary>
public struct FilmColorPerfection : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Gamma</b>.
	/// </summary>
	public float Gamma { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Film_ColorPerfection";
#else
	public static string Name => "CameraFilterPack_Film_ColorPerfection";
#endif
}