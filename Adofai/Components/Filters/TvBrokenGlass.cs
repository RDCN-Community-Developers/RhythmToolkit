namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV BrokenGlass</b>.
/// </summary>
public struct TvBrokenGlass : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Broken_Big</b>.
	/// </summary>
	[RDJsonProperty("Broken_Big")]
	public float BrokenBig { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>LightReflect</b>.
	/// </summary>
	public float LightReflect { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_BrokenGlass";
#else
	public static string Name => "CameraFilterPack_TV_BrokenGlass";
#endif
}