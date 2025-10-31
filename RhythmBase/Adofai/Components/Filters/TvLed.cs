namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV LED</b>.
/// </summary>
public struct TvLed : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public int Size { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_LED";
#else
	public static string Name => "CameraFilterPack_TV_LED";
#endif
}