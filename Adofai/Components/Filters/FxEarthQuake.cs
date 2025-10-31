namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX EarthQuake</b>.
/// </summary>
public struct FxEarthQuake : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>X</b>.
	/// </summary>
	public float X { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Y</b>.
	/// </summary>
	public float Y { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_EarthQuake";
#else
	public static string Name => "CameraFilterPack_FX_EarthQuake";
#endif
}