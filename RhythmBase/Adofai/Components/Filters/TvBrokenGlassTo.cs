namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV BrokenGlass2</b>.
/// </summary>
public struct TvBrokenGlassTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Bullet_4</b>.
	/// </summary>
	[RDJsonProperty("Bullet_4")]
	public float Bullet4 { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_BrokenGlass2";
#else
	public static string Name => "CameraFilterPack_TV_BrokenGlass2";
#endif
}