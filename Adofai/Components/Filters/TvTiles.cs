namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Tiles</b>.
/// </summary>
public struct TvTiles : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StretchX</b>.
	/// </summary>
	public float StretchX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>StretchY</b>.
	/// </summary>
	public float StretchY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Tiles";
#else
	public static string Name => "CameraFilterPack_TV_Tiles";
#endif
}