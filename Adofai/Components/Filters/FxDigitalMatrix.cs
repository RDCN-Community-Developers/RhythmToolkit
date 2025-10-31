namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DigitalMatrix</b>.
/// </summary>
public struct FxDigitalMatrix : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorR</b>.
	/// </summary>
	public float ColorR { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorG</b>.
	/// </summary>
	public float ColorG { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorB</b>.
	/// </summary>
	public float ColorB { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_DigitalMatrix";
#else
	public static string Name => "CameraFilterPack_FX_DigitalMatrix";
#endif
}