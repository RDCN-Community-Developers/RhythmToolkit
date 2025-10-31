namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hypno</b>.
/// </summary>
public struct FxHypno : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green</b>.
	/// </summary>
	public float Green { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blue</b>.
	/// </summary>
	public float Blue { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Hypno";
#else
	public static string Name => "CameraFilterPack_FX_Hypno";
#endif
}